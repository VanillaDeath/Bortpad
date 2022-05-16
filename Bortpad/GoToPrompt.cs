using System;
using System.Windows.Forms;

namespace Bortpad
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows7.0")]
    public partial class GoToPrompt : Form
    {
        private BortForm BortParent;
        private int origLineNumber;

        public GoToPrompt(int setOrigLineNumber = 0)
        {
            InitializeComponent();
            origLineNumber = setOrigLineNumber;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GoToButton_Click(object sender, EventArgs e)
        {
            if (BortParent.GoToLineFromPrompt(lineNumber.Text))
            {
                Close();
            }
            else
            {
                initField();
            }
        }

        private void GoToPrompt_Shown(object sender, EventArgs e)
        {
            BortParent = (BortForm)Owner;
            if (origLineNumber < 1)
            {
                origLineNumber = BortParent.Ln;
            }
            initField();
        }

        private void initField()
        {
            lineNumber.Text = origLineNumber.ToString();
            lineNumber.SelectAll();
        }
    }
}