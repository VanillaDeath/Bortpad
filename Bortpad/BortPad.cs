using Bortpad.Properties;
using PropertyChanged;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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

    public const Eol _CR = Eol.Cr;
    public const Eol _CRLF = Eol.CrLf;
    public const Eol _LF = Eol.Lf;

    #endregion _CONSTANTS

    #region _fields

    private static readonly Dictionary<string, object> _defaults = new()
    {
        { "General_DefaultEncoding", 65001 },
        { "General_Font", new Font("Consolas", 11.25f) },
        { "General_StatusBar", true },
        { "General_WordWrap", false },
        { "General_DarkMode", false },
        { "General_LineEnding", _CRLF },
        { "General_ShowLineEndings", false },
        { "Find_Search", "" },
        { "Find_Replace", "" },
        { "Find_Up", false },
        { "Find_MatchCase", false },
        { "Find_WrapAround", false },
        { "Filename", "Untitled" },
        { "Section", "General" },
        { "ConfigFile", $"{ProgramName}.cfg" },
        { "SearchURI", "https://google.com/search?q="},
        { "HelpURL", "https://stevenwilson.ca/bortpad/help" },
        { "FeedbackURL", "https://stevenwilson.ca/contact" }
    };

    private Encoding _encodingSetting;
    private string _filename;
    private string _printString;

    #endregion _fields

    #region Properties

    public static EncodingInfo[] CodePages
    {
        get;
    } = Encoding
        .GetEncodings()
        .OrderBy(e => e.DisplayName)
        .ThenBy(e => e.Name)
        .ToArray();

    public static Dictionary<string, object> Defaults
    {
        get;
    } = _defaults;

    public static string ProgramName
    {
        get;
    } = Assembly.GetExecutingAssembly().GetName().Name;

    public Config Conf
    {
        get; private set;
    } = Config.Load(
        configFile: string.Format(Resources.ConfigFile, ProgramName),
        defaults: Defaults,
        programName: ProgramName
        );

    public bool DarkMode
    {
        get; private set;
    } = (bool)Defaults["General_DarkMode"];

    public Encoding EncodingSetting
    {
        get => _encodingSetting ?? Encoding.GetEncoding(Conf.Get<int>("DefaultEncoding")) ?? Encoding.GetEncoding((int)Defaults["General_DefaultEncoding"]);
        private set => _encodingSetting = value;
    }

    public Eol EolSetting
    {
        get; private set;
    } = (Eol)Defaults["General_LineEnding"];

    public string FileName
    {
        get => _filename ?? Resources.DefaultFilename ?? (string)Defaults["Filename"];
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
    } = "";

    public FileInfo OpenFilesInfo => IsFile ? new(_filename) : null;

    public bool StatusBar
    {
        get; private set;
    } = (bool)Defaults["General_StatusBar"];

    public bool ViewEol
    {
        get; private set;
    } = (bool)Defaults["General_ShowLineEndings"];

    #endregion Properties

    #region Constructors and Initializers

    public Bortpad(string filenameSpecified)
    {
        InitializeComponent();

        if (!Init(filenameSpecified))
        {
            Close();
        }
    }

    public Bortpad() : this(null)
    {
    }

    public bool Init(string filenameSpecified)
    {
        try
        {
            FileName = filenameSpecified;

            Conf ??= Config.Load(string.Format(Resources.ConfigFile, ProgramName), Defaults, ProgramName);

            EncodingSetting = null; // Default for new files
            editor.Font = Conf.Get<Font>("Font");
            editor.WrapMode = Conf.Get<bool>("WordWrap") ? WrapMode.Word : WrapMode.None;
            StatusBar = Conf.Get<bool>("StatusBar");
            DarkMode = Conf.Get<bool>("DarkMode");
            windowsLineFeed.Tag = Eol.CrLf;
            linuxLineFeed.Tag = Eol.Lf;
            macLineFeed.Tag = Eol.Cr;
            lineReturnType.Tag = Defaults["General_LineEnding"];
            ViewEol = Conf.Get<bool>("ShowLineEndings");
            EolSetting = Conf.Get<Eol>("LineEnding");

            // Resources.DefaultFilename for language-dependent new file name
            Text = $"{Resources.DefaultFilename ?? Defaults["Filename"]} - {ProgramName}";

            return true;
        }
        catch (Exception e)
        {
            ErrorBox(e);
            return false;
        }
    }

    #endregion Constructors and Initializers

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

    public static void ErrorBox(Exception e, MessageBoxIcon? icon = MessageBoxIcon.Error)
    {
        Trace.TraceError(e.Message);
        Trace.Indent();
        Trace.TraceError(e.StackTrace);
        Trace.Unindent();
        _ = MsgBox.Show(e.Message, ProgramName, MessageBoxButtons.OK, icon);
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

    #region Instance Methods: Document

    private bool NewDocument(bool saveFirst = true)
    {
        if (!saveFirst || SaveConfirmPrompt(force: false, pendingAction: true))
        {
            SetEncodingStatus(EncodingSetting = null);

            editor.Reset();
            readOnlyNotice.Visible = false;
            editor.EmptyUndoBuffer();

            FileName = null;

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
            string openFileName;
            // If no filename as param, get filename from Open Dialog
            if (fileName is null)
            {
                using OpenFileDialog openDlg = new()
                {
                    Title = Resources.openDlg_Title,
                    DefaultExt = "txt",
                    Filter = $"{Resources.TextFiles}|*.txt|{Resources.AllFiles}|*.*"
                };
                if (openDlg.ShowDialog() != DialogResult.OK || (openFileName = openDlg.FileName) is null)
                {
                    return false;
                }
            }
            else
            {
                openFileName = fileName;
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
            _ = MsgBox.Show(
                Resources.PathNotFound,
                ProgramName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return default;
        }
        catch (Exception e)
        {
            ErrorBox(e);
            return default;
        }
    }

    private void PageSetup()
    {
        using PageSetupDialog setupDlg = new()
        {
            Document = printDoc
        };
        if (setupDlg.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        printDoc.DefaultPageSettings = setupDlg.PageSettings;
        printDoc.PrinterSettings = setupDlg.PrinterSettings;
    }

    private void Print()
    {
        using PrintDialog printDlg = new()
        {
            AllowSelection = true,
            Document = printDoc,
            UseEXDialog = true
        };
        if (printDlg.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        printDoc.PrinterSettings = printDlg.PrinterSettings;
        _printString = editor.Text;
        printDoc.Print();
    }

    private bool SaveConfirmPrompt(bool force = false, bool pendingAction = false)
    {
        if (!force && !editor.Modified)
        {
            // Return: false = cancel pending open dialog/close app action
            // true = we're good to proceed and launch open dialog/new/exit
            return true;
        }
        // READ-ONLY files
        if (IsReadOnly)
        {
            if (!readOnlyNotice.Visible)
            {
                readOnlyNotice.Visible = true;
            }
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
                    });
            }

            switch (MsgBox.Show(
                string.Format(Resources.IsMarkedAsReadOnly, Path.GetFileName(FileName)),
                Resources.ReadOnlyOptions,
                ProgramName,
                buttons,
                null,
                "saveAsBtn"))
            {
                case DialogResult.Yes:
                    IsReadOnly = false;
                    editor.ReadOnly = IsReadOnly;
                    readOnlyNotice.Visible = editor.ReadOnly;
                    return SaveDocument();

                case DialogResult.OK:
                    if (SaveDocument(saveAs: true))
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

        // Non READ-ONLY files
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
            using SaveFileDialog saveDlg = new()
            {
                Title = Resources.saveDlg_Title,
                DefaultExt = "txt",
                Filter = $"{Resources.TextFiles}|*.txt|{Resources.AllFiles}|*.*",
                FileName = Path.GetFileName(FileName),
                InitialDirectory = Path.GetDirectoryName(FileName)
            };
            if (saveDlg.ShowDialog() != DialogResult.OK || (fileName = saveDlg.FileName) is null)
            {
                return false;
            }
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
            ErrorBox(e);
            return default;
        }
    }

    private void SetFont()
    {
        using FontDialog fontDlg = new()
        {
            Font = editor.Font,
            ShowEffects = false
        };
        if (fontDlg.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        Conf.Set("Font", editor.Font = fontDlg.Font);
        editor.StyleClearAll();
    }

    #endregion Instance Methods: Document

    #region Instance Methods: Find & Replace

    private bool CanRepeatFind()
    {
        return Conf.Get<string>("Search", "Find").Length > 0 && editor.HasText;
    }

    private void Find()
    {
        FindPrompt find;
        if ((find = Application.OpenForms.OfType<FindPrompt>().FirstOrDefault()) is not null)
        {
            find.BringToFront();
            return;
        }
        find = new(
            Conf.Get<string>("Search", "Find"),
            Conf.Get<bool>("Up", "Find"),
            Conf.Get<bool>("MatchCase", "Find"),
            Conf.Get<bool>("WrapAround", "Find"));
        find.FindClick += FindFromPrompt;
        find.Show();
    }

    private void FindFromPrompt(object sender, EventArgs args)
    {
        FindPrompt thisPrompt = (FindPrompt)sender;
        Conf.Set("Search", thisPrompt.SearchQuery, "Find", save: false);
        Conf.Set("Up", thisPrompt.Up, "Find", save: false);
        Conf.Set("MatchCase", thisPrompt.MatchCase, "Find", save: false);
        Conf.Set("WrapAround", thisPrompt.WrapAround, "Find", save: false);
        Conf.Save();

        FindNextPrev(previous: thisPrompt.Up);
    }

    private void FindNext(object sender, EventArgs args)
    {
        FindNextPrev(previous: false);
    }

    private void FindNextPrev(bool previous = false)
    {
        string searchQuery = Conf.Get<string>("Search", "Find");
        bool searchMatchCase = Conf.Get<bool>("MatchCase", "Find");
        int current = editor.SelectionStart;
        int length = editor.SelectionLength;
        int pos
            = (previous && current == 0)
            ? -1
            : OmniIndexOf(searchQuery, editor.Text, previous, current + (previous ? -1 : length), searchMatchCase);
        if (pos == -1 && Conf.Get<bool>("WrapAround", "Find"))
        {
            pos = OmniIndexOf(searchQuery, editor.Text, previous, previous ? editor.Text.Length - 1 : 0, searchMatchCase);
            MainStatus = pos == -1 ? "" : string.Format(previous ? Resources.FoundNextBottom : Resources.FoundNextTop);
        }
        else
        {
            MainStatus = "";
        }
        HighlightResult(pos, searchQuery);
    }

    private void FindPrev(object sender, EventArgs args)
    {
        FindNextPrev(previous: true);
    }

    private void GoTo()
    {
        using GoToPrompt gt = new(editor.Ln);
        gt.GoToClick += GoToLineFromPrompt;
        _ = gt.ShowDialog();
    }

    private void GoToLineFromPrompt(object sender, EventArgs args)
    {
        long ln = ((GoToPrompt)sender).LineNumber;
        if (ln < 1 || ln > editor.NumLines)
        {
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
            _ = MsgBox.Show(string.Format(Resources.CannotFind, q), ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        editor.GotoPosition(pos);
        editor.SetSelection(pos, pos + q.Length);
        editor.ScrollCaret();
        UpdatePos();
    }

    private void ReplaceAll(object sender, EventArgs args)
    {
        ReplaceFromPrompt(sender, args, replaceAll: true);
    }

    private void ReplaceFromPrompt(object sender, EventArgs args, bool replaceAll = false)
    {
        ReplacePrompt replacePrompt = (ReplacePrompt)sender;
        string query = replacePrompt.SearchQuery;
        string replaceString = replacePrompt.ReplaceWith;
        bool matchCase = replacePrompt.MatchCase;
        bool wrapAround = replacePrompt.WrapAround;
        Conf.Set("Search", query, "Find", save: false);
        Conf.Set("Replace", replaceString, "Find", save: false);
        Conf.Set("MatchCase", matchCase, "Find", save: false);
        Conf.Set("WrapAround", wrapAround, "Find", save: false);
        Conf.Save();

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
        ReplaceFromPrompt(sender, args, replaceAll: false);
    }

    private void ReplacePrompt()
    {
        ReplacePrompt replace;
        if ((replace = Application.OpenForms.OfType<ReplacePrompt>().FirstOrDefault()) is not null)
        {
            replace.BringToFront();
            return;
        }
        replace = new(
            Conf.Get<string>("Search", "Find"),
            Conf.Get<string>("Replace", "Find"),
            Conf.Get<bool>("MatchCase", "Find"),
            Conf.Get<bool>("WrapAround", "Find"));
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
                bool defaultEnc = e.CodePage == Conf.Get<int>("DefaultEncoding");
                bool currentEnc = Equals(e.GetEncoding(), encoding);
                ToolStripMenuItem i = new()
                {
                    Name = e.Name,
                    Text = e.DisplayName + (detected && currentEnc ? $" ({Resources.Detected})" : "") + (defaultEnc ? $" ({Resources.Default})" : ""),
                    Tag = e,
                    Checked = currentEnc,
                    Font = defaultEnc ? new Font(Font, FontStyle.Bold) : null,
                };
                if (!i.Checked)
                {
                    i.Click += SetEncoding;
                }
                return i;
            }
        )
            .OrderByDescending(i => i.Checked)
            .ThenByDescending(i => ((EncodingInfo)(i.Tag))?.CodePage == Conf.Get<int>("DefaultEncoding"))
            .ThenBy(i => i.Text)
            .ToArray();
        encodingStatus.DropDownItems.AddRange(items);
        encodingStatus.DropDownItems.Insert(encoding?.CodePage == Conf.Get<int>("DefaultEncoding") ? 1 : 2, new ToolStripSeparator());
    }

    private void SetEolStatus(Eol eolMode)
    {
        showLineEndings.Checked = editor.ViewEol;
        ToolStripItemCollection items = lineReturnType.DropDownItems;
        foreach (ToolStripMenuItem item in items.OfType<ToolStripMenuItem>())
        {
            if (item?.Tag is not null && item.Tag.GetType().Equals(typeof(Eol)))
            {
                if (!Equals(item.Tag, eolMode))
                {
                    item.Checked = false;
                    continue;
                }
                item.Checked = true;
                lineReturnType.Text = Regex.Replace(item.Text, "&(.)", "$1");
                lineReturnType.Tag = item.Tag;
            }
        }
    }

    private void UpdatePos()
    {
        if (!StatusBar)
        {
            return;
        }
        position.Text = $"{Resources.Ln} {editor.Ln}, {Resources.Col} {editor.Col}"; // + $", {Resources.Pos} {editor.Pos}";
        position.ToolTipText = $"{Resources.Line}: {editor.Ln} \n{Resources.Column}: {editor.Col} \n{Resources.Position}: {editor.Pos} ";
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
            {               // Not holding shift
                if (first && (e.KeyState & 4) != 4)
                {
                    _ = OpenDocument(saveFirst: true, fileName: file);
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
            // Not holding shift
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
        if (_filename is not null && OpenDocument(saveFirst: false, fileName: _filename))
        {
            return;
        }
        _ = NewDocument(saveFirst: false);
    }

    private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        editorCut.Enabled
            = editorCopy.Enabled
            = editorDelete.Enabled
            = editorSearch.Enabled
            = editor.HasSelection;

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
            != DialogResult.Yes)
        {
            return;
        }
        editor.ConvertEols(editor.EolMode);
    }

    private void CursorPosition_Changed(object sender, UpdateUIEventArgs e)
    {
        UpdatePos();
    }

    private void Edit_DropDownClosed(object sender, EventArgs e)
    {
        /*
        // Less mess, but iterates over every Edit menu item.
        foreach (ToolStripMenuItem item in editMenu.DropDownItems.OfType<ToolStripMenuItem>())
        {
            item.Enabled = true;
        }
        */
        // Only enables items that were possibly previously disabled.
        // No iteration. More performant?
        editCut.Enabled
            = editCopy.Enabled
            = editDelete.Enabled
            = editSearch.Enabled
            = editPaste.Enabled
            = editUndo.Enabled
            = editRedo.Enabled
            = editFind.Enabled
            = editFindNext.Enabled
            = editFindPrevious.Enabled
            = true;
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
                editCut.Enabled
                    = editCopy.Enabled
                    = editDelete.Enabled
                    = editSearch.Enabled
                    = editor.HasSelection;

                editPaste.Enabled
                    = editor.CanPaste;
                editUndo.Enabled
                    = editor.CanUndo;
                editRedo.Enabled
                    = editor.CanRedo;
                editFind.Enabled
                    = editor.HasText;

                editFindNext.Enabled
                    = editFindPrevious.Enabled
                    = CanRepeatFind();
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
                _ = NewDocument();
                break;

            case "fileNewWindow":
                NewWindow();
                break;

            case "fileOpen":
                _ = OpenDocument();
                break;

            case "fileSave":
                _ = IsReadOnly
                    ? SaveConfirmPrompt(force: true, pendingAction: false)
                    : SaveDocument();
                break;

            case "fileSaveAs":
                _ = SaveDocument(saveAs: true);
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
                _ = editor.HasSelection
                    ? Process.Start(Defaults["SearchURI"] + WebUtility.UrlEncode(editor.SelectedText))
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
                GoTo();
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
                editor.WrapMode
                    = Conf.Toggle("WordWrap")
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
                _ = Process.Start((string)Defaults["HelpURL"]);
                break;

            case "helpSendFeedback":
                _ = Process.Start((string)Defaults["FeedbackURL"]);
                break;

            case "helpAbout":
                using (About about = new())
                {
                    _ = about.ShowDialog();
                }
                break;

            #endregion Help Menu

            default:
                break;
        }
    }

    private void ModifyAttempt(object sender, EventArgs e)
    {
        List<Button> buttons = new()
        {
            new()
            {
                Name = "okBtn",
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
            editor.ReadOnly ? "cancelBtn" : "okBtn");
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
                Conf.Set("StatusBar", statusBar.Visible = statusBarValue);
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
                Conf.Set("DarkMode", darkModeValue);
                break;

            case "EolSetting":
                Eol eolSetting = (Eol)after;
                editor.EolMode = eolSetting;
                Conf.Set("LineEnding", eolSetting);
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
                Conf.Set("ShowLineEndings", editor.ViewEol);
                break;
        }
    }

    private void Position_DoubleClick(object sender, EventArgs e)
    {
        GoTo();
    }

    private void PrintPage(object sender, PrintPageEventArgs e)
    {
        // Sets the value of charactersOnPage to the number of characters
        // of stringToPrint that will fit within the bounds of the page.
        _ = e.Graphics.MeasureString(_printString, editor.Font,
            e.MarginBounds.Size, StringFormat.GenericTypographic,
            out int charactersOnPage, out _); // linesPerPage unused

        // Draws the string within the bounds of the page
        e.Graphics.DrawString(_printString, editor.Font, Brushes.Black,
            e.MarginBounds, StringFormat.GenericTypographic);

        // Remove the portion of the string that has been printed.
        _printString = _printString.Substring(charactersOnPage);

        // Check to see if more pages are to be printed.
        e.HasMorePages = _printString.Length > 0;
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
        SetEncodingStatus(EncodingSetting = ((EncodingInfo)((ToolStripMenuItem)sender).Tag).GetEncoding(), detected: false);
    }

    private void SetEOL(object sender, EventArgs e)
    {
        ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
        EolSetting = (Eol)clickedItem.Tag;
    }

    private void ToggleDarkMode(object sender, EventArgs e)
    {
        DarkMode = !DarkMode;
    }

    private void ToggleShowLineEndings(object sender, EventArgs e)
    {
        ViewEol = !ViewEol;
    }

    private void UpdateTitle(object sender, EventArgs e)
    {
        Text = (editor.Modified ? "*" : "") + (IsFile ? Path.GetFileName(FileName) : FileName) + " - " + ProgramName;
    }

    private void Zoom_Changed(object sender, EventArgs e)
    {
        zoomLevel.Text = (100 + (editor.Zoom * 10)) + Resources.Percent;
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