namespace Bortpad
{
    public class BortScintilla : ScintillaNET.Scintilla
    {
        private const int STYLE_DEFAULT = 32;

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