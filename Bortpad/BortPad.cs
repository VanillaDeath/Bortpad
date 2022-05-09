using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Web;
using System.Diagnostics;

namespace Bortpad
{
    public partial class BortForm : Form
    {
        internal string programName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        internal string searchQuery, searchReplaceString;
        internal bool searchReverse, searchMatchCase, searchWrapAround;
        private const string defaultFilename = "Untitled";
        private string filename;
        private bool ChangesMade;
        private bool isFile;
        private Encoding encodingSetting;
        private FindPrompt find;
        private ReplacePrompt replace;
        public BortForm(string filenameSpecified = null)
        {
            InitializeComponent();
            isFile = filenameSpecified != null && File.Exists(filenameSpecified) ? true : false;
            ChangesMade = false;
            filename = isFile ? filenameSpecified : defaultFilename;
            encodingSetting = Encoding.UTF8;
            encodingStatus.Text = encodingSetting.EncodingName;
            searchQuery = "";
            searchReplaceString = "";
            searchReverse = false;
            searchMatchCase = false;
            searchWrapAround = false;
            UpdateTitle();
        }

        private bool SaveConfirmPrompt(bool force = false)
        {
            if (force || ChangesMade)
            {
                SaveConfirmPrompt SCP = new SaveConfirmPrompt(filename);
                SCP.ShowDialog();
                switch (SCP.DialogResult)
                {
                    case DialogResult.Yes:
                        // Save/Show Save Dialog
                        return SaveDocument();
                    case DialogResult.No:
                        return true;
                    default:
                        return false;
                }
            }
            // Return: false = cancel open dialog/close app action
            // true = we're good to proceed and launch open dialog/close app/etc
            return true;
        }
        
        private void NewDocument()
        {
            if (SaveConfirmPrompt(false))
            {
                CleanEditor();
                ChangesMade = false;
                isFile = false;
                filename = defaultFilename;
                UpdateTitle();
                editor.Select();
            }
        }

        private void CleanEditor()
        {
            editor.Clear();
            editor.ClearUndo();
            editor.ResetText();
        }

        private void UpdateTitle()
        {
            this.Text = (ChangesMade ? "*" : "") + filename + " - " + programName;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(editor.Text, editor.Font, Brushes.Black, 20, 20);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Copy();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void OpenDocument()
        {
            if (SaveConfirmPrompt(false))
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    CleanEditor();
                    editor.Text = File.ReadAllText(filename, encodingSetting);
                    ChangesMade = false;
                    isFile = true;
                    UpdateTitle();
                    editor.Select();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool hasText = editor.Text.Length > 0;
            findToolStripMenuItem.Enabled = hasText;
            findNextToolStripMenuItem.Enabled = canRepeatFind();
            findPreviousToolStripMenuItem.Enabled = canRepeatFind();
            replaceToolStripMenuItem.Enabled = hasText;
            ChangesMade = true;
            UpdatePos();
            UpdateTitle();
        }

        private void UpdatePos()
        {
            if (statusBar.Visible)
            {
                int line = editor.GetLineFromCharIndex(editor.SelectionStart);
                int column = editor.SelectionStart - editor.GetFirstCharIndexFromLine(line);
                position.Text = "Ln " + (line + 1) + ", Col " + (column + 1);
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.WordWrap = !editor.WordWrap;
            wordWrapToolStripMenuItem.Checked = editor.WordWrap;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font oldFont = editor.Font;
            fontDlg.Font = oldFont;
            DialogResult result = fontDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                FontDlg_Apply(this.AcceptButton, new System.EventArgs());
            }
            else if (result == DialogResult.Cancel)
            {
                editor.Font = oldFont;
            }
        }

        private void FontDlg_Apply(object sender, EventArgs e)
        {
            editor.Font = fontDlg.Font;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument(true);
        }

        private bool SaveDocument(bool saveAs = false)
        {
            if (isFile && !saveAs)
            {
                File.WriteAllText(filename, editor.Text, encodingSetting);
                ChangesMade = false;
                UpdateTitle();
                return true;
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = saveFileDialog1.FileName;
                    File.WriteAllText(filename, editor.Text, encodingSetting);
                    isFile = true;
                    ChangesMade = false;
                    UpdateTitle();
                    return true;
                }
            }
            return false;
        }

