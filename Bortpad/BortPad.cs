using ScintillaNET;
using SharpConfig;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UtfUnknown;

namespace Bortpad
{
    public partial class BortForm : Form
    {
        // UTF-8
        internal const int _DEFAULT_CODEPAGE = 65001;

        internal const string _DEFAULT_CONFIG_FILE = "Bortpad.cfg";
        internal const string _DEFAULT_FILENAME = "Untitled";
        internal const string _DEFAULT_FONT = "Consolas, 11.25pt";
        internal const string _DEFAULT_SECTION = "General";
        private bool _darkMode;
        private Encoding _encodingSetting;
        private string _filename;
        private FindPrompt _find;
        private int _ln, _col;
        private ReplacePrompt _replace;

        public BortForm(string filenameSpecified = null)
        {
            InitializeComponent();

            FileName = filenameSpecified;

            ConfigFile = ProgramName + ".cfg";
            EncodingSetting = Encoding.GetEncoding(GetSetting<int>("DefaultEncoding")); // Default for new files
            fontDlg.Font = editor.Font = GetSetting<Font>("Font");
            editor.WrapMode = GetSetting<bool>("WordWrap") ? WrapMode.Word : WrapMode.None;
            statusBar.Visible = GetSetting<bool>("StatusBar");
            DarkMode = GetSetting<bool>("DarkMode");
            editor.ViewEol = showLineEndings.Checked = GetSetting<bool>("ShowLineEndings");
            windowsLineFeed.Tag = Eol.CrLf;
            linuxLineFeed.Tag = Eol.Lf;
            macLineFeed.Tag = Eol.Cr;

            Text = _DEFAULT_FILENAME + " - " + ProgramName;
        }

        public EncodingInfo[] CodePages
        {
            get;
        } = Encoding.GetEncodings();

        public int Col
        {
            get
            {
                return _col + 1;
            }
            private set
            {
                _col = value;
            }
        }

        public String ConfigFile
        {
            get; private set;
        }

        public bool DarkMode
        {
            get
            {
                return _darkMode;
            }
            set
            {
                if (_darkMode != value)
                {
                    editor.Styles[Style.Default].BackColor = value ? Color.Black : Color.White;
                    editor.Styles[Style.Default].ForeColor = value ? Color.White : Color.Black;
                    editor.StyleClearAll();
                    editor.CaretForeColor = value ? Color.White : Color.Black;
                    editor.CaretLineBackColor = value ? Color.Black : Color.White;
                    editor.CaretLineBackColorAlpha = 256;
                    darkMode.Text = value ? "☀" : "🌙";
                    ColorSwapItem(darkMode, !value);
                    ColorSwap(statusBar, value);

                    darkMode.Checked = value;
                    SetSetting("DarkMode", _darkMode = value);
                }
            }
        }

        public Encoding EncodingSetting
        {
            get
            {
                return _encodingSetting;
            }
            private set
            {
                _encodingSetting = value ?? Encoding.GetEncoding(GetSetting<int>("DefaultEncoding"));
                SetEncodingStatus(_encodingSetting);
            }
        }

        public string FileName
        {
            get { return _filename ?? _DEFAULT_FILENAME; }
            private set
            {
                _filename = value;
            }
        }

        public bool HasText
        {
            get { return editor.TextLength > 0; }
        }

        public bool IsFile
        {
            get
            {
                if (_filename != null && File.Exists(_filename))
                {
                    if (OpenFilesInfo == null)
                    {
                        OpenFilesInfo = new FileInfo(_filename);
                    }
                    return true;
                }
                return false;
            }
        }

        public int Ln
        {
            get
            {
                return _ln + 1;
            }
            private set
            {
                _ln = value;
            }
        }

        public int NumLines
        {
            get { return editor.Lines.Count < 1 ? 1 : editor.Lines.Count; }
        }

        public FileInfo OpenFilesInfo
        {
            get; private set;
        }

        public int Pos
        {
            get;
            private set;
        }

        public string ProgramName
        {
            get;
        } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public Configuration Settings
        {
            get; private set;
        }

