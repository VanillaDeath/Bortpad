# Bortpad
**Bortpad** is a reproduction of a *familiar*😉 simple Windows-based text editor.  
It is primarily a C# learning project for myself.  


## Extra Features
🔙 Multiple undo levels  
🔁 Redo  
🔍 Search with Google instead of the other one  
🌙 Simple dark mode  
📚 Multi-file drag-and-drop to open


## Missing Features
🌍 Localization  
🔤 Support for non-UTF-8 character encodings  


## Known Issues
⛔ Page Setup doesn't work (throws exception).  
⏳ Print dialog takes a long time to appear  
🖨️ No word wrap when printing  

---

## Technical Details
Bortpad is a C# WinForms application, built to closely match the built-in *Windows 10* text editor.  
- Like its inspiration, Bortpad supports **basic file editing** (New, Open, Save), changing the textbox font, text zoom, word wrap, a basic status bar, and simple find and replace functions.  
- It uses a specialized version of the **RichTextBox** control that disables rich text, but maintains text zoom, multiple undo levels, and redo functionality. Currently it is hard-coded to use UTF-8 encoding only.  
- The **status bar** shows line number, column number, zoom level, etc.  
- **Find and replace** dialogs are non-modal but stay in front, as expected.  
- The **Go To** prompt uses a specialized version of the **TextBox** control that only accepts numeric input, and displays a balloon-tip when other characters are entered.  
- It will keep track of the same **settings** between uses: font style, word wrap, show statusbar, find/replace settings.  
- A simple **dark mode** that swaps the editor pallette is implemented (setting maintained between uses).  
- Search web for selection is hard-coded to *Google* instead of a competitor's search engine.  
- Supports command-line argument to open file.  
- Supports text drag-and-drop on editor, file drag-and-drop on window.  
- All language strings are hard-coded in English-only for now.  
- Printing is still a work in progress.  