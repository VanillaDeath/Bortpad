
namespace Bortpad
{
    partial class Bortpad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bortpad));
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
            this.printDoc = new System.Drawing.Printing.PrintDocument();
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
            this.editor = new WilsonUtils.Bortilla();
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
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
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
            resources.ApplyResources(this.fileMenu, "fileMenu");
            // 
            // fileNew
            // 
            this.fileNew.Name = "fileNew";
            resources.ApplyResources(this.fileNew, "fileNew");
            this.fileNew.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileNewWindow
            // 
            this.fileNewWindow.Name = "fileNewWindow";
            resources.ApplyResources(this.fileNewWindow, "fileNewWindow");
            this.fileNewWindow.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileOpen
            // 
            this.fileOpen.Name = "fileOpen";
            resources.ApplyResources(this.fileOpen, "fileOpen");
            this.fileOpen.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileSave
            // 
            this.fileSave.Name = "fileSave";
            resources.ApplyResources(this.fileSave, "fileSave");
            this.fileSave.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileSaveAs
            // 
            this.fileSaveAs.Name = "fileSaveAs";
            resources.ApplyResources(this.fileSaveAs, "fileSaveAs");
            this.fileSaveAs.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileSeparator1
            // 
            this.fileSeparator1.Name = "fileSeparator1";
            resources.ApplyResources(this.fileSeparator1, "fileSeparator1");
            // 
            // filePageSetup
            // 
            this.filePageSetup.Name = "filePageSetup";
            resources.ApplyResources(this.filePageSetup, "filePageSetup");
            this.filePageSetup.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // filePrint
            // 
            this.filePrint.Name = "filePrint";
            resources.ApplyResources(this.filePrint, "filePrint");
            this.filePrint.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileSeparator2
            // 
            this.fileSeparator2.Name = "fileSeparator2";
            resources.ApplyResources(this.fileSeparator2, "fileSeparator2");
            // 
            // fileExit
            // 
            this.fileExit.Name = "fileExit";
            resources.ApplyResources(this.fileExit, "fileExit");
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
            resources.ApplyResources(this.editMenu, "editMenu");
            this.editMenu.DropDownClosed += new System.EventHandler(this.Edit_DropDownClosed);
            this.editMenu.DropDownOpening += new System.EventHandler(this.Menu_Opening);
            // 
            // editUndo
            // 
            this.editUndo.Name = "editUndo";
            resources.ApplyResources(this.editUndo, "editUndo");
            this.editUndo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editRedo
            // 
            this.editRedo.Name = "editRedo";
            resources.ApplyResources(this.editRedo, "editRedo");
            this.editRedo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editSeparator1
            // 
            this.editSeparator1.Name = "editSeparator1";
            resources.ApplyResources(this.editSeparator1, "editSeparator1");
            // 
            // editCut
            // 
            this.editCut.Name = "editCut";
            resources.ApplyResources(this.editCut, "editCut");
            this.editCut.Tag = "NeedsSelection";
            this.editCut.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editCopy
            // 
            this.editCopy.Name = "editCopy";
            resources.ApplyResources(this.editCopy, "editCopy");
            this.editCopy.Tag = "NeedsSelection";
            this.editCopy.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editPaste
            // 
            this.editPaste.Name = "editPaste";
            resources.ApplyResources(this.editPaste, "editPaste");
            this.editPaste.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editDelete
            // 
            this.editDelete.Name = "editDelete";
            resources.ApplyResources(this.editDelete, "editDelete");
            this.editDelete.Tag = "NeedsSelection";
            this.editDelete.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editSeparator2
            // 
            this.editSeparator2.Name = "editSeparator2";
            resources.ApplyResources(this.editSeparator2, "editSeparator2");
            // 
            // editSearch
            // 
            this.editSearch.Name = "editSearch";
            resources.ApplyResources(this.editSearch, "editSearch");
            this.editSearch.Tag = "NeedsSelection";
            this.editSearch.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editFind
            // 
            this.editFind.Name = "editFind";
            resources.ApplyResources(this.editFind, "editFind");
            this.editFind.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editFindNext
            // 
            this.editFindNext.Name = "editFindNext";
            resources.ApplyResources(this.editFindNext, "editFindNext");
            this.editFindNext.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editFindPrevious
            // 
            this.editFindPrevious.Name = "editFindPrevious";
            resources.ApplyResources(this.editFindPrevious, "editFindPrevious");
            this.editFindPrevious.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editReplace
            // 
            this.editReplace.Name = "editReplace";
            resources.ApplyResources(this.editReplace, "editReplace");
            this.editReplace.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editGoTo
            // 
            this.editGoTo.Name = "editGoTo";
            resources.ApplyResources(this.editGoTo, "editGoTo");
            this.editGoTo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editSeparator3
            // 
            this.editSeparator3.Name = "editSeparator3";
            resources.ApplyResources(this.editSeparator3, "editSeparator3");
            // 
            // editSelectAll
            // 
            this.editSelectAll.Name = "editSelectAll";
            resources.ApplyResources(this.editSelectAll, "editSelectAll");
            this.editSelectAll.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editTimeDate
            // 
            this.editTimeDate.Name = "editTimeDate";
            resources.ApplyResources(this.editTimeDate, "editTimeDate");
            this.editTimeDate.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // formatMenu
            // 
            this.formatMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatWordWrap,
            this.formatFont});
            this.formatMenu.Name = "formatMenu";
            resources.ApplyResources(this.formatMenu, "formatMenu");
            this.formatMenu.DropDownOpening += new System.EventHandler(this.Menu_Opening);
            // 
            // formatWordWrap
            // 
            this.formatWordWrap.Name = "formatWordWrap";
            resources.ApplyResources(this.formatWordWrap, "formatWordWrap");
            this.formatWordWrap.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // formatFont
            // 
            this.formatFont.Name = "formatFont";
            resources.ApplyResources(this.formatFont, "formatFont");
            this.formatFont.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewZoom,
            this.viewStatusBar});
            this.viewMenu.Name = "viewMenu";
            resources.ApplyResources(this.viewMenu, "viewMenu");
            this.viewMenu.DropDownOpening += new System.EventHandler(this.Menu_Opening);
            // 
            // viewZoom
            // 
            this.viewZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomIn,
            this.zoomOut,
            this.restoreDefaultZoom});
            this.viewZoom.Name = "viewZoom";
            resources.ApplyResources(this.viewZoom, "viewZoom");
            // 
            // zoomIn
            // 
            this.zoomIn.Name = "zoomIn";
            resources.ApplyResources(this.zoomIn, "zoomIn");
            this.zoomIn.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // zoomOut
            // 
            this.zoomOut.Name = "zoomOut";
            resources.ApplyResources(this.zoomOut, "zoomOut");
            this.zoomOut.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // restoreDefaultZoom
            // 
            this.restoreDefaultZoom.Name = "restoreDefaultZoom";
            resources.ApplyResources(this.restoreDefaultZoom, "restoreDefaultZoom");
            this.restoreDefaultZoom.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // viewStatusBar
            // 
            this.viewStatusBar.Checked = true;
            this.viewStatusBar.CheckOnClick = true;
            this.viewStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewStatusBar.Name = "viewStatusBar";
            resources.ApplyResources(this.viewStatusBar, "viewStatusBar");
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
            resources.ApplyResources(this.helpMenu, "helpMenu");
            // 
            // helpViewHelp
            // 
            this.helpViewHelp.Name = "helpViewHelp";
            resources.ApplyResources(this.helpViewHelp, "helpViewHelp");
            this.helpViewHelp.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // helpSendFeedback
            // 
            this.helpSendFeedback.Name = "helpSendFeedback";
            resources.ApplyResources(this.helpSendFeedback, "helpSendFeedback");
            this.helpSendFeedback.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // helpSeparator1
            // 
            this.helpSeparator1.Name = "helpSeparator1";
            resources.ApplyResources(this.helpSeparator1, "helpSeparator1");
            // 
            // helpAbout
            // 
            this.helpAbout.Name = "helpAbout";
            resources.ApplyResources(this.helpAbout, "helpAbout");
            this.helpAbout.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // darkMode
            // 
            this.darkMode.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.darkMode, "darkMode");
            this.darkMode.BackColor = System.Drawing.SystemColors.ControlText;
            this.darkMode.ForeColor = System.Drawing.SystemColors.Control;
            this.darkMode.Name = "darkMode";
            this.darkMode.Click += new System.EventHandler(this.ToggleDarkMode);
            // 
            // holdShiftNotice
            // 
            this.holdShiftNotice.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.holdShiftNotice, "holdShiftNotice");
            this.holdShiftNotice.Name = "holdShiftNotice";
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
            resources.ApplyResources(this.statusBar, "statusBar");
            this.statusBar.Name = "statusBar";
            this.statusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusBar.ShowItemToolTips = true;
            // 
            // statusBarLeft
            // 
            this.statusBarLeft.Name = "statusBarLeft";
            resources.ApplyResources(this.statusBarLeft, "statusBarLeft");
            this.statusBarLeft.Spring = true;
            // 
            // readOnlyNotice
            // 
            this.readOnlyNotice.ActiveLinkColor = System.Drawing.Color.LightCoral;
            resources.ApplyResources(this.readOnlyNotice, "readOnlyNotice");
            this.readOnlyNotice.IsLink = true;
            this.readOnlyNotice.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.readOnlyNotice.LinkColor = System.Drawing.SystemColors.ControlText;
            this.readOnlyNotice.Name = "readOnlyNotice";
            this.readOnlyNotice.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.readOnlyNotice.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.readOnlyNotice.Click += new System.EventHandler(this.ReadOnlyNotice_Click);
            this.readOnlyNotice.VisibleChanged += new System.EventHandler(this.ReadOnlyNotice_VisibleChanged);
            // 
            // position
            // 
            resources.ApplyResources(this.position, "position");
            this.position.DoubleClickEnabled = true;
            this.position.Name = "position";
            this.position.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.position.DoubleClick += new System.EventHandler(this.Position_DoubleClick);
            // 
            // zoomLevel
            // 
            resources.ApplyResources(this.zoomLevel, "zoomLevel");
            this.zoomLevel.DoubleClickEnabled = true;
            this.zoomLevel.Name = "zoomLevel";
            this.zoomLevel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.zoomLevel.DoubleClick += new System.EventHandler(this.ZoomLevel_DoubleClick);
            this.zoomLevel.MouseEnter += new System.EventHandler(this.ZoomLevel_MouseEnter);
            this.zoomLevel.MouseLeave += new System.EventHandler(this.ZoomLevel_MouseLeave);
            // 
            // lineReturnType
            // 
            resources.ApplyResources(this.lineReturnType, "lineReturnType");
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
            this.lineReturnType.Name = "lineReturnType";
            this.lineReturnType.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lineReturnType.ShowDropDownArrow = false;
            this.lineReturnType.DropDownOpening += new System.EventHandler(this.LineReturnType_DropDownOpening);
            // 
            // showLineEndings
            // 
            this.showLineEndings.Name = "showLineEndings";
            resources.ApplyResources(this.showLineEndings, "showLineEndings");
            this.showLineEndings.Click += new System.EventHandler(this.ToggleShowLineEndings);
            // 
            // convertLineEndings
            // 
            this.convertLineEndings.Name = "convertLineEndings";
            resources.ApplyResources(this.convertLineEndings, "convertLineEndings");
            this.convertLineEndings.Click += new System.EventHandler(this.ConvertLineEndings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // pressingEnterUses
            // 
            resources.ApplyResources(this.pressingEnterUses, "pressingEnterUses");
            this.pressingEnterUses.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pressingEnterUses.Name = "pressingEnterUses";
            // 
            // windowsLineFeed
            // 
            this.windowsLineFeed.Checked = true;
            this.windowsLineFeed.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.windowsLineFeed, "windowsLineFeed");
            this.windowsLineFeed.Name = "windowsLineFeed";
            this.windowsLineFeed.Tag = "CRLF";
            this.windowsLineFeed.Click += new System.EventHandler(this.SetEOL);
            // 
            // linuxLineFeed
            // 
            resources.ApplyResources(this.linuxLineFeed, "linuxLineFeed");
            this.linuxLineFeed.Name = "linuxLineFeed";
            this.linuxLineFeed.Tag = "LF";
            this.linuxLineFeed.Click += new System.EventHandler(this.SetEOL);
            // 
            // macLineFeed
            // 
            resources.ApplyResources(this.macLineFeed, "macLineFeed");
            this.macLineFeed.Name = "macLineFeed";
            this.macLineFeed.Tag = "CR";
            this.macLineFeed.Click += new System.EventHandler(this.SetEOL);
            // 
            // encodingStatus
            // 
            resources.ApplyResources(this.encodingStatus, "encodingStatus");
            this.encodingStatus.AutoToolTip = false;
            this.encodingStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.encodingStatus.Name = "encodingStatus";
            this.encodingStatus.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.encodingStatus.ShowDropDownArrow = false;
            // 
            // printDoc
            // 
            this.printDoc.DocumentName = "FileName";
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintPage);
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
            resources.ApplyResources(this.editorContextMenu, "editorContextMenu");
            this.editorContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
            // 
            // editorUndo
            // 
            resources.ApplyResources(this.editorUndo, "editorUndo");
            this.editorUndo.Name = "editorUndo";
            this.editorUndo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorRedo
            // 
            resources.ApplyResources(this.editorRedo, "editorRedo");
            this.editorRedo.Name = "editorRedo";
            this.editorRedo.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorSeparator1
            // 
            this.editorSeparator1.Name = "editorSeparator1";
            resources.ApplyResources(this.editorSeparator1, "editorSeparator1");
            // 
            // editorCut
            // 
            resources.ApplyResources(this.editorCut, "editorCut");
            this.editorCut.Name = "editorCut";
            this.editorCut.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorCopy
            // 
            resources.ApplyResources(this.editorCopy, "editorCopy");
            this.editorCopy.Name = "editorCopy";
            this.editorCopy.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorPaste
            // 
            resources.ApplyResources(this.editorPaste, "editorPaste");
            this.editorPaste.Name = "editorPaste";
            this.editorPaste.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorDelete
            // 
            resources.ApplyResources(this.editorDelete, "editorDelete");
            this.editorDelete.Name = "editorDelete";
            this.editorDelete.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorSeparator2
            // 
            this.editorSeparator2.Name = "editorSeparator2";
            resources.ApplyResources(this.editorSeparator2, "editorSeparator2");
            // 
            // editorSelectAll
            // 
            this.editorSelectAll.Name = "editorSelectAll";
            resources.ApplyResources(this.editorSelectAll, "editorSelectAll");
            this.editorSelectAll.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editorSeparator3
            // 
            this.editorSeparator3.Name = "editorSeparator3";
            resources.ApplyResources(this.editorSeparator3, "editorSeparator3");
            // 
            // editorSearch
            // 
            resources.ApplyResources(this.editorSearch, "editorSearch");
            this.editorSearch.Name = "editorSearch";
            this.editorSearch.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // editor
            // 
            this.editor.AllowDrop = true;
            this.editor.AutoCMaxHeight = 9;
            this.editor.BiDirectionality = ScintillaNET.BiDirectionalDisplayType.LeftToRight;
            this.editor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editor.BufferedDraw = false;
            this.editor.CaretLineBackColor = System.Drawing.Color.White;
            this.editor.CaretLineVisible = true;
            this.editor.ContextMenuStrip = this.editorContextMenu;
            resources.ApplyResources(this.editor, "editor");
            this.editor.LexerName = null;
            this.editor.Name = "editor";
            this.editor.ScrollWidth = 1;
            this.editor.TabIndents = true;
            this.editor.Technology = ScintillaNET.Technology.DirectWrite;
            this.editor.UseRightToLeftReadingLayout = false;
            this.editor.WrapMode = ScintillaNET.WrapMode.None;
            this.editor.ModifyAttempt += new System.EventHandler<System.EventArgs>(this.ModifyAttempt);
            this.editor.SavePointLeft += new System.EventHandler<System.EventArgs>(this.UpdateTitle);
            this.editor.SavePointReached += new System.EventHandler<System.EventArgs>(this.UpdateTitle);
            this.editor.UpdateUI += new System.EventHandler<ScintillaNET.UpdateUIEventArgs>(this.CursorPosition_Changed);
            this.editor.ZoomChanged += new System.EventHandler<System.EventArgs>(this.Zoom_Changed);
            // 
            // Bortpad
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editor);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Bortpad";
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
        private System.Drawing.Printing.PrintDocument printDoc;
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
        internal WilsonUtils.Bortilla editor;
    }
}