        public static void ColorSwap(Control item, bool mode)
        {
            item.BackColor = mode ? SystemColors.ControlText : SystemColors.Control;
            item.ForeColor = mode ? SystemColors.Control : SystemColors.ControlText;
        }

        public static void ColorSwapItem(ToolStripMenuItem item, bool mode)
        {
            item.BackColor = mode ? SystemColors.ControlText : SystemColors.Control;
            item.ForeColor = mode ? SystemColors.Control : SystemColors.ControlText;
        }

        public static StringComparison GetComparisonType(bool matchCase = false)
        {
            return matchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
        }

        public static void NewWindow(string args = null)
        {
            Process.Start(Application.ExecutablePath, args);
        }

        public static int OmniIndexOf(string needle, string haystack, bool reverse = false, int start = 0, bool matchCase = false)
        {
            StringComparison sc = GetComparisonType(matchCase);
            return reverse ? haystack.LastIndexOf(needle, start, sc) : haystack.IndexOf(needle, start, sc);
        }

        internal int FindFromPrompt(string query, bool reverse, bool matchCase, bool wrapAround)
        {
            SetSetting("Search", query, "Find")
                .SetSetting("Up", reverse, "Find")
                .SetSetting("MatchCase", matchCase, "Find")
                .SetSetting("WrapAround", wrapAround, "Find");
            findNextToolStripMenuItem.Enabled = CanRepeatFind();
            findPreviousToolStripMenuItem.Enabled = CanRepeatFind();

            return GetSetting<bool>("Up", "Find") ? FindPrev() : FindNext();
        }

        internal T GetSetting<T>(string key, string section = _DEFAULT_SECTION)
        {
            if (Settings == null || !Settings.Any())
            {
                LoadSettings();
            }
            return (T)Settings[section][key].GetValue<T>();
        }

        internal bool GoToLineFromPrompt(string ln)
        {
            if (ln != null && ln != "" && long.TryParse(ln, out long lnNum) && lnNum >= 1 && lnNum <= NumLines)
            {
                Ln = (int)lnNum;
                return true;
            }
            MessageBox.Show("The line number is beyond the total number of lines", ProgramName + " - Goto Line", MessageBoxButtons.OK);
            return false;
        }

        internal BortForm LoadSettings()
        {
            if (File.Exists(ConfigFile))
            {
                Settings = Configuration.LoadFromFile(ConfigFile);
            }
            else
            {
                Settings = new Configuration();
                SetSetting("DefaultEncoding", _DEFAULT_CODEPAGE, save: false);
                SetSetting("Font", _DEFAULT_FONT, save: false);
                SetSetting("StatusBar", true, save: false);
                SetSetting("WordWrap", false, save: false);
                SetSetting("DarkMode", false, save: false);
                SetSetting("ShowLineEndings", false, save: false);
                SetSetting("Search", "", "Find", false);
                SetSetting("Replace", "", "Find", false);
                SetSetting("Up", false, "Find", false);
                SetSetting("MatchCase", false, "Find", false);
                SetSetting("WrapAround", false, "Find", false);
                SaveSettings();
            }
            return this;
        }

        internal void OpenAsNew()
        {
            FileName = null;
            OpenFilesInfo = null;
            // editor.ReadOnly = false; // TODO: if Undo is called after this, it will revert to orig text ok, but mark modified = false (want it dirty tho)
        }

        internal void ReplaceAll(string query, string replaceString, bool matchCase, bool wrapAround)
        {
            SetSetting("Search", query, "Find")
                .SetSetting("Replace", replaceString, "Find")
                .SetSetting("MatchCase", matchCase, "Find")
                .SetSetting("WrapAround", wrapAround, "Find");
            string input = string.Format(@"({0})", System.Text.RegularExpressions.Regex.Escape(query));
            string pattern = string.Format("{0}", System.Text.RegularExpressions.Regex.Escape(replaceString));
            editor.Text = System.Text.RegularExpressions.Regex.Replace(
                editor.Text,
                input,
                pattern,
                matchCase ? System.Text.RegularExpressions.RegexOptions.None : System.Text.RegularExpressions.RegexOptions.IgnoreCase
                );
        }

