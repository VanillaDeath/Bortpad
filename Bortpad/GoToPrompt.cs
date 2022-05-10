using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bortpad
{
    public partial class GoToPrompt : Form
    {
        BortForm BortParent;
        int origLineNumber;
        ToolTip err;
        [DllImport("user32.dll")]
        static extern bool GetCaretPos(out Point lpPoint);
        public GoToPrompt(int setOrigLineNumber = 0)
        {
            InitializeComponent();
            origLineNumber = setOrigLineNumber;
            err = new ToolTip();
            err.IsBalloon = true;
            err.ToolTipIcon = ToolTipIcon.Error;
            err.ToolTipTitle = "Unacceptable Character";
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
                char.IsSymbol(e.KeyChar) ||
                char.IsWhiteSpace(e.KeyChar) ||
                char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // MessageBox.Show(e.KeyCode + " " + (char)e.KeyCode); // DEBUG
            //Allow navigation keyboard arrows
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Delete:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    e.SuppressKeyPress = false;
                    return;
                default:
                    break;
            }

            //Block non-number characters
            char currentKey = (char)e.KeyCode;
            bool modifier = e.Control || e.Alt || e.Shift;
            bool nonNumber = char.IsLetter(currentKey) ||
                             char.IsSymbol(currentKey) ||
                             char.IsWhiteSpace(currentKey) ||
                             char.IsPunctuation(currentKey);

            if (!modifier && nonNumber)
            {
                errorBubble();
                e.SuppressKeyPress = true;
            }

            //Handle pasted Text
            if (e.Control && e.KeyCode == Keys.V)
            {
                //Preview paste data (removing non-number characters)
                string pasteText = Clipboard.GetText();
                string strippedText = "";
                for (int i = 0; i < pasteText.Length; i++)
                {
                    if (char.IsDigit(pasteText[i]))
                        strippedText += pasteText[i].ToString();
                }

                if (strippedText != pasteText)
                {
                    //There were non-numbers in the pasted text
                    errorBubble();
                    e.SuppressKeyPress = true;
                }
                else
                    e.SuppressKeyPress = false;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GoToButton_Click(object sender, EventArgs e)
        {
            // LineNumber = int.Parse(lineNumber.Text);
            if (BortParent.goToLineFromPrompt(lineNumber.Text))
            {
                Close();
            } else
            {
                initField();
            }
        }

        private void GoToPrompt_Shown(object sender, EventArgs e)
        {
            BortParent = (BortForm)Owner;
            if (origLineNumber < 1)
            {
                origLineNumber = BortParent.getLineNumber();
            }
            initField();
            err.Show(String.Empty, lineNumber, 0);
        }

        private void initField()
        {
            lineNumber.Text = origLineNumber.ToString();
            lineNumber.SelectAll();
        }
        private void errorBubble()
        {
            GetCaretPos(out Point pt);
            err.Show("You can only type a number here.", lineNumber, pt.X, pt.Y + 13, 10000);
        }
    }
}
