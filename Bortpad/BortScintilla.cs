using ScintillaNET;

namespace Bortpad
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows7.0")]
    public class BortScintilla : Scintilla
    {
        // [DllImport("user32.dll")]
        // private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public BortScintilla() : base()
        {
        }

        public int SelectionLength
        {
            get { return SelectionEnd - SelectionStart; }
        }
    }
}