        internal int ReplaceFromPrompt(string query, string replaceString, bool matchCase, bool wrapAround)
        {
            SetSetting("Search", query, "Find")
                .SetSetting("Replace", replaceString, "Find")
                .SetSetting("MatchCase", matchCase, "Find")
                .SetSetting("WrapAround", wrapAround, "Find");
            if (editor.SelectedText.Equals(query, GetComparisonType(matchCase)))
            {
                editor.ReplaceSelection(replaceString);
            }
            return FindFromPrompt(query, false, matchCase, wrapAround);
        }

        internal BortForm SaveSettings()
        {
            if (!File.Exists(ConfigFile))
            {
                return LoadSettings();
            }
            Settings.SaveToFile(ConfigFile);
            return this;
        }

        internal BortForm SetSetting<T>(string key, T value, string section = _DEFAULT_SECTION, bool save = true)
        {
            Settings[section][key].SetValue(value);
            return save ? SaveSettings() : this;
        }

        internal bool ToggleSetting(string key)
        {
            SetSetting(key, !GetSetting<bool>(key));
            return GetSetting<bool>(key);
        }

        internal void UnsetReadonly()
        {
            OpenFilesInfo.IsReadOnly = false;
            editor.ReadOnly = OpenFilesInfo.IsReadOnly;
        }

        private void About(object sender, EventArgs e)
        {
            About about = new();
            about.ShowDialog();
        }

        private void BortForm_DragDrop(object sender, DragEventArgs e)
        {
            holdShiftNotice.Visible = false;
            holdShiftNotice.Image = Properties.Resources.shift;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                bool first = true;
                foreach (string file in files)
                {
                    if (first && (e.KeyState & 4) != 4)
                    {
                        OpenDocument(true, file);
                        first = false;
                        continue;
                    }
                    NewWindow(file);
                }
            }
        }

        private void BortForm_DragLeave(object sender, EventArgs e)
        {
            holdShiftNotice.Visible = false;
            holdShiftNotice.Image = Properties.Resources.shift;
        }

