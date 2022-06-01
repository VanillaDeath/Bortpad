using ScintillaNET;

namespace WilsonUtils;

public class Bortilla : Scintilla
{
    // [DllImport("user32.dll")]
    // private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
    public Bortilla() : base()
    {
    }

    public int Col => GetColumn(CurrentPosition) + 1;

    public bool HasSelection => SelectionLength > 0;

    public bool HasText => TextLength > 0;

    public int Ln
    {
        get => CurrentLine + 1;
        internal set
        {
            if (value > NumLines || value < 1)
            {
                return;
            }
            Lines[value - 1].Goto();
        }
    }

    public int NumLines => Lines.Count < 1 ? 1 : Lines.Count;

    public int Pos
    {
        get => CurrentPosition + 1;
        internal set
        {
            if (value > TextLength || value < 0)
            {
                return;
            }
            CurrentPosition = value;
        }
    }

    public int SelectionLength => SelectionEnd - SelectionStart;

    public void Reset()
    {
        ReadOnly = false;
        ClearAll();
        ScrollWidth = 1;
    }
}