        private void satusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleStatusBar();
        }

        private void ToggleStatusBar()
        {
            statusBar.Visible = !statusBar.Visible;
            statusBarToolStripMenuItem.Checked = statusBar.Visible;
            UpdatePos();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SelectAll();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SelectedText = "";
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            editor.Paste(now.ToString("g"));
        }

        private void editor_MouseDown(object sender, MouseEventArgs e)
        {
            UpdatePos();
        }

        private void editor_MouseUp(object sender, MouseEventArgs e)
        {
            UpdatePos();
        }

        private void editor_KeyDown(object sender, KeyEventArgs e)
        {
            UpdatePos();
        }

        private void editor_KeyUp(object sender, KeyEventArgs e)
        {
            UpdatePos();
        }

        private void editor_KeyPress(object sender, KeyPressEventArgs e)
        {
            UpdatePos();
        }

        private void editor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                UpdatePos();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !SaveConfirmPrompt();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetup();
        }

        public void PageSetup()
        {
            pageSetupDialog1.ShowDialog();
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            bool isSelected = editor.SelectionLength > 0;
            cutToolStripMenuItem.Enabled = isSelected;
            copyToolStripMenuItem.Enabled = isSelected;
            deleteToolStripMenuItem.Enabled = isSelected;
            searchWithBortToolStripMenuItem.Enabled = isSelected;
            pasteToolStripMenuItem.Enabled = Clipboard.GetText().Length > 0;
            undoToolStripMenuItem.Enabled = editor.CanUndo;
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newWindow();
        }
        private void newWindow()
        {
            Process.Start(Application.ExecutablePath);
        }

        private void BortForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Application.OpenForms.OfType<BortForm>().Any())
            {
                Application.Exit();
            }
        }

        private void searchWithBortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchWeb();
        }

        private void searchWeb()
        {
            if (editor.SelectionLength > 0)
            {
                string searchString = editor.SelectedText;
                Process.Start("https://google.com/search?q=" + HttpUtility.UrlEncode(editor.SelectedText));
            }
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToLine();
        }

        private void GoToLine()
        {
            GoToPrompt gt = new GoToPrompt();
            if (gt.ShowDialog() == DialogResult.OK)
            {
                editor.Select(editor.GetFirstCharIndexFromLine(gt.LineNumber - 1), 0);
                editor.Focus();
                editor.ScrollToCaret();
                UpdatePos();
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findInEditor();
        }

        private void findInEditor()
        {
            // find = Application.OpenForms.OfType<FindPrompt>().FirstOrDefault();
            if (find != null && find.Visible)
            {
                find.BringToFront();
                return;
            }
            find = new FindPrompt();
            find.Show(this);
        }

        private void editToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            searchWithBortToolStripMenuItem.Enabled = true;
        }

        private BortForm setStatus(string text = "")
        {
            statusBarLeft.Text = text;
            return this;
        }
        private int findNextPrev(bool prev = false)
        {
            int pos = OmniIndexOf(getSearchQuery(), prev, editor.SelectionStart + (prev ? 0 : editor.SelectionLength), getSearchMatchCase());
            if (pos == -1 && getSearchWrapAround())
            {
                pos = OmniIndexOf(getSearchQuery(), prev, prev ? editor.Text.Length - 1 : 0, getSearchMatchCase());
                setStatus(pos == -1 ? "" : "Found next from the " + (prev ? "bottom" : "top"));
            } else
            {
                setStatus();
            }
            gotoResult(pos);
            return pos;
        }
        private int findNext()
        {
            return findNextPrev(false);
        }
        private int findPrev()
        {
            return findNextPrev(true);
        }

        private void gotoResult(int pos)
        {
            string sq = getSearchQuery();
            if (pos != -1)
            {
                editor.SelectionStart = pos;
                editor.SelectionLength = sq.Length;
                editor.ScrollToCaret();
                UpdatePos();
            }
            else
            {
                cantFind(sq);
            }
        }
        private bool canRepeatFind()
        {
            return getSearchQuery().Length > 0 && editor.Text.Length > 0;
        }
        internal int findFromPrompt(string query, bool reverse, bool matchCase, bool wrapAround)
        {
            setSearchQuery(query).setSearchReverse(reverse).setSearchMatchCase(matchCase).setSearchWrapAround(wrapAround);
            findNextToolStripMenuItem.Enabled = canRepeatFind();
            findPreviousToolStripMenuItem.Enabled = canRepeatFind();

            return getSearchReverse() ? findPrev() : findNext();
        }
        internal int replaceFromPrompt(string query, string replaceString, bool matchCase, bool wrapAround)
        {
            setSearchQuery(query).setReplaceString(replaceString).setSearchMatchCase(matchCase).setSearchWrapAround(wrapAround);
            if (editor.SelectedText.Equals(query, getComparisonType(matchCase))) {
                editor.SelectedText = replaceString;
            }
            return findFromPrompt(query, false, matchCase, wrapAround);
        }
        internal void replaceAll(string query, string replaceString, bool matchCase, bool wrapAround)
        {
            setSearchQuery(query).setReplaceString(replaceString).setSearchMatchCase(matchCase).setSearchWrapAround(wrapAround);
            string input = string.Format(@"({0})", System.Text.RegularExpressions.Regex.Escape(query));
            string pattern = string.Format("{0}", System.Text.RegularExpressions.Regex.Escape(replaceString));
            editor.Text = System.Text.RegularExpressions.Regex.Replace(
                editor.Text,
                input,
                pattern,
                matchCase ? System.Text.RegularExpressions.RegexOptions.None : System.Text.RegularExpressions.RegexOptions.IgnoreCase
                );
        }

        internal BortForm setSearchQuery(string query)
        {
            searchQuery = query;
            return this;
        }
        internal BortForm setSearchReverse(bool reverse)
        {
            searchReverse = reverse;
            return this;
        }
        internal BortForm setSearchMatchCase(bool matchCase)
        {
            searchMatchCase = matchCase;
            return this;
        }
        internal BortForm setSearchWrapAround(bool wrapAround)
        {
            searchWrapAround = wrapAround;
            return this;
        }
        internal BortForm setReplaceString(string replaceString)
        {
            searchReplaceString = replaceString;
            return this;
        }
        internal string getSearchQuery()
        {
            return searchQuery;
        }
        internal string getReplaceString()
        {
            return searchReplaceString;
        }
        internal bool getSearchReverse()
        {
            return searchReverse;
        }
        internal bool getSearchMatchCase()
        {
            return searchMatchCase;
        }
        internal bool getSearchWrapAround()
        {
            return searchWrapAround;
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replaceInEditor();
        }

        private void replaceInEditor()
        {
            // replace = Application.OpenForms.OfType<ReplacePrompt>().FirstOrDefault();
            if (replace != null && replace.Visible)
            {
                replace.BringToFront();
                return;
            }
            replace = new ReplacePrompt();
            replace.Show(this);
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findNext();
        }

        private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findPrev();
        }

        private void aboutNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://stevenwilson.ca/contact");
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://stevenwilson.ca/bortpad/help");
        }

        private int OmniIndexOf(string text, bool reverse = false, int start = 0, bool matchCase = false)
        {
            StringComparison sc = getComparisonType(matchCase);
            return reverse ? editor.Text.LastIndexOf(text, start, sc) : editor.Text.IndexOf(text, start, sc);
        }
        private StringComparison getComparisonType(bool matchCase = false)
        {
            return matchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
        }

        internal void cantFind(string text)
        {
            MessageBox.Show("Cannot find \"" + text + "\"", programName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
