using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

internal class PlainRichTextBox : RichTextBox
{
    private const int WM_USER = 0x400;
    private const int EM_SETTEXTMODE = WM_USER + 89;
    private const int EM_GETTEXTMODE = WM_USER + 90;
    private const int EM_LINELENGTH = 0x00c1;
    private const int EM_LINEINDEX = 0x00bb;

    // EM_SETTEXTMODE/EM_GETTEXTMODE flags
    private const int TM_PLAINTEXT = 1;

    private const int TM_RICHTEXT = 2;          // Default behavior
    private const int TM_SINGLELEVELUNDO = 4;
    private const int TM_MULTILEVELUNDO = 8;    // Default behavior
    private const int TM_SINGLECODEPAGE = 16;
    private const int TM_MULTICODEPAGE = 32;    // Default behavior

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

    private bool m_PlainTextMode;

    public event EventHandler CursorPositionChanged;

    // If this property doesn't work for you from the designer for some reason
    // (for example framework version...) then set this property from outside
    // the designer then uncomment the Browsable and DesignerSerializationVisibility
    // attributes and set the Property from your component initializer code
    // that runs after the designer's code.
    //  [Browsable(false)]
    //  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public bool PlainTextMode
    {
        get
        {
            return m_PlainTextMode;
        }
        set
        {
            m_PlainTextMode = value;
            if (IsHandleCreated)
            {
                IntPtr mode = value ? (IntPtr)TM_PLAINTEXT : (IntPtr)TM_RICHTEXT;
                SendMessage(Handle, EM_SETTEXTMODE, mode, IntPtr.Zero);
            }
        }
    }

    public int LineLength
    {
        get
        {
            return (int)SendMessage(Handle, EM_LINELENGTH, (IntPtr)LineIndex, IntPtr.Zero);
        }
    }

    public int LineIndex
    {
        get
        {
            return (int)SendMessage(Handle, EM_LINEINDEX, (IntPtr)(-1), IntPtr.Zero);
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        // For some reason it worked for me only if I manipulated the created
        // handle before calling the base method.
        PlainTextMode = m_PlainTextMode;
        base.OnHandleCreated(e);
    }

    protected virtual void OnCursorPositionChanged(EventArgs e)
    {
        if (CursorPositionChanged != null)
            CursorPositionChanged(this, e);
    }

    protected override void OnSelectionChanged(EventArgs e)
    {
        if (SelectionLength == 0)
            OnCursorPositionChanged(e);
        else
            base.OnSelectionChanged(e);
    }

    public int CurrentColumn
    {
        get { return CursorPosition.Column(this, SelectionStart); }
    }

    public int CurrentLine
    {
        get { return CursorPosition.Line(this, SelectionStart); }
    }

    public int CurrentPosition
    {
        get { return this.SelectionStart; }
    }

    public int SelectionEnd
    {
        get { return SelectionStart + SelectionLength; }
    }
}

internal class CursorPosition
{
    [System.Runtime.InteropServices.DllImport("user32")]
    public static extern int GetCaretPos(ref Point lpPoint);

    private static int GetCorrection(PlainRichTextBox e, int index)
    {
        Point pt1 = Point.Empty;
        GetCaretPos(ref pt1);
        Point pt2 = e.GetPositionFromCharIndex(index);

        if (pt1 != pt2)
            return 1;
        else
            return 0;
    }

    public static int Line(PlainRichTextBox e, int index)
    {
        int correction = GetCorrection(e, index);
        int ln = e.GetLineFromCharIndex(index) - correction;
        return ln < 0 ? 1 : ln + 1;
    }

    public static int Column(PlainRichTextBox e, int index1)
    {
        int correction = GetCorrection(e, index1);
        Point p = e.GetPositionFromCharIndex(index1 - correction);

        if (p.X == 1)
            return 1;

        p.X = 0;
        int index2 = e.GetCharIndexFromPosition(p);

        int col = index1 - index2;

        return col + 1;
    }
}