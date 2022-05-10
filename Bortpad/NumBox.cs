using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class NumBox : TextBox
{
    private const int WM_PASTE = 0x0302;

    protected override void WndProc(ref Message m)
    {
        if (m.Msg != WM_PASTE)
        {
            // Handle all other messages normally
            base.WndProc(ref m);
        }
        else
        {
            if (double.TryParse(Clipboard.GetText(), out double value))
            {
                // Text = value.ToString();
                // SelectedText = value.ToString();
                base.WndProc(ref m);
            }
        }
    }
}
