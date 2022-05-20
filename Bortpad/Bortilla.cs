using ScintillaNET;

namespace Bortpad
{
    public class Bortilla : Scintilla
    {
        // [DllImport("user32.dll")]
        // private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public Bortilla() : base()
        {
        }

        public int SelectionLength
        {
            get { return SelectionEnd - SelectionStart; }
        }
    }
}