        private void BortForm_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                holdShiftNotice.Visible = true;
                if ((e.KeyState & 4) != 4)
                {
                    e.Effect = DragDropEffects.Copy;
                    holdShiftNotice.Image = Properties.Resources.shift;
                    return;
                }
                e.Effect = DragDropEffects.Move;
                holdShiftNotice.Image = Properties.Resources.check;
            }
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
            if (_filename == null || !OpenDocument(false, _filename))
            {
                NewDocument(false);
            }
        }

        private bool CanRepeatFind()
        {
            return GetSetting<string>("Search", "Find").Length > 0 && HasText;
        }

        private void CantFind(string text)
        {
            MessageBox.Show("Cannot find \"" + text + "\"", ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool isSelected = editor.SelectionLength > 0;
            editorCut.Enabled = isSelected;
            editorCopy.Enabled = isSelected;
            editorDelete.Enabled = isSelected;
            editorSearchWithGoogle.Enabled = isSelected;
            editorPaste.Enabled = editor.CanPaste;
            editorUndo.Enabled = editor.CanUndo;
            editorRedo.Enabled = editor.CanRedo;
        }

        private void Copy(object sender, EventArgs e)
        {
            editor.Copy();
        }

        private void CursorPositionChanged(object sender, EventArgs e)
        {
            Ln = editor.CurrentLine;
            Col = editor.GetColumn(editor.CurrentPosition);
            Pos = editor.CurrentPosition;
            UpdatePos();
        }

        private void Cut(object sender, EventArgs e)
        {
            editor.Cut();
        }

        private void Delete(object sender, EventArgs e)
        {
            editor.Clear();
        }

        private void Edit_DropDownClosed(object sender, EventArgs e)
        {
            searchWithGoogleToolStripMenuItem.Enabled = true;
        }

        private void Edit_DropDownOpening(object sender, EventArgs e)
        {
            bool isSelected = editor.SelectionLength > 0;
            cutToolStripMenuItem.Enabled = isSelected;
            copyToolStripMenuItem.Enabled = isSelected;
            deleteToolStripMenuItem.Enabled = isSelected;
            searchWithGoogleToolStripMenuItem.Enabled = isSelected;
            pasteToolStripMenuItem.Enabled = editor.CanPaste;
            undoToolStripMenuItem.Enabled = editor.CanUndo;
            redoToolStripMenuItem.Enabled = editor.CanRedo;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FileNotFound()
        {
            MessageBox.Show("The system cannot find the path specified.", ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Find(object sender, EventArgs e)
        {
            if (_find != null && _find.Visible)
            {
                _find.BringToFront();
                return;
            }
            _find = new FindPrompt(
                GetSetting<string>("Search", "Find"),
                GetSetting<bool>("Up", "Find"),
                GetSetting<bool>("MatchCase", "Find"),
                GetSetting<bool>("WrapAround", "Find"));
            _find.Show(this);
        }

        private int FindNext()
        {
            return FindNextPrev(false);
        }

        private void FindNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private int FindNextPrev(bool prev = false)
        {
            string searchQuery = GetSetting<string>("Search", "Find");
            bool searchMatchCase = GetSetting<bool>("MatchCase", "Find");
            int pos = OmniIndexOf(searchQuery, editor.Text, prev, editor.SelectionStart + (prev ? 0 : editor.SelectionLength), searchMatchCase);
            if (pos == -1 && GetSetting<bool>("WrapAround", "Find"))
            {
                pos = OmniIndexOf(searchQuery, editor.Text, prev, prev ? editor.Text.Length - 1 : 0, searchMatchCase);
                SetStatus(pos == -1 ? "" : "Found next from the " + (prev ? "bottom" : "top"));
            }
            else
            {
                SetStatus();
            }
            HighlightResult(pos, searchQuery);
            return pos;
        }

        private int FindPrev()
        {
            return FindNextPrev(true);
        }

        private void FindPrevious_Click(object sender, EventArgs e)
        {
            FindPrev();
        }

        private void Format_DropDownOpening(object sender, EventArgs e)
        {
            wordWrapToolStripMenuItem.Checked = editor.WrapMode == WrapMode.Word;
        }

        private void GoToLine(object sender, EventArgs e)
        {
            GoToPrompt gt = new GoToPrompt(Ln);
            gt.ShowDialog(this);
        }

        private void HighlightResult(int pos, string q)
        {
            if (pos != -1)
            {
                editor.SelectionStart = pos;
                editor.SetSelection(pos, pos + q.Length);
                editor.ScrollCaret();
                UpdatePos();
            }
            else
            {
                CantFind(q);
            }
        }

        private void Modified(object sender, EventArgs e)
        {
            findToolStripMenuItem.Enabled = HasText;
            findNextToolStripMenuItem.Enabled = CanRepeatFind();
            findPreviousToolStripMenuItem.Enabled = CanRepeatFind();
            // replaceToolStripMenuItem.Enabled = HasText;
            // goToToolStripMenuItem.Enabled = HasText;
            // selectAllToolStripMenuItem.Enabled = HasText;
        }

        private void ModifyAttempt(object sender, EventArgs e)
        {
            /*
            if (FileName == null && OpenFilesInfo == null)
            {
                editor.ReadOnly = false;
                return;
            }
            */
            ReadonlyPrompt ro = new();
            if (ro.ShowDialog(this) == DialogResult.OK)
            {
                editor.ReadOnly = false;
                // encodingStatus.Enabled = true;
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void NewDocument(bool saveFirst = true)
        {
            if (!saveFirst || SaveConfirmPrompt(false))
            {
                editor.ReadOnly = false;
                editor.ClearAll();
                editor.EmptyUndoBuffer();

                FileName = null;
                OpenFilesInfo = null;
                EncodingSetting = null;
                editor.ReadOnly = false;
                editor.SetSavePoint();
                editor.Select();
            }
        }

        private void NewWindow_Click(object sender, EventArgs e)
        {
            NewWindow();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private bool OpenDocument(bool saveFirst = true, string openFilename = null)
        {
            if (!saveFirst || SaveConfirmPrompt(false))
            {
                try
                {
                    if (openFilename == null)
                    {
                        // No filename as param, get filename from Open Dialog
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            openFilename = openFileDialog1.FileName;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (File.Exists(openFilename))
                    {
                        OpenFilesInfo = new FileInfo(openFilename);
                        DetectionDetail getEncoding = CharsetDetector.DetectFromFile(openFilename).Detected;
                        EncodingSetting = getEncoding?.Encoding;
                        editor.ReadOnly = false;
                        editor.Clear();
                        editor.Text = File.ReadAllText(openFilename, EncodingSetting);
                        FileName = openFilename;
                        SetEncodingStatus(EncodingSetting, true, getEncoding != null ? getEncoding.Confidence : 0);
                        editor.EmptyUndoBuffer();
                        editor.ReadOnly = OpenFilesInfo.IsReadOnly;
                        editor.SetSavePoint();
                        editor.Select();
                        return true;
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                catch (FileNotFoundException)
                {
                    FileNotFound();
                    return false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            // User clicked Cancel on SaveConfirmPrompt
            return false;
        }

        private void PageSetup(object sender, EventArgs e)
        {
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                // TODO
            }
        }

        private void Paste(object sender, EventArgs e)
        {
            editor.Paste();
        }

        private void Print(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(editor.Text, editor.Font, Brushes.Black, 20, 20);
        }

        private void Redo(object sender, EventArgs e)
        {
            editor.Redo();
        }

        private void Replace_Click(object sender, EventArgs e)
        {
            ReplaceReplace();
        }

        private void ReplaceReplace()
        {
            if (_replace != null && _replace.Visible)
            {
                _replace.BringToFront();
                return;
            }
            _replace = new ReplacePrompt(GetSetting<string>("Search", "Find"), GetSetting<string>("Replace", "Find"), GetSetting<bool>("MatchCase", "Find"), GetSetting<bool>("WrapAround", "Find"));
            _replace.Show(this);
        }

        private void RestoreDefaultZoom_Click(object sender, EventArgs e)
        {
            editor.Zoom = 0;
        }

        private void RightToLeft_Click(object sender, EventArgs e)
        {
            // editor.RightToLeft = editor.RightToLeft == RightToLeft.No ? RightToLeft.Yes : RightToLeft.No;
            // editorRightToLeft.Checked = editor.RightToLeft == RightToLeft.Yes;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (IsFile && OpenFilesInfo.IsReadOnly)
            {
                SaveConfirmPrompt(true);
                return;
            }
            SaveDocument();
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            SaveDocument(true);
        }

        private bool SaveConfirmPrompt(bool force = false)
        {
            if (force || editor.Modified)
            {
                if (IsFile && OpenFilesInfo.IsReadOnly)
                {
                    ReadonlyDecision rod = new();
                    rod.ShowDialog();
                    switch (rod.DialogResult)
                    {
                        case DialogResult.Yes:
                            OpenFilesInfo.IsReadOnly = false;
                            return SaveDocument();

                        case DialogResult.Continue:
                            return SaveDocument(true);

                        case DialogResult.No:
                            return true;

                        default:
                            return false;
                    }
                }
                SaveConfirmPrompt scp = new(FileName);
                scp.ShowDialog();
                return scp.DialogResult switch
                {
                    DialogResult.Yes => SaveDocument(), // Save/Show Save Dialog
                    DialogResult.No => true,
                    _ => false,
                };
            }
            // Return: false = cancel open dialog/close app action
            // true = we're good to proceed and launch open dialog/close app/etc
            return true;
        }

        private bool SaveDocument(bool saveAs = false)
        {
            if (IsFile && !saveAs)
            {
                File.WriteAllText(FileName, editor.Text, EncodingSetting);
                editor.SetSavePoint();
                return true;
            }
            else
            {
                saveFileDialog1.FileName = Path.GetFileName(FileName);
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(FileName);
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog1.FileName;
                    OpenFilesInfo = new FileInfo(FileName);
                    File.WriteAllText(FileName, editor.Text, EncodingSetting);
                    editor.SetSavePoint();
                    return true;
                }
            }
            return false;
        }

        private void SavePointLeft(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void SavePointReached(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void SearchWeb(object sender, EventArgs e)
        {
            if (editor.SelectionLength > 0)
            {
                Process.Start("https://google.com/search?q=" + WebUtility.UrlEncode(editor.SelectedText));
            }
        }

        private void SelectAll(object sender, EventArgs e)
        {
            editor.SelectAll();
        }

        private void SendFeedback_Click(object sender, EventArgs e)
        {
            Process.Start("https://stevenwilson.ca/contact");
        }

        private BortForm SetEncodingStatus(Encoding encoding, bool detected = false, float confidence = 1)
        {
            encodingStatus.DropDownItems.Clear();
            encodingStatus.Text = encoding.EncodingName;
            encodingStatus.ToolTipText = detected ? "Confidence: " + (confidence * 100) + "%" : "";
            foreach (EncodingInfo codePage in CodePages)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                bool current = Equals(codePage.GetEncoding(), EncodingSetting);
                item.Name = codePage.Name;
                item.Text = codePage.DisplayName;
                // if (current && detected) { item.ShortcutKeyDisplayString = " (Detected)"; }
                item.Tag = codePage;
                item.Checked = current;
                item.Click += new EventHandler(SetEncoding);
                encodingStatus.DropDownItems.Add(item);
            }
            return this;
        }

        private void SetEncoding(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            EncodingSetting = ((EncodingInfo)clickedItem.Tag).GetEncoding();
            SetEncodingStatus(EncodingSetting, false);
        }

        private void SetFont(object sender, EventArgs e)
        {
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                SetSetting("Font", fontDlg.Font);
                editor.Font = fontDlg.Font;
                editor.StyleClearAll();
            }
        }

        private BortForm SetStatus(string text = "")
        {
            statusBarLeft.Text = text;
            return this;
        }

        private void StatusBar_Click(object sender, EventArgs e)
        {
            ToggleStatusBar();
        }

        private void TimeDate_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            editor.ReplaceSelection(now.ToString("g"));
        }

        private void ToggleDarkMode(object sender, EventArgs e)
        {
            DarkMode = !DarkMode;
        }

        private void ToggleStatusBar()
        {
            statusBarToolStripMenuItem.Checked = statusBar.Visible = ToggleSetting("StatusBar");
            UpdatePos();
        }

        private void Undo(object sender, EventArgs e)
        {
            editor.Undo();
        }

        private void UpdatePos()
        {
            if (statusBar.Visible)
            {
                position.Text = "Ln " + Ln + ", Col " + Col;
            }
        }

        private void UpdateTitle()
        {
            Text = (editor.Modified ? "*" : "") + FileName + (IsFile && OpenFilesInfo.IsReadOnly ? " [Read-Only]" : "") + " - " + ProgramName;
        }

        private void View_DropDownOpening(object sender, EventArgs e)
        {
            statusBarToolStripMenuItem.Checked = statusBar.Visible;
        }

        private void ViewHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://stevenwilson.ca/bortpad/help");
        }

        private void WordWrap_Click(object sender, EventArgs e)
        {
            editor.WrapMode = (wordWrapToolStripMenuItem.Checked = ToggleSetting("WordWrap")) ? WrapMode.Word : WrapMode.None;
            UpdatePos();
        }

        private void Zoom_Changed(object sender, EventArgs e)
        {
            zoomLevel.Text = (100 + (editor.Zoom * 10)) + "%";
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            if (editor.Zoom < 50)
            {
                editor.ZoomIn();
            }
        }

        private void SetEOL(object sender, EventArgs e)
        {
            windowsLineFeed.Checked = false;
            linuxLineFeed.Checked = false;
            macLineFeed.Checked = false;
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            Eol eol = (Eol)clickedItem.Tag;
            editor.EolMode = eol;
            lineReturnType.Text = Regex.Replace(clickedItem.Text, "&(.)", "$1");
            clickedItem.Checked = true;
        }

        private void ShowLineEndings_Click(object sender, EventArgs e)
        {
            editor.ViewEol = !editor.ViewEol;
            showLineEndings.Checked = editor.ViewEol;
            SetSetting("ShowLineEndings", editor.ViewEol);
        }

        private void ConvertLineEndings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Convert all End of Line characters in this document to " + lineReturnType.Text + "?", ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                editor.ConvertEols(editor.EolMode);
            }
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            if (editor.Zoom > -9)
            {
                editor.ZoomOut();
            }
        }
    }
}