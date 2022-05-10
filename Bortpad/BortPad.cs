using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Bortpad
{
    public partial class BortForm : Form
    {
        internal string programName;
        private const string defaultFilename = "Untitled";
        private bool ChangesMade;
        private bool darkModeOn;
        private Encoding encodingSetting;
        private string filename;
        private FindPrompt find;
        private bool isFile;
        private ReplacePrompt replace;
        private string searchQuery, searchReplaceString;
        private bool searchReverse, searchMatchCase, searchWrapAround;

        public BortForm(string filenameSpecified = null)
        {
            InitializeComponent();
            programName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            filename = filenameSpecified;
            encodingSetting = Encoding.UTF8;
            encodingStatus.Text = encodingSetting.EncodingName;
            searchQuery = "";
            searchReplaceString = "";
            searchReverse = false;
            searchMatchCase = false;
            searchWrapAround = false;
            darkModeOn = false;
            Text = defaultFilename + " - " + programName;
        }

        internal int findFromPrompt(string query, bool reverse, bool matchCase, bool wrapAround)
        {
            setSearchQuery(query).setSearchReverse(reverse).setSearchMatchCase(matchCase).setSearchWrapAround(wrapAround);
            findNextToolStripMenuItem.Enabled = canRepeatFind();
            findPreviousToolStripMenuItem.Enabled = canRepeatFind();

            return getSearchReverse() ? findPrev() : findNext();
        }

        internal int getLineNumber(int startFrom = 1)
        {
            return editor.GetLineFromCharIndex(editor.SelectionStart) + startFrom;
        }

        internal int[] getLnCol(int startFrom = 1)
        {
            int line = getLineNumber(0);
            return new int[] { line + startFrom, editor.SelectionStart - editor.GetFirstCharIndexFromLine(line) + startFrom };
        }

        internal string getReplaceString()
        {
            return searchReplaceString;
        }

        internal bool getSearchMatchCase()
        {
            return searchMatchCase;
        }

        internal string getSearchQuery()
        {
            return searchQuery;
        }

        internal bool getSearchReverse()
        {
            return searchReverse;
        }

        internal bool getSearchWrapAround()
        {
            return searchWrapAround;
        }

        internal bool goToLineFromPrompt(string ln)
        {
            int max = editor.Lines.Length < 1 ? 1 : editor.Lines.Length;
            if (ln != null && ln != "" && long.TryParse(ln, out long lnNum) && lnNum >= 1 && lnNum <= max && setLineNumber((int)lnNum))
            {
                return true;
            }
            MessageBox.Show("The line number is beyond the total number of lines", programName + " - Goto Line", MessageBoxButtons.OK);
            return false;
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

        internal int replaceFromPrompt(string query, string replaceString, bool matchCase, bool wrapAround)
        {
            setSearchQuery(query).setReplaceString(replaceString).setSearchMatchCase(matchCase).setSearchWrapAround(wrapAround);
            if (editor.SelectedText.Equals(query, getComparisonType(matchCase)))
            {
                editor.SelectedText = replaceString;
            }
            return findFromPrompt(query, false, matchCase, wrapAround);
        }

        internal BortForm setReplaceString(string replaceString)
        {
            searchReplaceString = replaceString;
            return this;
        }

        internal BortForm setSearchMatchCase(bool matchCase)
        {
            searchMatchCase = matchCase;
            return this;
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

        internal BortForm setSearchWrapAround(bool wrapAround)
        {
            searchWrapAround = wrapAround;
            return this;
        }

        private void aboutNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void BortForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Application.OpenForms.OfType<BortForm>().Any())
            {
                Application.Exit();
            }
        }

        private void BortForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !SaveConfirmPrompt();
        }

        private void BortForm_Shown(object sender, EventArgs e)
        {
            if (filename == null || !OpenDocument(false, filename))
            {
                NewDocument(false);
            }
        }

        private bool canRepeatFind()
        {
            return getSearchQuery().Length > 0 && editor.Text.Length > 0;
        }

        private void cantFind(string text)
        {
            MessageBox.Show("Cannot find \"" + text + "\"", programName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Cut();
        }

        private void darkMode_Click(object sender, EventArgs e)
        {
            darkModeOn = !darkModeOn;
            editor.TextChanged -= editor_TextChanged;
            editor.BackColor = darkModeOn ? SystemColors.WindowText : SystemColors.Window;
            editor.ForeColor = darkModeOn ? SystemColors.Window : SystemColors.WindowText;
            editor.TextChanged += editor_TextChanged;
            darkMode.Text = darkModeOn ? "☀" : "🌙";
            darkMode.BackColor = darkModeOn ? SystemColors.Control : SystemColors.ControlText;
            darkMode.ForeColor = darkModeOn ? SystemColors.ControlText : SystemColors.Control;
            darkMode.Checked = darkModeOn;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SelectedText = "";
        }

        private void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(editor.Text, editor.Font, Brushes.Black, 20, 20);
        }

        private void editor_KeyDown(object sender, KeyEventArgs e)
        {
            UpdatePos();
        }

        private void editor_KeyPress(object sender, KeyPressEventArgs e)
        {
            UpdatePos();
        }

        private void editor_KeyUp(object sender, KeyEventArgs e)
        {
            UpdatePos();
        }

        private void editor_MouseDown(object sender, MouseEventArgs e)
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

        private void editor_MouseUp(object sender, MouseEventArgs e)
        {
            UpdatePos();
        }

        private void editor_TextChanged(object sender, EventArgs e)
        {
            bool hasText = editor.Text.Length > 0;
            findToolStripMenuItem.Enabled = hasText;
            findNextToolStripMenuItem.Enabled = canRepeatFind();
            findPreviousToolStripMenuItem.Enabled = canRepeatFind();
            // replaceToolStripMenuItem.Enabled = hasText;
            // goToToolStripMenuItem.Enabled = hasText;
            // selectAllToolStripMenuItem.Enabled = hasText;
            ChangesMade = true;
            UpdatePos();
            UpdateTitle();
        }

        private void editorContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool isSelected = editor.SelectionLength > 0;
            editorCut.Enabled = isSelected;
            editorCopy.Enabled = isSelected;
            editorDelete.Enabled = isSelected;
            editorSearchWithGoogle.Enabled = isSelected;
            editorPaste.Enabled = Clipboard.GetText().Length > 0;
            editorUndo.Enabled = editor.CanUndo;
            editorRedo.Enabled = editor.CanRedo;
        }

        private void editorCopy_Click(object sender, EventArgs e)
        {
            editor.Copy();
        }

        private void editorCut_Click(object sender, EventArgs e)
        {
            editor.Cut();
        }

        private void editorDelete_Click(object sender, EventArgs e)
        {
            editor.SelectedText = "";
        }

        private void editorPaste_Click(object sender, EventArgs e)
        {
            editor.Paste();
        }

        private void editorRedo_Click(object sender, EventArgs e)
        {
            editor.Redo();
        }

        private void editorRightToLeft_Click(object sender, EventArgs e)
        {
            editor.RightToLeft = editor.RightToLeft == RightToLeft.No ? RightToLeft.Yes : RightToLeft.No;
            editorRightToLeft.Checked = editor.RightToLeft == RightToLeft.Yes;
        }

        private void editorSearchWithGoogle_Click(object sender, EventArgs e)
        {
            searchWeb();
        }

        private void editorSelectAll_Click(object sender, EventArgs e)
        {
            editor.SelectAll();
        }

        private void editorUndo_Click(object sender, EventArgs e)
        {
            editor.Undo();
        }

        private void editToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            searchWithBortToolStripMenuItem.Enabled = true;
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
            redoToolStripMenuItem.Enabled = editor.CanRedo;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fileNotFound()
        {
            MessageBox.Show("The system cannot find the path specified.", programName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void findInEditor()
        {
            if (find != null && find.Visible)
            {
                find.BringToFront();
                return;
            }
            find = new FindPrompt();
            find.Show(this);
        }

        private int findNext()
        {
            return findNextPrev(false);
        }

        private int findNextPrev(bool prev = false)
        {
            int pos = OmniIndexOf(getSearchQuery(), prev, editor.SelectionStart + (prev ? 0 : editor.SelectionLength), getSearchMatchCase());
            if (pos == -1 && getSearchWrapAround())
            {
                pos = OmniIndexOf(getSearchQuery(), prev, prev ? editor.Text.Length - 1 : 0, getSearchMatchCase());
                setStatus(pos == -1 ? "" : "Found next from the " + (prev ? "bottom" : "top"));
            }
            else
            {
                setStatus();
            }
            gotoResult(pos);
            return pos;
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findNext();
        }

        private int findPrev()
        {
            return findNextPrev(true);
        }

        private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findPrev();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findInEditor();
        }

        private void FontDlg_Apply(object sender, EventArgs e)
        {
            editor.Font = fontDlg.Font;
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

        private StringComparison getComparisonType(bool matchCase = false)
        {
            return matchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
        }

        private void GoToLine()
        {
            GoToPrompt gt = new GoToPrompt(getLineNumber());
            gt.ShowDialog(this);
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

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToLine();
        }

        private void NewDocument(bool saveFirst = true)
        {
            if (!saveFirst || SaveConfirmPrompt(false))
            {
                editor.Clear();
                ChangesMade = false;
                isFile = false;
                filename = defaultFilename;
                UpdateTitle();
                editor.Select();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void newWindow()
        {
            Process.Start(Application.ExecutablePath);
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newWindow();
        }

        private int OmniIndexOf(string text, bool reverse = false, int start = 0, bool matchCase = false)
        {
            StringComparison sc = getComparisonType(matchCase);
            return reverse ? editor.Text.LastIndexOf(text, start, sc) : editor.Text.IndexOf(text, start, sc);
        }

        private bool OpenDocument(bool saveFirst = true, string openFilename = null)
        {
            if (!saveFirst || SaveConfirmPrompt(false))
            {
                if (openFilename == null && openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(openFileDialog1.FileName))
                    {
                        fileNotFound();
                        return false;
                    }
                    filename = openFileDialog1.FileName;
                }
                else if (openFilename != null)
                {
                    if (!File.Exists(openFilename))
                    {
                        fileNotFound();
                        return false;
                    }
                    filename = openFilename;
                }
                else
                {
                    return false;
                }
                try
                {
                    editor.Clear();
                    editor.Text = File.ReadAllText(filename, encodingSetting);
                    ChangesMade = false;
                    isFile = true;
                    UpdateTitle();
                    editor.Select();
                    return true;
                }
                catch (FileNotFoundException)
                {
                    fileNotFound();
                    return false;
                }
                catch (IOException e)
                {
                    MessageBox.Show(e.Message, programName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void PageSetup()
        {
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetup();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Paste();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Redo();
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

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replaceInEditor();
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.ZoomFactor = 1.0f;
            UpdateZoom();
        }

        private void satusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleStatusBar();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument(true);
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void searchWeb()
        {
            if (editor.SelectionLength > 0)
            {
                string searchString = editor.SelectedText;
                Process.Start("https://google.com/search?q=" + HttpUtility.UrlEncode(editor.SelectedText));
            }
        }

        private void searchWithBortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchWeb();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SelectAll();
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://stevenwilson.ca/contact");
        }

        private bool setLineNumber(int lnNum)
        {
            try
            {
                editor.Select(editor.GetFirstCharIndexFromLine(lnNum - 1), 0);
                editor.Focus();
                editor.ScrollToCaret();
                UpdatePos();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private BortForm setStatus(string text = "")
        {
            statusBarLeft.Text = text;
            return this;
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            editor.SelectedText = now.ToString("g");
        }

        private void ToggleStatusBar()
        {
            statusBar.Visible = !statusBar.Visible;
            statusBarToolStripMenuItem.Checked = statusBar.Visible;
            UpdatePos();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Undo();
        }

        private void UpdatePos()
        {
            if (statusBar.Visible)
            {
                int[] pos = getLnCol();
                position.Text = "Ln " + pos[0] + ", Col " + pos[1];
            }
        }

        private void UpdateTitle()
        {
            this.Text = (ChangesMade ? "*" : "") + filename + " - " + programName;
        }

        private void UpdateZoom()
        {
            zoomLevel.Text = Math.Round(editor.ZoomFactor * 100).ToString() + "%";
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://stevenwilson.ca/bortpad/help");
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.WordWrap = !editor.WordWrap;
            wordWrapToolStripMenuItem.Checked = editor.WordWrap;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editor.ZoomFactor < 5)
            {
                editor.ZoomFactor = (float)(Math.Round(editor.ZoomFactor + 0.1, 1));
            }
            UpdateZoom();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editor.ZoomFactor >= 0.2)
            {
                editor.ZoomFactor = (float)(Math.Round(editor.ZoomFactor - 0.1, 1));
            }
            UpdateZoom();
        }
    }
}