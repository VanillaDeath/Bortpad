# Bortpad
**Bortpad** is a reproduction of a *familiar*😉 simple Windows-based text editor.  
It is primarily a C# learning project for myself.  


## Extra Features
🔙 Multiple undo levels  
🔁 Redo  
🔍 Search with Google instead of the other one  
🌙 Simple dark mode  
📚 Multi-file drag-and-drop to open  


---

## Technical Details
Bortpad is a C# WinForms application, built on .NET Framework 4.8, designed to closely match the built-in *Windows 10* text editor.  
- Like its inspiration, Bortpad supports **basic file editing** (New, Open, Save), changing the textbox font, text zoom, word wrap, a basic status bar, and simple find and replace functions.  
- It uses the **[Scintilla.NET](https://github.com/VPKSoft/ScintillaNET) component** for text editing.  
- The **status bar** shows line number, column number, zoom level, etc.  
- **Find and replace** dialogs are non-modal but stay in front, as expected.  
- The **Go To** prompt uses a specialized version of the **TextBox** control that only accepts numeric input, and displays a balloon-tip when other characters are entered.  
- It will keep track of the same **settings** between uses: font style, word wrap, show statusbar, find/replace settings.  
- A simple **dark mode** that swaps the editor pallette is implemented (setting maintained between uses).  
- Search web for selection is hard-coded to *Google* instead of a competitor's search engine.  
- Supports command-line argument to open file.  
- Supports text drag-and-drop on editor, file drag-and-drop on window.  
- Localized to English, French, and Japanese (v1.0.5).  

---

## Releases
* ### [v1.0.6](https://github.com/VanillaDeath/Bortpad/releases/tag/v1.0.6)
> - Fixed printing
* ### [v1.0.5](https://github.com/VanillaDeath/Bortpad/releases/tag/v1.0.5)
> - French localization
> - Japanese localization
* ### v1.0.4
> Skipped (due to compiler error that needed version increase to fix)
* ### [v1.0.3](https://github.com/VanillaDeath/Bortpad/releases/tag/v1.0.3)
> - Reverted to .NET Framework 4.8 since the Scintilla custom control wouldn't co-operate with .NET Core's designer
> - A lot of code clean-up
> - Decoupled Find/Replace/Goto prompts from main class using events
* ### [v1.0.2](https://github.com/VanillaDeath/Bortpad/releases/tag/v1.0.2)
> * Converted from legacy App.config API (built into .NET Framework) to [SharpConfig project](https://github.com/cemdervis/SharpConfig) (.NET Core compatible)
> * Built as single exe
> * Fixes and improvements
* ### [v1.0.1](https://github.com/VanillaDeath/Bortpad/releases/tag/v1.0.1)
> * Converted custom RichTextBox implementation to [Scintilla.NET](https://github.com/jacobslusser/ScintillaNET) component, much better to work with.
> * Using C# Properties
> * Text encoding detection working (thanks to [Mozilla's UTF-unknown project](https://github.com/CharsetDetector/UTF-unknown))
* ### [v1.0.0](https://github.com/VanillaDeath/Bortpad/releases/tag/v1.0.0)
> Initial release.  



[All Releases](https://github.com/VanillaDeath/Bortpad/releases)