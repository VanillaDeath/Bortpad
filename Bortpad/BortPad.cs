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
        #region _CONSTANTS

        internal const Eol _CR = Eol.Cr;
        internal const Eol _CRLF = Eol.CrLf;
        internal const int _DEFAULT_CODEPAGE = 65001;
        internal const string _DEFAULT_CONFIG_FILE = "Bortpad.cfg";
        internal const Eol _DEFAULT_EOL = _CRLF;
        internal const string _DEFAULT_FILENAME = "Untitled";
        internal const string _DEFAULT_FONT = "Consolas, 11.25pt";
        internal const string _DEFAULT_SECTION = "General";
        internal const Eol _LF = Eol.Lf;

        #endregion _CONSTANTS

        #region _fields

        private bool _darkMode;
        private Encoding _encodingSetting;
        private Eol _eolSetting;
        private string _filename;
        private FindPrompt _find;
        private ReplacePrompt _replace;

        #endregion _fields

        #region Properties

        public EncodingInfo[] CodePages
        {
            get;
        } = Encoding
            .GetEncodings()
            .OrderBy(e => e.DisplayName)
            .ThenBy(e => e.Name)
            .ToArray();

        public string ConfigFile
        {
            get; private set;
        }

        public bool DarkMode
        {
            get => _darkMode;
            set
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
                readOnlyNotice.LinkColor = value ? SystemColors.Control : SystemColors.ControlText;
                readOnlyNotice.Image = value ? Properties.Resources.readonlyw : Properties.Resources._readonly;

                darkMode.Checked = value;
                SetSetting("DarkMode", _darkMode = value);
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

        public Eol EolSetting
        {
            get
            {
                return _eolSetting;
            }
            private set
            {
                _eolSetting = value;
                editor.EolMode = _eolSetting;
                SetSetting("LineEnding", _eolSetting);
                SetEolStatus(_eolSetting);
            }
        }

        public string FileName
        {
            get => _filename ?? _DEFAULT_FILENAME;
            private set => _filename = value;
        }

        public bool IsFile => _filename != null && File.Exists(_filename);

        public bool IsReadOnly
        {
            get => IsFile && OpenFilesInfo.IsReadOnly;
            private set
            {
                OpenFilesInfo.IsReadOnly = value;
                readOnlyNotice.Visible = OpenFilesInfo.IsReadOnly;
            }
        }

        public string MainStatus
        {
            get => statusBarLeft.Text;
            private set => statusBarLeft.Text = value ?? "";
        }

        public FileInfo OpenFilesInfo => IsFile ? new(_filename) : null;

        public string ProgramName
        {
            get;
        } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public Configuration Settings
        {
            get; private set;
        }

        public bool StatusBar
        {
            get => statusBar.Visible;
            private set
            {
                SetSetting("StatusBar", statusBar.Visible = value);
                viewStatusBar.Checked = statusBar.Visible;
                if (value) { UpdatePos(); }
            }
        }

        #endregion Properties

        #region Constructors

        public BortForm(string filenameSpecified = null)
        {
            InitializeComponent();

            FileName = filenameSpecified;

            ConfigFile = ProgramName + ".cfg";
            EncodingSetting = Encoding.GetEncoding(GetSetting<int>("DefaultEncoding")); // Default for new files
            editor.Font = GetSetting<Font>("Font");
            editor.WrapMode = GetSetting<bool>("WordWrap") ? WrapMode.Word : WrapMode.None;
            StatusBar = GetSetting<bool>("StatusBar");
            DarkMode = GetSetting<bool>("DarkMode");
            windowsLineFeed.Tag = _CRLF;
            linuxLineFeed.Tag = _LF;
            macLineFeed.Tag = _CR;
            editor.ViewEol = GetSetting<bool>("ShowLineEndings");
            EolSetting = GetSetting<Eol>("LineEnding");

            Text = _DEFAULT_FILENAME + " - " + ProgramName;
        }

        #endregion Constructors

        #region Static Methods

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

        #endregion Static Methods

        #region Instance Methods: Settings

        private T GetSetting<T>(string key, string section = _DEFAULT_SECTION)
        {
            if (Settings == null || !Settings.Any())
            {
                LoadSettings();
            }
            return (T)Settings[section][key].GetValue<T>();
        }

        private bool LoadSettings()
        {
            if (File.Exists(ConfigFile))
            {
                Settings = Configuration.LoadFromFile(ConfigFile);
                return true;
            }
            Settings = new Configuration();
            SetSetting("DefaultEncoding", _DEFAULT_CODEPAGE, save: false);
            SetSetting("Font", _DEFAULT_FONT, save: false);
            SetSetting("StatusBar", true, save: false);
            SetSetting("WordWrap", false, save: false);
            SetSetting("DarkMode", false, save: false);
            SetSetting("LineEnding", _DEFAULT_EOL, save: false);
            SetSetting("ShowLineEndings", false, save: false);
            SetSetting("Search", "", "Find", false);
            SetSetting("Replace", "", "Find", false);
            SetSetting("Up", false, "Find", false);
            SetSetting("MatchCase", false, "Find", false);
            SetSetting("WrapAround", false, "Find", false);
            SaveSettings();
            return false;
        }

        private void SaveSettings()
        {
            Settings.SaveToFile(ConfigFile);
        }

        private void SetSetting<T>(string key, T value, string section = _DEFAULT_SECTION, bool save = true)
        {
            Settings[section][key].SetValue(value);
            if (save)
            {
                SaveSettings();
            }
        }

        private bool ToggleSetting(string key)
        {
            SetSetting(key, !GetSetting<bool>(key));
            return GetSetting<bool>(key);
        }

        #endregion Instance Methods: Settings

        #region Instance Methods: Document

        private void FileNotFound()
        {
            MessageBox.Show("The system cannot find the path specified.", ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private bool NewDocument(bool saveFirst = true)
        {
            if (!saveFirst || SaveConfirmPrompt(false))
            {
                editor.ReadOnly = false;
                editor.ClearAll();
                editor.EmptyUndoBuffer();

                FileName = null;
                // OpenFilesInfo = null;
                EncodingSetting = null;
                editor.ReadOnly = false;
                readOnlyNotice.Visible = false;
                editor.SetSavePoint();
                editor.Select();
                return true;
            }
            return false;
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
                        // OpenFilesInfo = new FileInfo(openFilename);
                        DetectionDetail getEncoding = CharsetDetector.DetectFromFile(openFilename).Detected;
                        EncodingSetting = getEncoding?.Encoding;
                        editor.ReadOnly = false;
                        readOnlyNotice.Visible = false;
                        editor.Clear();
                        editor.Text = File.ReadAllText(openFilename, EncodingSetting);
                        FileName = openFilename;
                        SetEncodingStatus(EncodingSetting, true, getEncoding != null ? getEncoding.Confidence : 0);
                        editor.EmptyUndoBuffer();
                        editor.ReadOnly = IsReadOnly;
                        readOnlyNotice.Visible = editor.ReadOnly;
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

        private void PageSetup()
        {
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                // TODO
            }
        }

        private void Print()
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private bool SaveConfirmPrompt(bool force = false)
        {
            if (force || editor.Modified)
            {
                if (IsReadOnly)
                {
                    if (!readOnlyNotice.Visible)
                    {
                        readOnlyNotice.Visible = true;
                    }
                    ReadonlyDecision rod = new();
                    rod.ShowDialog();
                    switch (rod.DialogResult)
                    {
                        case DialogResult.Yes:
                            IsReadOnly = false;
                            editor.ReadOnly = IsReadOnly;
                            readOnlyNotice.Visible = editor.ReadOnly;
                            return SaveDocument();

                        case DialogResult.OK:
                            if (SaveDocument(true))
                            {
                                editor.ReadOnly = IsReadOnly;
                                readOnlyNotice.Visible = editor.ReadOnly;
                                return true;
                            }
                            return false;

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
                try
                {
                    if (!IsReadOnly)
                    {
                        File.WriteAllText(FileName, editor.Text, EncodingSetting);
                        editor.SetSavePoint();
                        return true;
                    }
                    return SaveConfirmPrompt(true);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                saveFileDialog1.FileName = Path.GetFileName(FileName);
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(FileName);
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog1.FileName;
                    // OpenFilesInfo = new FileInfo(FileName);
                    File.WriteAllText(FileName, editor.Text, EncodingSetting);
                    editor.SetSavePoint();
                    return true;
                }
            }
            return false;
        }

        private void SetFont()
        {
            fontDlg.Font = editor.Font;
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                SetSetting("Font", editor.Font = fontDlg.Font);
                editor.StyleClearAll();
            }
        }

        #endregion Instance Methods: Document

        #region Instance Methods: Find & Replace

        private bool CanRepeatFind()
        {
            return GetSetting<string>("Search", "Find").Length > 0 && editor.HasText;
        }

        private void Find()
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
            _find.FindClick += FindFromPrompt;
            _find.Show();
        }

        private void FindFromPrompt(object sender, EventArgs args)
        {
            FindPrompt thisPrompt = (FindPrompt)sender;
            SetSetting("Search", thisPrompt.SearchQuery, "Find");
            SetSetting("Up", thisPrompt.Up, "Find");
            SetSetting("MatchCase", thisPrompt.MatchCase, "Find");
            SetSetting("WrapAround", thisPrompt.WrapAround, "Find");
            editFindNext.Enabled = CanRepeatFind();
            editFindPrevious.Enabled = editFindNext.Enabled;

            FindNextPrev(thisPrompt.Up);
        }

        private void FindNext(object sender, EventArgs args)
        {
            FindNextPrev(false);
        }

        private void FindNextPrev(bool prev = false)
        {
            string searchQuery = GetSetting<string>("Search", "Find");
            bool searchMatchCase = GetSetting<bool>("MatchCase", "Find");
            int pos = OmniIndexOf(searchQuery, editor.Text, prev, editor.SelectionStart + (prev ? 0 : editor.SelectionLength), searchMatchCase);
            if (pos == -1 && GetSetting<bool>("WrapAround", "Find"))
            {
                pos = OmniIndexOf(searchQuery, editor.Text, prev, prev ? editor.Text.Length - 1 : 0, searchMatchCase);
                MainStatus = pos == -1 ? "" : "Found next from the " + (prev ? "bottom" : "top");
            }
            else
            {
                MainStatus = "";
            }
            HighlightResult(pos, searchQuery);
        }

        private void FindPrev(object sender, EventArgs args)
        {
            FindNextPrev(true);
        }

        private void GoToLineFromPrompt(object sender, EventArgs args)
        {
            long ln = ((GoToPrompt)sender).LineNumber;
            if (ln >= 1 && ln <= editor.NumLines)
            {
                editor.Ln = (int)ln;
                ((GoToPrompt)sender).Close();
                return;
            }
            MessageBox.Show("The line number is beyond the total number of lines", ProgramName + " - Goto Line", MessageBoxButtons.OK);
        }

        private void HighlightResult(int pos, string q)
        {
            if (pos != -1)
            {
                editor.Pos = pos;
                editor.SetSelection(pos, pos + q.Length);
                editor.ScrollCaret();
                UpdatePos();
                return;
            }
            _ = MessageBox.Show("Cannot find \"" + q + "\"", ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ReplaceAll(object sender, EventArgs args)
        {
            ReplaceFromPrompt(sender, args, true);
        }

        private void ReplaceFromPrompt(object sender, EventArgs args, bool replaceAll = false)
        {
            ReplacePrompt replacePrompt = (ReplacePrompt)sender;
            string query = replacePrompt.SearchQuery;
            string replaceString = replacePrompt.ReplaceWith;
            bool matchCase = replacePrompt.MatchCase;
            bool wrapAround = replacePrompt.WrapAround;
            SetSetting("Search", query, "Find");
            SetSetting("Replace", replaceString, "Find");
            SetSetting("MatchCase", matchCase, "Find");
            SetSetting("WrapAround", wrapAround, "Find");

            if (replaceAll)
            {
                string input = string.Format(@"({0})", Regex.Escape(query));
                string pattern = string.Format("{0}", Regex.Escape(replaceString));
                editor.Text = Regex.Replace(
                    editor.Text,
                    input,
                    pattern,
                    matchCase ? RegexOptions.None : RegexOptions.IgnoreCase
                    );
                return;
            }

            if (editor.SelectedText.Equals(query, GetComparisonType(matchCase)))
            {
                editor.ReplaceSelection(replaceString);
            }
            FindNext(sender, args);
        }

        private void ReplaceOne(object sender, EventArgs args)
        {
            ReplaceFromPrompt(sender, args, false);
        }

        private void ReplacePrompt()
        {
            if (_replace != null && _replace.Visible)
            {
                _replace.BringToFront();
                return;
            }
            _replace = new ReplacePrompt(
                GetSetting<string>("Search", "Find"),
                GetSetting<string>("Replace", "Find"),
                GetSetting<bool>("MatchCase", "Find"),
                GetSetting<bool>("WrapAround", "Find"));
            _replace.FindClick += FindNext;
            _replace.ReplaceClick += ReplaceOne;
            _replace.ReplaceAllClick += ReplaceAll;
            _replace.Show();
        }

        #endregion Instance Methods: Find & Replace

        #region Instance Methods: Status Bar

        private void SetEncodingStatus(Encoding encoding, bool detected = false, float confidence = 1)
        {
            encodingStatus.DropDownItems.Clear();
            encodingStatus.Text = encoding.EncodingName;
            encodingStatus.ToolTipText = detected ? "Confidence: " + (confidence * 100) + "%" : "";

            ToolStripMenuItem[] items = CodePages.Select(e =>
                {
                    bool defaultopt = e.CodePage == _DEFAULT_CODEPAGE;
                    bool current = Equals(e.GetEncoding(), encoding);
                    ToolStripMenuItem i = new()
                    {
                        Name = e.Name,
                        Text = e.DisplayName + (detected && current ? " (Detected)" : "") + (defaultopt ? " (Default)" : ""),
                        Tag = e,
                        Checked = current,
                        Font = defaultopt ? new Font(this.Font, FontStyle.Bold) : null,
                    };
                    if (!i.Checked)
                    {
                        i.Click += SetEncoding;
                    }
                    return i;
                }
            )
                .OrderByDescending(i => i.Checked)
                .ThenByDescending(i => ((EncodingInfo)(i.Tag)).CodePage == _DEFAULT_CODEPAGE)
                .ThenBy(i => i.Text)
                .ToArray();
            encodingStatus.DropDownItems.AddRange(items);
            encodingStatus.DropDownItems.Insert(encoding.CodePage == _DEFAULT_CODEPAGE ? 1 : 2, new ToolStripSeparator());
        }

        private void SetEolStatus(Eol eolMode)
        {
            showLineEndings.Checked = editor.ViewEol;
            ToolStripItemCollection items = lineReturnType.DropDownItems;
            foreach (ToolStripMenuItem item in items.OfType<ToolStripMenuItem>())
            {
                if (item.Tag != null && item.Tag.GetType().Equals(typeof(Eol)))
                {
                    if (Equals(item.Tag, eolMode))
                    {
                        item.Checked = true;
                        lineReturnType.Text = Regex.Replace(item.Text, "&(.)", "$1");
                        continue;
                    }
                    item.Checked = false;
                }
            }
        }

        private void UpdatePos()
        {
            if (!StatusBar)
            {
                return;
            }
            position.Text = "Ln " + editor.Ln + ", Col " + editor.Col;
        }

        #endregion Instance Methods: Status Bar

        #region Instance Methods: Events

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

        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool isSelected = editor.SelectionLength > 0;
            editorCut.Enabled = isSelected;
            editorCopy.Enabled = isSelected;
            editorDelete.Enabled = isSelected;
            editorSearch.Enabled = isSelected;
            editorPaste.Enabled = editor.CanPaste;
            editorUndo.Enabled = editor.CanUndo;
            editorRedo.Enabled = editor.CanRedo;
        }

        private void ConvertLineEndings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Convert all End of Line characters in this document to " + lineReturnType.Text + "?", ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                editor.ConvertEols(editor.EolMode);
            }
        }

        private void CursorPosition_Changed(object sender, EventArgs e)
        {
            UpdatePos();
        }

        private void Edit_DropDownClosed(object sender, EventArgs e)
        {
            editSearch.Enabled = true;
        }

        private void Menu_Opening(object sender, EventArgs e)
        {
            switch (((ToolStripMenuItem)sender).Name)
            {
                case "editMenu":
                    bool isSelected = editor.SelectionLength > 0;
                    editCut.Enabled = isSelected;
                    editCopy.Enabled = isSelected;
                    editDelete.Enabled = isSelected;
                    editSearch.Enabled = isSelected;
                    editPaste.Enabled = editor.CanPaste;
                    editUndo.Enabled = editor.CanUndo;
                    editRedo.Enabled = editor.CanRedo;
                    break;

                case "viewMenu":
                    viewStatusBar.Checked = statusBar.Visible;
                    break;

                case "formatMenu":
                    formatWordWrap.Checked = editor.WrapMode == WrapMode.Word;
                    break;
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            switch (((ToolStripMenuItem)sender).Name)
            {
                #region File Menu

                case "fileNew":
                    NewDocument();
                    break;

                case "fileNewWindow":
                    NewWindow();
                    break;

                case "fileOpen":
                    OpenDocument();
                    break;

                case "fileSave":
                    _ = IsReadOnly
                        ? SaveConfirmPrompt(true)
                        : SaveDocument();
                    break;

                case "fileSaveAs":
                    SaveDocument(true);
                    break;

                case "filePageSetup":
                    PageSetup();
                    break;

                case "filePrint":
                    Print();
                    break;

                case "fileExit":
                    Close();
                    break;

                #endregion File Menu

                #region Edit Menu

                case "editUndo":
                case "editorUndo":
                    editor.Undo();
                    break;

                case "editRedo":
                case "editorRedo":
                    editor.Redo();
                    break;

                case "editCut":
                case "editorCut":
                    editor.Cut();
                    break;

                case "editCopy":
                case "editorCopy":
                    editor.Copy();
                    break;

                case "editPaste":
                case "editorPaste":
                    editor.Paste();
                    break;

                case "editDelete":
                case "editorDelete":
                    editor.Clear();
                    break;

                case "editSearch":
                case "editorSearch":
                    _ = editor.SelectionLength > 0
                        ? Process.Start("https://google.com/search?q=" + WebUtility.UrlEncode(editor.SelectedText))
                        : null;
                    break;

                case "editFind":
                    Find();
                    break;

                case "editFindNext":
                    FindNext(sender, e);
                    break;

                case "editFindPrevious":
                    FindPrev(sender, e);
                    break;

                case "editReplace":
                    ReplacePrompt();
                    break;

                case "editGoTo":
                    using (GoToPrompt gt = new(editor.Ln))
                    {
                        gt.GoToClick += GoToLineFromPrompt;
                        _ = gt.ShowDialog();
                    }
                    break;

                case "editSelectAll":
                case "editorSelectAll":
                    editor.SelectAll();
                    break;

                case "editTimeDate":
                    editor.ReplaceSelection(DateTime.Now.ToString("g"));
                    break;

                #endregion Edit Menu

                #region Format Menu

                case "formatWordWrap":
                    editor.WrapMode = ToggleSetting("WordWrap")
                        ? WrapMode.Word
                        : WrapMode.None;
                    ((ToolStripMenuItem)sender).Checked = editor.WrapMode == WrapMode.Word;
                    UpdatePos();
                    break;

                case "formatFont":
                    SetFont();
                    break;

                #endregion Format Menu

                #region View Menu

                case "zoomIn":
                    if (editor.Zoom < 50)
                    {
                        editor.ZoomIn();
                    }
                    break;

                case "zoomOut":
                    if (editor.Zoom > -9)
                    {
                        editor.ZoomOut();
                    }
                    break;

                case "restoreDefaultZoom":
                    editor.Zoom = 0;
                    break;

                case "viewStatusBar":
                    StatusBar = !StatusBar;
                    break;

                #endregion View Menu

                #region Help Menu

                case "helpViewHelp":
                    Process.Start("https://stevenwilson.ca/bortpad/help");
                    break;

                case "helpSendFeedback":
                    Process.Start("https://stevenwilson.ca/contact");
                    break;

                case "helpAbout":
                    using (About about = new())
                    {
                        _ = about.ShowDialog();
                    }
                    break;

                    #endregion Help Menu
            }
        }

        private void Modified(object sender, EventArgs e)
        {
            editFind.Enabled = editor.HasText;
            editFindNext.Enabled = CanRepeatFind();
            editFindPrevious.Enabled = CanRepeatFind();
            // replaceToolStripMenuItem.Enabled = editor.HasText;
            // goToToolStripMenuItem.Enabled = editor.HasText;
            // selectAllToolStripMenuItem.Enabled = editor.HasText;
        }

        private void ModifyAttempt(object sender, EventArgs e)
        {
            ReadonlyPrompt ro = new(editor.ReadOnly);
            if (ro.ShowDialog(this) == DialogResult.OK && editor.ReadOnly)
            {
                editor.ReadOnly = false;
                readOnlyNotice.DisplayStyle = ToolStripItemDisplayStyle.Text;
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(editor.Text, editor.Font, Brushes.Black, 20, 20);
        }

        private void ReadOnlyNotice_Click(object sender, EventArgs e)
        {
            ModifyAttempt(sender, e);
        }

        private void ReadOnlyNotice_VisibleChanged(object sender, EventArgs e)
        {
            readOnlyNotice.Enabled = readOnlyNotice.Visible;
            readOnlyNotice.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void ScrollZoom(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                MenuItem_Click(zoomIn, e);
                return;
            }
            MenuItem_Click(zoomOut, e);
        }

        private void SetEncoding(object sender, EventArgs e)
        {
            SetEncodingStatus(EncodingSetting = ((EncodingInfo)((ToolStripMenuItem)sender).Tag).GetEncoding(), false);
        }

        private void SetEOL(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            EolSetting = (Eol)clickedItem.Tag;
        }

        private void ShowLineEndings_Click(object sender, EventArgs e)
        {
            editor.ViewEol = !editor.ViewEol;
            showLineEndings.Checked = editor.ViewEol;
            SetSetting("ShowLineEndings", editor.ViewEol);
        }

        private void ToggleDarkMode(object sender, EventArgs e)
        {
            DarkMode = !DarkMode;
        }

        private void UpdateTitle(object sender, EventArgs e)
        {
            Text = (editor.Modified ? "*" : "") + (IsFile ? Path.GetFileName(FileName) : FileName) + " - " + ProgramName;
        }

        private void Zoom_Changed(object sender, EventArgs e)
        {
            zoomLevel.Text = (100 + (editor.Zoom * 10)) + "%";
        }

        private void ZoomLevel_DoubleClick(object sender, EventArgs e)
        {
            editor.Zoom = 0;
        }

        private void ZoomLevel_MouseEnter(object sender, EventArgs e)
        {
            statusBar.MouseWheel += ScrollZoom;
        }

        private void ZoomLevel_MouseLeave(object sender, EventArgs e)
        {
            statusBar.MouseWheel -= ScrollZoom;
        }

        #endregion Instance Methods: Events
    }
}