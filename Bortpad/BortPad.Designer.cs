
namespace Bortpad
{
    partial class BortForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.filePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.filePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.fileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.editRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.editSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editCut = new System.Windows.Forms.ToolStripMenuItem();
            this.editCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.editPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.editDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.editSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.editFind = new System.Windows.Forms.ToolStripMenuItem();
            this.editFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.editFindPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.editReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.editGoTo = new System.Windows.Forms.ToolStripMenuItem();
            this.editSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.editTimeDate = new System.Windows.Forms.ToolStripMenuItem();
            this.formatMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.formatWordWrap = new System.Windows.Forms.ToolStripMenuItem();
            this.formatFont = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreDefaultZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpViewHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.helpSendFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.helpSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.darkMode = new System.Windows.Forms.ToolStripMenuItem();
            this.holdShiftNotice = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.readOnlyNotice = new System.Windows.Forms.ToolStripStatusLabel();
            this.position = new System.Windows.Forms.ToolStripStatusLabel();
            this.zoomLevel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lineReturnType = new System.Windows.Forms.ToolStripDropDownButton();
            this.showLineEndings = new System.Windows.Forms.ToolStripMenuItem();
            this.convertLineEndings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.pressingEnterUses = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsLineFeed = new System.Windows.Forms.ToolStripMenuItem();
            this.linuxLineFeed = new System.Windows.Forms.ToolStripMenuItem();
            this.macLineFeed = new System.Windows.Forms.ToolStripMenuItem();
            this.encodingStatus = new System.Windows.Forms.ToolStripDropDownButton();
            this.fontDlg = new System.Windows.Forms.FontDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.editorContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editorUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.editorRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.editorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editorCut = new System.Windows.Forms.ToolStripMenuItem();
            this.editorCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.editorPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.editorDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.editorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editorSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.editorSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editorSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.editor = new ScintillaNET.Scintilla();
            this.mainMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.editorContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.formatMenu,
            this.viewMenu,
            this.helpMenu,
            this.darkMode,
            this.holdShiftNotice});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mainMenu.Size = new System.Drawing.Size(1008, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "Main Menu";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNew,
            this.fileNewWindow,
            this.fileOpen,
            this.fileSave,
            this.fileSaveAs,
            this.fileSeparator1,
            this.filePageSetup,
            this.filePrint,
            this.fileSeparator2,
            this.fileExit});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // fileNew
            // 
            this.fileNew.Name = "fileNew";
            this.fileNew.ShortcutKeyDisplayString = "";
            this.fileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.fileNew.Size = new System.Drawing.Size(220, 22);
            this.fileNew.Text = "&New";
            this.fileNew.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileNewWindow
            // 
            this.fileNewWindow.Name = "fileNewWindow";
            this.fileNewWindow.ShortcutKeyDisplayString = "";
            this.fileNewWindow.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.fileNewWindow.Size = new System.Drawing.Size(220, 22);
            this.fileNewWindow.Text = "New &Window";
            this.fileNewWindow.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileOpen
            // 
            this.fileOpen.Name = "fileOpen";
            this.fileOpen.ShortcutKeyDisplayString = "";
            this.fileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileOpen.Size = new System.Drawing.Size(220, 22);
            this.fileOpen.Text = "&Open...";
            this.fileOpen.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileSave
            // 
            this.fileSave.Name = "fileSave";
            this.fileSave.ShortcutKeyDisplayString = "";
            this.fileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileSave.Size = new System.Drawing.Size(220, 22);
            this.fileSave.Text = "&Save";
            this.fileSave.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileSaveAs
            // 
            this.fileSaveAs.Name = "fileSaveAs";
            this.fileSaveAs.ShortcutKeyDisplayString = "";
            this.fileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.fileSaveAs.Size = new System.Drawing.Size(220, 22);
            this.fileSaveAs.Text = "Save &As...";
            this.fileSaveAs.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileSeparator1
            // 
            this.fileSeparator1.Name = "fileSeparator1";
            this.fileSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // filePageSetup
            // 
            this.filePageSetup.Name = "filePageSetup";
            this.filePageSetup.Size = new System.Drawing.Size(220, 22);
            this.filePageSetup.Text = "Page Set&up...";
            this.filePageSetup.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // filePrint
            // 
            this.filePrint.Name = "filePrint";
            this.filePrint.ShortcutKeyDisplayString = "";
            this.filePrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.filePrint.Size = new System.Drawing.Size(220, 22);
            this.filePrint.Text = "&Print...";
            this.filePrint.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileSeparator2
            // 
            this.fileSeparator2.Name = "fileSeparator2";
            this.fileSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // fileExit
            // 
            this.fileExit.Name = "fileExit";
            this.fileExit.Size = new System.Drawing.Size(220, 22);
            this.fileExit.Text = "E&xit";
            this.fileExit.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editUndo,
            this.editRedo,
            this.editSeparator1,
            this.editCut,
            this.editCopy,
            this.editPaste,
            this.editDelete,
            this.editSeparator2,
            this.editSearch,
            this.editFind,
            this.editFindNext,
            this.editFindPrevious,
            this.editReplace,
            this.editGoTo,
            this.editSeparator3,
            this.editSelectAll,
            this.editTimeDate});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(39, 20);
            this.editMenu.Text = "&Edit";
            this.editMenu.DropDownClosed += new System.EventHandler(this.Edit_DropDownClosed);
            this.editMenu.DropDownOpening += new System.EventHandler(this.Menu_Opening);
            // 
            // editUndo
            // 
            this.editUndo.Enabled = false;
            this.editUndo.Name = "editUndo";
            this.editUndo.ShortcutKeyDisplayString = "";
            this.editUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.editUndo.Size = new System.Drawing.Size(225, 22);
            this.editUndo.Text = "&Undo";
            this.editUndo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editRedo
            // 
            this.editRedo.Enabled = false;
            this.editRedo.Name = "editRedo";
            this.editRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.editRedo.Size = new System.Drawing.Size(225, 22);
            this.editRedo.Text = "R&edo";
            this.editRedo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editSeparator1
            // 
            this.editSeparator1.Name = "editSeparator1";
            this.editSeparator1.Size = new System.Drawing.Size(222, 6);
            // 
            // editCut
            // 
            this.editCut.Enabled = false;
            this.editCut.Name = "editCut";
            this.editCut.ShortcutKeyDisplayString = "";
            this.editCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.editCut.Size = new System.Drawing.Size(225, 22);
            this.editCut.Text = "Cu&t";
            this.editCut.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editCopy
            // 
            this.editCopy.Enabled = false;
            this.editCopy.Name = "editCopy";
            this.editCopy.ShortcutKeyDisplayString = "";
            this.editCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.editCopy.Size = new System.Drawing.Size(225, 22);
            this.editCopy.Text = "&Copy";
            this.editCopy.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editPaste
            // 
            this.editPaste.Enabled = false;
            this.editPaste.Name = "editPaste";
            this.editPaste.ShortcutKeyDisplayString = "";
            this.editPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.editPaste.Size = new System.Drawing.Size(225, 22);
            this.editPaste.Text = "&Paste";
            this.editPaste.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editDelete
            // 
            this.editDelete.Enabled = false;
            this.editDelete.Name = "editDelete";
            this.editDelete.ShortcutKeyDisplayString = "";
            this.editDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.editDelete.Size = new System.Drawing.Size(225, 22);
            this.editDelete.Text = "De&lete";
            this.editDelete.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editSeparator2
            // 
            this.editSeparator2.Name = "editSeparator2";
            this.editSeparator2.Size = new System.Drawing.Size(222, 6);
            // 
            // editSearch
            // 
            this.editSearch.Enabled = false;
            this.editSearch.Name = "editSearch";
            this.editSearch.ShortcutKeyDisplayString = "";
            this.editSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.editSearch.Size = new System.Drawing.Size(225, 22);
            this.editSearch.Text = "&Search with Google...";
            this.editSearch.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editFind
            // 
            this.editFind.Enabled = false;
            this.editFind.Name = "editFind";
            this.editFind.ShortcutKeyDisplayString = "";
            this.editFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.editFind.Size = new System.Drawing.Size(225, 22);
            this.editFind.Text = "&Find...";
            this.editFind.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editFindNext
            // 
            this.editFindNext.Enabled = false;
            this.editFindNext.Name = "editFindNext";
            this.editFindNext.ShortcutKeyDisplayString = "";
            this.editFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.editFindNext.Size = new System.Drawing.Size(225, 22);
            this.editFindNext.Text = "Find &Next";
            this.editFindNext.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editFindPrevious
            // 
            this.editFindPrevious.Enabled = false;
            this.editFindPrevious.Name = "editFindPrevious";
            this.editFindPrevious.ShortcutKeyDisplayString = "";
            this.editFindPrevious.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.editFindPrevious.Size = new System.Drawing.Size(225, 22);
            this.editFindPrevious.Text = "Find Pre&vious";
            this.editFindPrevious.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editReplace
            // 
            this.editReplace.Name = "editReplace";
            this.editReplace.ShortcutKeyDisplayString = "";
            this.editReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.editReplace.Size = new System.Drawing.Size(225, 22);
            this.editReplace.Text = "&Replace...";
            this.editReplace.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editGoTo
            // 
            this.editGoTo.Name = "editGoTo";
            this.editGoTo.ShortcutKeyDisplayString = "";
            this.editGoTo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.editGoTo.Size = new System.Drawing.Size(225, 22);
            this.editGoTo.Text = "&Go To...";
            this.editGoTo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editSeparator3
            // 
            this.editSeparator3.Name = "editSeparator3";
            this.editSeparator3.Size = new System.Drawing.Size(222, 6);
            // 
            // editSelectAll
            // 
            this.editSelectAll.Name = "editSelectAll";
            this.editSelectAll.ShortcutKeyDisplayString = "";
            this.editSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.editSelectAll.Size = new System.Drawing.Size(225, 22);
            this.editSelectAll.Text = "Select &All";
            this.editSelectAll.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editTimeDate
            // 
            this.editTimeDate.Name = "editTimeDate";
            this.editTimeDate.ShortcutKeyDisplayString = "";
            this.editTimeDate.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.editTimeDate.Size = new System.Drawing.Size(225, 22);
            this.editTimeDate.Text = "Time/&Date";
            this.editTimeDate.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // formatMenu
            // 
            this.formatMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatWordWrap,
            this.formatFont});
            this.formatMenu.Name = "formatMenu";
            this.formatMenu.Size = new System.Drawing.Size(57, 20);
            this.formatMenu.Text = "F&ormat";
            this.formatMenu.DropDownOpening += new System.EventHandler(this.Menu_Opening);
            // 
            // formatWordWrap
            // 
            this.formatWordWrap.Name = "formatWordWrap";
            this.formatWordWrap.Size = new System.Drawing.Size(134, 22);
            this.formatWordWrap.Text = "&Word Wrap";
            this.formatWordWrap.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // formatFont
            // 
            this.formatFont.Name = "formatFont";
            this.formatFont.Size = new System.Drawing.Size(134, 22);
            this.formatFont.Text = "&Font...";
            this.formatFont.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewZoom,
            this.viewStatusBar});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(44, 20);
            this.viewMenu.Text = "&View";
            this.viewMenu.DropDownOpening += new System.EventHandler(this.Menu_Opening);
            // 
            // viewZoom
            // 
            this.viewZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomIn,
            this.zoomOut,
            this.restoreDefaultZoom});
            this.viewZoom.Name = "viewZoom";
            this.viewZoom.Size = new System.Drawing.Size(126, 22);
            this.viewZoom.Text = "&Zoom";
            // 
            // zoomIn
            // 
            this.zoomIn.Name = "zoomIn";
            this.zoomIn.ShortcutKeyDisplayString = "Ctrl+Plus";
            this.zoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.zoomIn.Size = new System.Drawing.Size(229, 22);
            this.zoomIn.Text = "Zoom &In";
            this.zoomIn.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // zoomOut
            // 
            this.zoomOut.Name = "zoomOut";
            this.zoomOut.ShortcutKeyDisplayString = "Ctrl+Minus";
            this.zoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.zoomOut.Size = new System.Drawing.Size(229, 22);
            this.zoomOut.Text = "Zoom &Out";
            this.zoomOut.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // restoreDefaultZoom
            // 
            this.restoreDefaultZoom.Name = "restoreDefaultZoom";
            this.restoreDefaultZoom.ShortcutKeyDisplayString = "";
            this.restoreDefaultZoom.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.restoreDefaultZoom.Size = new System.Drawing.Size(229, 22);
            this.restoreDefaultZoom.Text = "&Restore Default Zoom";
            this.restoreDefaultZoom.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // viewStatusBar
            // 
            this.viewStatusBar.Checked = true;
            this.viewStatusBar.CheckOnClick = true;
            this.viewStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewStatusBar.Name = "viewStatusBar";
            this.viewStatusBar.Size = new System.Drawing.Size(126, 22);
            this.viewStatusBar.Text = "&Status Bar";
            this.viewStatusBar.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpViewHelp,
            this.helpSendFeedback,
            this.helpSeparator1,
            this.helpAbout});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // helpViewHelp
            // 
            this.helpViewHelp.Name = "helpViewHelp";
            this.helpViewHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpViewHelp.Size = new System.Drawing.Size(153, 22);
            this.helpViewHelp.Text = "View &Help";
            this.helpViewHelp.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // helpSendFeedback
            // 
            this.helpSendFeedback.Name = "helpSendFeedback";
            this.helpSendFeedback.Size = new System.Drawing.Size(153, 22);
            this.helpSendFeedback.Text = "Send &Feedback";
            this.helpSendFeedback.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // helpSeparator1
            // 
            this.helpSeparator1.Name = "helpSeparator1";
            this.helpSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // helpAbout
            // 
            this.helpAbout.Name = "helpAbout";
            this.helpAbout.Size = new System.Drawing.Size(153, 22);
            this.helpAbout.Text = "&About Bortpad";
            this.helpAbout.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // darkMode
            // 
            this.darkMode.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.darkMode.AutoSize = false;
            this.darkMode.BackColor = System.Drawing.SystemColors.ControlText;
            this.darkMode.ForeColor = System.Drawing.SystemColors.Control;
            this.darkMode.Name = "darkMode";
            this.darkMode.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.darkMode.Size = new System.Drawing.Size(20, 20);
            this.darkMode.Text = "🌙";
            this.darkMode.ToolTipText = "Dark Mode";
            this.darkMode.Click += new System.EventHandler(this.ToggleDarkMode);
            // 
            // holdShiftNotice
            // 
            this.holdShiftNotice.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.holdShiftNotice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.holdShiftNotice.Image = global::Bortpad.Properties.Resources.shift;
            this.holdShiftNotice.Name = "holdShiftNotice";
            this.holdShiftNotice.ShortcutKeyDisplayString = "Shift";
            this.holdShiftNotice.Size = new System.Drawing.Size(156, 20);
            this.holdShiftNotice.Text = "Open in New Window";
            this.holdShiftNotice.Visible = false;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLeft,
            this.readOnlyNotice,
            this.position,
            this.zoomLevel,
            this.lineReturnType,
            this.encodingStatus});
            this.statusBar.Location = new System.Drawing.Point(0, 536);
            this.statusBar.Name = "statusBar";
            this.statusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusBar.ShowItemToolTips = true;
            this.statusBar.Size = new System.Drawing.Size(1008, 25);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusBarLeft
            // 
            this.statusBarLeft.Name = "statusBarLeft";
            this.statusBarLeft.Size = new System.Drawing.Size(382, 20);
            this.statusBarLeft.Spring = true;
            this.statusBarLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // readOnlyNotice
            // 
            this.readOnlyNotice.ActiveLinkColor = System.Drawing.Color.LightCoral;
            this.readOnlyNotice.AutoSize = false;
            this.readOnlyNotice.Enabled = false;
            this.readOnlyNotice.Image = global::Bortpad.Properties.Resources._readonly;
            this.readOnlyNotice.IsLink = true;
            this.readOnlyNotice.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.readOnlyNotice.LinkColor = System.Drawing.SystemColors.ControlText;
            this.readOnlyNotice.Name = "readOnlyNotice";
            this.readOnlyNotice.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.readOnlyNotice.Size = new System.Drawing.Size(110, 20);
            this.readOnlyNotice.Text = "Read-Only";
            this.readOnlyNotice.Visible = false;
            this.readOnlyNotice.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.readOnlyNotice.Click += new System.EventHandler(this.ReadOnlyNotice_Click);
            this.readOnlyNotice.VisibleChanged += new System.EventHandler(this.ReadOnlyNotice_VisibleChanged);
            // 
            // position
            // 
            this.position.AutoSize = false;
            this.position.Name = "position";
            this.position.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.position.Size = new System.Drawing.Size(120, 20);
            this.position.Text = "Ln 1, Col 1";
            this.position.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // zoomLevel
            // 
            this.zoomLevel.AutoSize = false;
            this.zoomLevel.DoubleClickEnabled = true;
            this.zoomLevel.Name = "zoomLevel";
            this.zoomLevel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.zoomLevel.Size = new System.Drawing.Size(50, 20);
            this.zoomLevel.Text = "100%";
            this.zoomLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.zoomLevel.ToolTipText = "Scroll Up: Zoom In\r\nScroll Down: Zoom Out\r\nDouble Click: Restore Default Zoom";
            this.zoomLevel.DoubleClick += new System.EventHandler(this.ZoomLevel_DoubleClick);
            this.zoomLevel.MouseEnter += new System.EventHandler(this.ZoomLevel_MouseEnter);
            this.zoomLevel.MouseLeave += new System.EventHandler(this.ZoomLevel_MouseLeave);
            // 
            // lineReturnType
            // 
            this.lineReturnType.AutoSize = false;
            this.lineReturnType.AutoToolTip = false;
            this.lineReturnType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lineReturnType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLineEndings,
            this.convertLineEndings,
            this.toolStripSeparator4,
            this.pressingEnterUses,
            this.windowsLineFeed,
            this.linuxLineFeed,
            this.macLineFeed});
            this.lineReturnType.Image = global::Bortpad.Properties.Resources.windows;
            this.lineReturnType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lineReturnType.Name = "lineReturnType";
            this.lineReturnType.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lineReturnType.ShowDropDownArrow = false;
            this.lineReturnType.Size = new System.Drawing.Size(120, 23);
            this.lineReturnType.Text = "Windows (CRLF)";
            this.lineReturnType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // showLineEndings
            // 
            this.showLineEndings.Name = "showLineEndings";
            this.showLineEndings.Size = new System.Drawing.Size(205, 22);
            this.showLineEndings.Text = "&Show EOL Characters";
            this.showLineEndings.Click += new System.EventHandler(this.ShowLineEndings_Click);
            // 
            // convertLineEndings
            // 
            this.convertLineEndings.Name = "convertLineEndings";
            this.convertLineEndings.Size = new System.Drawing.Size(205, 22);
            this.convertLineEndings.Text = "&Convert to...";
            this.convertLineEndings.Click += new System.EventHandler(this.ConvertLineEndings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(202, 6);
            // 
            // pressingEnterUses
            // 
            this.pressingEnterUses.Enabled = false;
            this.pressingEnterUses.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.pressingEnterUses.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pressingEnterUses.Name = "pressingEnterUses";
            this.pressingEnterUses.Size = new System.Drawing.Size(205, 22);
            this.pressingEnterUses.Text = "Pressing Return uses:";
            // 
            // windowsLineFeed
            // 
            this.windowsLineFeed.Checked = true;
            this.windowsLineFeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.windowsLineFeed.Image = global::Bortpad.Properties.Resources.windows;
            this.windowsLineFeed.Name = "windowsLineFeed";
            this.windowsLineFeed.Size = new System.Drawing.Size(205, 22);
            this.windowsLineFeed.Tag = "CRLF";
            this.windowsLineFeed.Text = "&Windows (CRLF)";
            this.windowsLineFeed.Click += new System.EventHandler(this.SetEOL);
            // 
            // linuxLineFeed
            // 
            this.linuxLineFeed.Image = global::Bortpad.Properties.Resources.macos;
            this.linuxLineFeed.Name = "linuxLineFeed";
            this.linuxLineFeed.Size = new System.Drawing.Size(205, 22);
            this.linuxLineFeed.Tag = "LF";
            this.linuxLineFeed.Text = "&Linux/macOS (LF)";
            this.linuxLineFeed.Click += new System.EventHandler(this.SetEOL);
            // 
            // macLineFeed
            // 
            this.macLineFeed.Image = global::Bortpad.Properties.Resources.os9;
            this.macLineFeed.Name = "macLineFeed";
            this.macLineFeed.Size = new System.Drawing.Size(205, 22);
            this.macLineFeed.Tag = "CR";
            this.macLineFeed.Text = "Legacy &Mac (CR)";
            this.macLineFeed.Click += new System.EventHandler(this.SetEOL);
            // 
            // encodingStatus
            // 
            this.encodingStatus.AutoSize = false;
            this.encodingStatus.AutoToolTip = false;
            this.encodingStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.encodingStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.encodingStatus.Name = "encodingStatus";
            this.encodingStatus.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.encodingStatus.ShowDropDownArrow = false;
            this.encodingStatus.Size = new System.Drawing.Size(180, 23);
            this.encodingStatus.Text = "UTF-8";
            this.encodingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fontDlg
            // 
            this.fontDlg.ShowEffects = false;
            this.fontDlg.ShowHelp = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "Text files|*.txt|All files|*.*";
            this.saveFileDialog1.Title = "Save File As...";
            // 
            // printDialog1
            // 
            this.printDialog1.AllowSelection = true;
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintPage);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.Filter = "Text Files|*.txt|All files|*.*";
            this.openFileDialog1.Title = "Open File...";
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // editorContextMenu
            // 
            this.editorContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editorUndo,
            this.editorRedo,
            this.editorSeparator1,
            this.editorCut,
            this.editorCopy,
            this.editorPaste,
            this.editorDelete,
            this.editorSeparator2,
            this.editorSelectAll,
            this.editorSeparator3,
            this.editorSearch});
            this.editorContextMenu.Name = "editorContextMenu";
            this.editorContextMenu.Size = new System.Drawing.Size(186, 198);
            this.editorContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
            // 
            // editorUndo
            // 
            this.editorUndo.Enabled = false;
            this.editorUndo.Name = "editorUndo";
            this.editorUndo.Size = new System.Drawing.Size(185, 22);
            this.editorUndo.Text = "&Undo";
            this.editorUndo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorRedo
            // 
            this.editorRedo.Enabled = false;
            this.editorRedo.Name = "editorRedo";
            this.editorRedo.Size = new System.Drawing.Size(185, 22);
            this.editorRedo.Text = "R&edo";
            this.editorRedo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorSeparator1
            // 
            this.editorSeparator1.Name = "editorSeparator1";
            this.editorSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // editorCut
            // 
            this.editorCut.Enabled = false;
            this.editorCut.Name = "editorCut";
            this.editorCut.Size = new System.Drawing.Size(185, 22);
            this.editorCut.Text = "Cu&t";
            this.editorCut.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorCopy
            // 
            this.editorCopy.Enabled = false;
            this.editorCopy.Name = "editorCopy";
            this.editorCopy.Size = new System.Drawing.Size(185, 22);
            this.editorCopy.Text = "&Copy";
            this.editorCopy.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorPaste
            // 
            this.editorPaste.Enabled = false;
            this.editorPaste.Name = "editorPaste";
            this.editorPaste.Size = new System.Drawing.Size(185, 22);
            this.editorPaste.Text = "&Paste";
            this.editorPaste.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorDelete
            // 
            this.editorDelete.Enabled = false;
            this.editorDelete.Name = "editorDelete";
            this.editorDelete.Size = new System.Drawing.Size(185, 22);
            this.editorDelete.Text = "&Delete";
            this.editorDelete.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorSeparator2
            // 
            this.editorSeparator2.Name = "editorSeparator2";
            this.editorSeparator2.Size = new System.Drawing.Size(182, 6);
            // 
            // editorSelectAll
            // 
            this.editorSelectAll.Name = "editorSelectAll";
            this.editorSelectAll.Size = new System.Drawing.Size(185, 22);
            this.editorSelectAll.Text = "Select &All";
            this.editorSelectAll.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorSeparator3
            // 
            this.editorSeparator3.Name = "editorSeparator3";
            this.editorSeparator3.Size = new System.Drawing.Size(182, 6);
            // 
            // editorSearch
            // 
            this.editorSearch.Enabled = false;
            this.editorSearch.Name = "editorSearch";
            this.editorSearch.Size = new System.Drawing.Size(185, 22);
            this.editorSearch.Text = "Search with &Google...";
            this.editorSearch.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editor
            // 
            this.editor.AutoCMaxHeight = 9;
            this.editor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editor.BufferedDraw = false;
            this.editor.ContextMenuStrip = this.editorContextMenu;
            this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.editor.Location = new System.Drawing.Point(0, 24);
            this.editor.Name = "editor";
            this.editor.ScrollWidth = 1;
            this.editor.Size = new System.Drawing.Size(1008, 512);
            this.editor.TabIndex = 0;
            this.editor.Technology = ScintillaNET.Technology.DirectWrite;
            this.editor.ModifyAttempt += new System.EventHandler<System.EventArgs>(this.ModifyAttempt);
            this.editor.SavePointLeft += new System.EventHandler<System.EventArgs>(this.UpdateTitle);
            this.editor.SavePointReached += new System.EventHandler<System.EventArgs>(this.UpdateTitle);
            this.editor.UpdateUI += new System.EventHandler<ScintillaNET.UpdateUIEventArgs>(this.CursorPosition_Changed);
            this.editor.ZoomChanged += new System.EventHandler<System.EventArgs>(this.Zoom_Changed);
            this.editor.TextChanged += new System.EventHandler(this.Modified);
            // 
            // BortForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.editor);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.Icon = global::Bortpad.Properties.Resources.bortpad;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "BortForm";
            this.Text = "Bortpad";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BortForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BortForm_FormClosed);
            this.Shown += new System.EventHandler(this.BortForm_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.BortForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.BortForm_DragOver);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.BortForm_DragOver);
            this.DragLeave += new System.EventHandler(this.BortForm_DragLeave);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.editorContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem fileExit;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripMenuItem fileNew;
        private System.Windows.Forms.ToolStripMenuItem fileNewWindow;
        private System.Windows.Forms.ToolStripMenuItem fileOpen;
        private System.Windows.Forms.ToolStripMenuItem fileSave;
        private System.Windows.Forms.ToolStripMenuItem fileSaveAs;
        private System.Windows.Forms.ToolStripSeparator fileSeparator1;
        private System.Windows.Forms.ToolStripMenuItem filePageSetup;
        private System.Windows.Forms.ToolStripMenuItem filePrint;
        private System.Windows.Forms.ToolStripSeparator fileSeparator2;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem formatMenu;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem formatWordWrap;
        private System.Windows.Forms.ToolStripMenuItem formatFont;
        private System.Windows.Forms.ToolStripMenuItem viewZoom;
        private System.Windows.Forms.ToolStripMenuItem zoomIn;
        private System.Windows.Forms.ToolStripMenuItem zoomOut;
        private System.Windows.Forms.ToolStripMenuItem restoreDefaultZoom;
        private System.Windows.Forms.ToolStripMenuItem viewStatusBar;
        private System.Windows.Forms.ToolStripMenuItem editUndo;
        private System.Windows.Forms.ToolStripSeparator editSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editCut;
        private System.Windows.Forms.ToolStripMenuItem editCopy;
        private System.Windows.Forms.ToolStripMenuItem editPaste;
        private System.Windows.Forms.ToolStripMenuItem editDelete;
        private System.Windows.Forms.ToolStripSeparator editSeparator2;
        private System.Windows.Forms.ToolStripMenuItem editSearch;
        private System.Windows.Forms.ToolStripMenuItem editGoTo;
        private System.Windows.Forms.ToolStripSeparator editSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editSelectAll;
        private System.Windows.Forms.ToolStripMenuItem editTimeDate;
        private System.Windows.Forms.ToolStripMenuItem helpViewHelp;
        private System.Windows.Forms.ToolStripMenuItem helpSendFeedback;
        private System.Windows.Forms.ToolStripSeparator helpSeparator1;
        private System.Windows.Forms.ToolStripMenuItem helpAbout;
        private System.Windows.Forms.ToolStripStatusLabel position;
        private System.Windows.Forms.ToolStripStatusLabel zoomLevel;
        private System.Windows.Forms.FontDialog fontDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLeft;
        internal System.Windows.Forms.ToolStripMenuItem editFind;
        internal System.Windows.Forms.ToolStripMenuItem editFindNext;
        internal System.Windows.Forms.ToolStripMenuItem editFindPrevious;
        internal System.Windows.Forms.ToolStripMenuItem editReplace;
        private System.Windows.Forms.ContextMenuStrip editorContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editorUndo;
        private System.Windows.Forms.ToolStripSeparator editorSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editorCut;
        private System.Windows.Forms.ToolStripMenuItem editorCopy;
        private System.Windows.Forms.ToolStripMenuItem editorPaste;
        private System.Windows.Forms.ToolStripMenuItem editorDelete;
        private System.Windows.Forms.ToolStripSeparator editorSeparator2;
        private System.Windows.Forms.ToolStripMenuItem editorSelectAll;
        private System.Windows.Forms.ToolStripSeparator editorSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editorSearch;
        private System.Windows.Forms.ToolStripMenuItem editRedo;
        private System.Windows.Forms.ToolStripMenuItem editorRedo;
        private System.Windows.Forms.ToolStripMenuItem darkMode;
        private System.Windows.Forms.ToolStripMenuItem holdShiftNotice;
        private ScintillaNET.Scintilla editor;
        private System.Windows.Forms.ToolStripDropDownButton encodingStatus;
        private System.Windows.Forms.ToolStripDropDownButton lineReturnType;
        private System.Windows.Forms.ToolStripMenuItem windowsLineFeed;
        private System.Windows.Forms.ToolStripMenuItem linuxLineFeed;
        private System.Windows.Forms.ToolStripMenuItem macLineFeed;
        private System.Windows.Forms.ToolStripMenuItem showLineEndings;
        private System.Windows.Forms.ToolStripMenuItem convertLineEndings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem pressingEnterUses;
        private System.Windows.Forms.ToolStripStatusLabel readOnlyNotice;
    }
}

