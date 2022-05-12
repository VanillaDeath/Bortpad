﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bortpad
{
    internal class NumBox : TextBox
    {
        private const int WM_PASTE = 0x0302;
        private ToolTip err;

        [DllImport("user32.dll")]
        private static extern bool GetCaretPos(out Point lpPoint);

        public NumBox() : base()
        {
            err = new ToolTip();
            err.IsBalloon = true;
            err.ToolTipIcon = ToolTipIcon.Error;
            err.ToolTipTitle = "Unacceptable Character";
        }

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
                else
                {
                    errorBubble();
                }
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (char.IsLetter(e.KeyChar) ||
                char.IsSymbol(e.KeyChar) ||
                char.IsWhiteSpace(e.KeyChar) ||
                char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
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
        }

        protected void errorBubble()
        {
            GetCaretPos(out Point pt);
            err.Show(String.Empty, this, pt.X, pt.Y + 13, 1);
            err.Show("You can only type a number here.", this, pt.X, pt.Y + 13, 10000);
        }
    }
}