using ScintillaNET;

namespace WilsonUtils;

public class Bortilla : Scintilla
{
    // [DllImport("user32.dll")]
    // private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
    public Bortilla() : base()
    {
    }

    public int Col
    {
        get => GetColumn(CurrentPosition) + 1;
        internal set
        {
            // if < 1, go to start of line
            if (value < 1)
            {
                Lines[CurrentLine].Goto();
                return;
            }

            // Get (0-based) position on current line based on (1-based) column given
            int newPos = Lines[CurrentLine].Position + value - 1;
            // If beyond end of line, go to end of line
            if (LineFromPosition(newPos) > CurrentLine)
            {
                GotoPosition(Lines[CurrentLine].Position + Lines[CurrentLine].Length);
                return;
            }

            // Otherwise, go to column position as calculated
            GotoPosition(newPos);
        }
    }

    public bool HasSelection => SelectionLength > 0;

    public bool HasText => TextLength > 0;

    public int Ln
    {
        get => CurrentLine + 1;
        internal set
        {
            if (value > NumLines)
            {
                Lines[NumLines - 1].Goto();
                return;
            }
            if (value < 1)
            {
                Lines[0].Goto();
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