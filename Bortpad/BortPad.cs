using Bortpad.Properties;
using PropertyChanged;
using ScintillaNET;
using SharpConfig;
using System;
using System.Collections.Generic;
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
using WilsonUtils;

namespace Bortpad;

[AddINotifyPropertyChangedInterface]
public partial class Bortpad : Form
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
    internal const string _FEEDBACK_URL = "https://stevenwilson.ca/contact";
    internal const string _HELP_URL = "https://stevenwilson.ca/bortpad/help";
    internal const Eol _LF = Eol.Lf;
    internal const string _SEARCH_URI = "https://google.com/search?q=";

    #endregion _CONSTANTS

    #region _fields

    private Encoding _encodingSetting;
    private string _filename;

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
        get; private set;
    }

    public Encoding EncodingSetting
    {
        get => _encodingSetting ?? Encoding.GetEncoding(GetSetting<int>("DefaultEncoding"));
        private set => _encodingSetting = value;
    }

    public Eol EolSetting
    {
        get; private set;
    }

    public string FileName
    {
        get => _filename ?? Resources.DefaultFilename ?? _DEFAULT_FILENAME;
        private set => _filename = value;
    }

    public bool IsFile => _filename is not null && File.Exists(_filename);

    public bool IsReadOnly
    {
        get => IsFile && OpenFilesInfo.IsReadOnly;
        private set
        {
            // See OnPropertyChanged event
        }
    }

    public string MainStatus
    {
        get; private set;
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
        get; private set;
    }

    public bool ViewEol
    {
        get; private set;
    }

    #endregion Properties

    #region Constructors

    public Bortpad(string filenameSpecified = null)
    {
        InitializeComponent();

        FileName = filenameSpecified;

        ConfigFile = $"{ProgramName}.cfg";
        EncodingSetting = Encoding.GetEncoding(GetSetting<int>("DefaultEncoding")); // Default for new files
        editor.Font = GetSetting<Font>("Font");
        editor.WrapMode = GetSetting<bool>("WordWrap") ? WrapMode.Word : WrapMode.None;
        StatusBar = GetSetting<bool>("StatusBar");
        DarkMode = GetSetting<bool>("DarkMode");
        windowsLineFeed.Tag = _CRLF;
        linuxLineFeed.Tag = _LF;
        macLineFeed.Tag = _CR;
        ViewEol = GetSetting<bool>("ShowLineEndings");
        EolSetting = GetSetting<Eol>("LineEnding");

        Text = $"{Resources.DefaultFilename ?? _DEFAULT_FILENAME} - {ProgramName}";
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
        bool keyNotFound = false;
        try
        {
            if (Settings is null || !Settings.Any())
            {
                LoadSettings();
            }
            if (!Settings.Contains(section ??= _DEFAULT_SECTION))
            {
                throw new KeyNotFoundException();
            }
            if (!Settings[section].Contains(key ??= ""))
            {
                keyNotFound = true;
                throw new KeyNotFoundException();
            }
            return Settings[section][key].GetValue<T>();
        }
        catch (KeyNotFoundException e)
        {
            _ = MsgBox.Show(string.Format(keyNotFound ? Resources.SettingsKeyNotFound : Resources.SettingsSectionNotFound, section, key), e.Message, ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
        catch (Exception e)
        {
            _ = MsgBox.Show(e.Message, ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
        return Settings[section][key].GetValue<T>();
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
        // _ = MessageBox.Show("The system cannot find the path specified.", ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        _ = MsgBox.Show(
            Resources.PathNotFound,
            ProgramName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }

    private bool NewDocument(bool saveFirst = true)
    {
        if (!saveFirst || SaveConfirmPrompt(force: false, pendingAction: true))
        {
            editor.Reset();
            readOnlyNotice.Visible = false;
            editor.EmptyUndoBuffer();

            FileName = null;
            SetEncodingStatus(EncodingSetting = null);
            editor.SetSavePoint();
            editor.Select();
            return true;
        }
        return false;
    }

    private bool OpenDocument(bool saveFirst = true, string fileName = null)
    {
        if (saveFirst && !SaveConfirmPrompt(force: false, pendingAction: true))
        {
            // User clicked Cancel on SaveConfirmPrompt
            return false;
        }
        try
        {
            // If no filename as param, get filename from Open Dialog
            string openFileName
                = fileName
                ?? (openFileDialog1.ShowDialog() == DialogResult.OK
                    ? openFileDialog1.FileName
                    : null);
            if (openFileName is null)
            {
                // No filename as param and Open Dialog cancelled?
                return false;
            }
            if (!File.Exists(openFileName))
            {
                // Where is file? :(
                throw new FileNotFoundException();
            }
            DetectionDetail getEncoding = CharsetDetector.DetectFromFile(openFileName)?.Detected;
            SetEncodingStatus(EncodingSetting = getEncoding?.Encoding, true, getEncoding?.Confidence ?? 0);
            editor.Reset();
            readOnlyNotice.Visible = false;
            editor.Text = File.ReadAllText(openFileName, EncodingSetting);
            FileName = openFileName;
            editor.EmptyUndoBuffer();
            editor.ReadOnly = IsReadOnly;
            readOnlyNotice.Visible = editor.ReadOnly;
            editor.SetSavePoint();
            editor.Select();
            return true;
        }
        catch (FileNotFoundException)
        {
            FileNotFound();
            return false;
        }
        catch (Exception e)
        {
            // _ = MessageBox.Show(e.Message, ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            _ = MsgBox.Show(e.Message, ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
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

    private bool SaveConfirmPrompt(bool force = false, bool pendingAction = false)
    {
        if (!force && !editor.Modified)
        {
            // Return: false = cancel pending open dialog/close app action
            // true = we're good to proceed and launch open dialog/new/exit
            return true;
        }
        if (IsReadOnly)
        {
            if (!readOnlyNotice.Visible)
            {
                readOnlyNotice.Visible = true;
            }
            // ReadonlyDecision rod = new();
            // rod.ShowDialog();
            // switch (rod.DialogResult)
            List<Button> buttons = new() {
                new()
                {
                    Name = "unsetAndSaveBtn",
                    Text = Resources.unsetAndSaveBtn_Text,
                    DialogResult = DialogResult.Yes
                },
                new()
                {
                    Name = "saveAsBtn",
                    Text = Resources.saveAsBtn_Text,
                    DialogResult = DialogResult.OK
                },
                new()
                {
                    Name = "cancelBtn",
                    Text = Resources.cancelBtn_Text,
                    DialogResult = DialogResult.Cancel
                }
            };
            if (pendingAction)
            {
                // "Don't Save" and "Cancel" are the same unless prompt was triggered by Open/New/Exit
                buttons.Insert(2,
                    new()
                    {
                        Name = "dontSaveBtn",
                        Text = Resources.dontSaveBtn_Text,
                        DialogResult = DialogResult.No
                    }); ;
            }
            DialogResult rod = MsgBox.Show(
                string.Format(Resources.IsMarkedAsReadOnly, Path.GetFileName(FileName)),
                Resources.ReadOnlyOptions,
                ProgramName,
                buttons,
                null,
                "saveAsBtn");
            switch (rod)
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
        // SaveConfirmPrompt scp = new(FileName);
        // scp.ShowDialog();
        DialogResult scp = MsgBox.Show(
            string.Format(Resources.SaveChangesTo, Path.GetFileName(FileName)),
            "",
            ProgramName,
            new()
            {
                new()
                {
                    Name = "saveBtn",
                    Text = Resources.saveBtn_Text,
                    DialogResult = DialogResult.Yes
                },
                new()
                {
                    Name = "dontSaveBtn",
                    Text = Resources.dontSaveBtn_Text,
                    DialogResult = DialogResult.No
                },
                new()
                {
                    Name = "cancelBtn",
                    Text = Resources.cancelBtn_Text,
                    DialogResult = DialogResult.Cancel
                }
            },
            null,
            "saveBtn");
        return scp switch
        {
            DialogResult.Yes => SaveDocument(), // Save/Show Save Dialog
            DialogResult.No => true,
            _ => false,
        };
    }

    private bool SaveDocument(bool saveAs = false)
    {
        string fileName;
        if (IsFile && !saveAs)
        {
            if (IsReadOnly)
            {
                return SaveConfirmPrompt(force: true, pendingAction: false);
            }
            fileName = FileName;
        }
        else
        {
            saveFileDialog1.FileName = Path.GetFileName(FileName);
            saveFileDialog1.InitialDirectory = Path.GetDirectoryName(FileName);
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            fileName = saveFileDialog1.FileName;
        }

        try
        {
            File.WriteAllText(fileName, editor.Text, EncodingSetting);
            FileName = fileName;
            editor.SetSavePoint();
            return true;
        }
        catch (Exception e)
        {
            _ = MsgBox.Show(e.Message, ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
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
        FindPrompt find = Application.OpenForms.OfType<FindPrompt>().FirstOrDefault();
        if (find is not null)
        {
            find.BringToFront();
            return;
        }
        find = new(
            GetSetting<string>("Search", "Find"),
            GetSetting<bool>("Up", "Find"),
            GetSetting<bool>("MatchCase", "Find"),
            GetSetting<bool>("WrapAround", "Find"));
        find.FindClick += FindFromPrompt;
        find.Show();
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
        int current = editor.SelectionStart;
        int length = editor.SelectionLength;
        int pos =
            (prev && current == 0)
            ? -1
            : OmniIndexOf(searchQuery, editor.Text, prev, current + (prev ? -1 : length), searchMatchCase);
        if (pos == -1 && GetSetting<bool>("WrapAround", "Find"))
        {
            pos = OmniIndexOf(searchQuery, editor.Text, prev, prev ? editor.Text.Length - 1 : 0, searchMatchCase);
            MainStatus = pos == -1 ? "" : string.Format(prev ? Resources.FoundNextBottom : Resources.FoundNextTop);
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
        if (ln < 1 || ln > editor.NumLines)
        {
            // _ = MessageBox.Show("The line number is beyond the total number of lines", ProgramName + " - Goto Line", MessageBoxButtons.OK);
            _ = MsgBox.Show(Resources.LineNumberBeyond, $"{ProgramName} - {Resources.GotoLine}", MessageBoxButtons.OK);
            return;
        }
        editor.Ln = (int)ln;
        ((GoToPrompt)sender).Close();
    }

    private void HighlightResult(int pos, string q)
    {
        if (pos == -1)
        {
            // _ = MessageBox.Show("Cannot find \"" + q + "\"", ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            _ = MsgBox.Show(string.Format(Resources.CannotFind, q), ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        editor.Pos = pos;
        editor.SetSelection(pos, pos + q.Length);
        editor.ScrollCaret();
        UpdatePos();
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
        ReplacePrompt replace = Application.OpenForms.OfType<ReplacePrompt>().FirstOrDefault();
        if (replace is not null)
        {
            replace.BringToFront();
            return;
        }
        replace = new(
            GetSetting<string>("Search", "Find"),
            GetSetting<string>("Replace", "Find"),
            GetSetting<bool>("MatchCase", "Find"),
            GetSetting<bool>("WrapAround", "Find"));
        replace.FindClick += FindNext;
        replace.ReplaceClick += ReplaceOne;
        replace.ReplaceAllClick += ReplaceAll;
        replace.Show();
    }

    #endregion Instance Methods: Find & Replace

    #region Instance Methods: Status Bar

    private void SetEncodingStatus(Encoding encoding, bool detected = false, float confidence = 1)
    {
        encoding ??= EncodingSetting;
        encodingStatus.DropDownItems.Clear();
        encodingStatus.Text = encoding?.EncodingName;
        encodingStatus.ToolTipText = detected ? string.Format(Resources.Confidence, confidence * 100) : "";

        ToolStripMenuItem[] items = CodePages.Select(e =>
            {
                bool defaultopt = e.CodePage == _DEFAULT_CODEPAGE;
                bool current = Equals(e.GetEncoding(), encoding);
                ToolStripMenuItem i = new()
                {
                    Name = e.Name,
                    Text = e.DisplayName + (detected && current ? $" ({Resources.Detected})" : "") + (defaultopt ? $" ({Resources.Default})" : ""),
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
            .ThenByDescending(i => ((EncodingInfo)(i.Tag))?.CodePage == _DEFAULT_CODEPAGE)
            .ThenBy(i => i.Text)
            .ToArray();
        encodingStatus.DropDownItems.AddRange(items);
        encodingStatus.DropDownItems.Insert(encoding?.CodePage == _DEFAULT_CODEPAGE ? 1 : 2, new ToolStripSeparator());
    }

    private void SetEolStatus(Eol eolMode)
    {
        showLineEndings.Checked = editor.ViewEol;
        ToolStripItemCollection items = lineReturnType.DropDownItems;
        foreach (ToolStripMenuItem item in items.OfType<ToolStripMenuItem>())
        {
            if (item?.Tag is not null && item.Tag.GetType().Equals(typeof(Eol)))
            {
                if (Equals(item.Tag, eolMode))
                {
                    item.Checked = true;
                    lineReturnType.Text = Regex.Replace(item.Text, "&(.)", "$1");
                    lineReturnType.Tag = item.Tag;
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
        position.Text = $"{Resources.Ln} {editor.Ln}, {Resources.Col} {editor.Col}";
    }

    #endregion Instance Methods: Status Bar

    #region Instance Methods: Events

    private void BortForm_DragDrop(object sender, DragEventArgs e)
    {
        holdShiftNotice.Visible = false;
        holdShiftNotice.Image = Resources.shift;
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
        holdShiftNotice.Image = Resources.shift;
    }

    private void BortForm_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            holdShiftNotice.Visible = true;
            if ((e.KeyState & 4) != 4)
            {
                e.Effect = DragDropEffects.Copy;
                holdShiftNotice.Image = Resources.shift;
                return;
            }
            e.Effect = DragDropEffects.Move;
            holdShiftNotice.Image = Resources.check;
        }
    }

    private void BortForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (!Application.OpenForms.OfType<Bortpad>().Any())
        {
            Application.Exit();
        }
    }

    private void BortForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = !SaveConfirmPrompt(force: false, pendingAction: true);
    }

    private void BortForm_Shown(object sender, EventArgs e)
    {
        if (_filename is null || !OpenDocument(false, _filename))
        {
            _ = NewDocument(false);
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
        if (MsgBox.Show(
                string.Format(Resources.ConvertLineEndings, lineReturnType.Text),
                string.Format(Resources.ConvertLineEndingsDesc, ((Eol)lineReturnType.Tag).ToString()),
                ProgramName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2)
            == DialogResult.Yes)
        {
            editor.ConvertEols(editor.EolMode);
        }
    }

    private void CursorPosition_Changed(object sender, UpdateUIEventArgs e)
    {
        UpdatePos();
    }

    private void Edit_DropDownClosed(object sender, EventArgs e)
    {
        // This is just a workaround to make the Ctrl+E shortcut work reliably
        editSearch.Enabled = true;
    }

    private void LineReturnType_DropDownOpening(object sender, EventArgs e)
    {
        showLineEndings.Checked = editor.ViewEol;
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
                    ? SaveConfirmPrompt(force: true, pendingAction: false)
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
                    ? Process.Start(_SEARCH_URI + WebUtility.UrlEncode(editor.SelectedText))
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
                Process.Start(_HELP_URL);
                break;

            case "helpSendFeedback":
                Process.Start(_FEEDBACK_URL);
                break;

            case "helpAbout":
                using (About about = new())
                {
                    _ = about.ShowDialog();
                }
                break;

            default:
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
        // ReadonlyPrompt ro = new(editor.ReadOnly);
        // if (ro.ShowDialog() == DialogResult.OK && editor.ReadOnly)
        List<Button> buttons = new()
        {
            new()
            {
                Name = "editBtn",
                Text = editor.ReadOnly ? Resources.editBtn_Text : Resources.okBtn_Text,
                DialogResult = DialogResult.OK
            }
        };
        if (editor.ReadOnly)
        {
            buttons.Add(new()
            {
                Name = "cancelBtn",
                Text = Resources.cancelBtn_Text,
                DialogResult = DialogResult.Cancel
            });
        }
        DialogResult ro = MsgBox.Show(
            string.Format(Resources.IsMarkedAsReadOnly, Path.GetFileName(FileName)),
            Resources.ChooseToEdit,
            ProgramName,
            buttons,
            null,
            editor.ReadOnly ? "cancelBtn" : "editBtn");
        if (ro == DialogResult.OK && editor.ReadOnly)
        {
            editor.ReadOnly = false;
            readOnlyNotice.DisplayStyle = ToolStripItemDisplayStyle.Text;
        }
    }

    private void OnPropertyChanged(string propertyName, object before, object after)
    {
        // Side effects of property changes performed here
        if (Equals(before, after))
        {
            return;
        }
        switch (propertyName)
        {
            case "StatusBar":
                bool statusBarValue = (bool)after;
                SetSetting("StatusBar", statusBar.Visible = statusBarValue);
                viewStatusBar.Checked = statusBar.Visible;
                if (statusBarValue) { UpdatePos(); }
                break;

            case "DarkMode":
                bool darkModeValue = (bool)after;
                editor.Styles[Style.Default].BackColor = darkModeValue ? Color.Black : Color.White;
                editor.Styles[Style.Default].ForeColor = darkModeValue ? Color.White : Color.Black;
                editor.StyleClearAll();
                editor.CaretForeColor = darkModeValue ? Color.White : Color.Black;
                editor.CaretLineBackColor = darkModeValue ? Color.Black : Color.White;
                editor.CaretLineBackColorAlpha = 256;
                darkMode.Text = darkModeValue ? "☀" : "🌙";
                ColorSwapItem(darkMode, !darkModeValue);
                ColorSwap(statusBar, darkModeValue);
                readOnlyNotice.LinkColor = darkModeValue ? SystemColors.Control : SystemColors.ControlText;
                readOnlyNotice.Image = darkModeValue ? Resources.readonlyw : Resources._readonly;

                darkMode.Checked = darkModeValue;
                SetSetting("DarkMode", darkModeValue);
                break;

            case "EolSetting":
                Eol eolSetting = (Eol)after;
                editor.EolMode = eolSetting;
                SetSetting("LineEnding", eolSetting);
                SetEolStatus(eolSetting);
                break;

            case "IsReadOnly":
                bool isReadOnly = (bool)after;
                if (IsFile && OpenFilesInfo.IsReadOnly != isReadOnly)
                {
                    OpenFilesInfo.IsReadOnly = isReadOnly;
                }
                readOnlyNotice.Visible = isReadOnly;
                break;

            case "MainStatus":
                statusBarLeft.Text = (string)after;
                break;

            case "ViewEol":
                editor.ViewEol = (bool)after;
                showLineEndings.Checked = editor.ViewEol;
                SetSetting("ShowLineEndings", editor.ViewEol);
                break;

            default:
                break;
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
        ViewEol = !ViewEol;
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