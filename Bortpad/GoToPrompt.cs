using System;
using System.Windows.Forms;

namespace Bortpad
{
    public partial class GoToPrompt : Form
    {
        // private BortForm BortParent;

        public event EventHandler GoToClick;

        public int OriginalLineNumber
        {
            get; private set;
        }

        public long LineNumber
        {
            get
            {
                string line = lineNumber.Text;
                if (line != null && line != "" && long.TryParse(line, out long result))
                {
                    return result;
                }
                return -1;
            }
            private set => lineNumber.Text = value.ToString();
        }

        public GoToPrompt(int setOrigLineNumber = 0)
        {
            InitializeComponent();
            OriginalLineNumber = setOrigLineNumber;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GoToButton_Click(object sender, EventArgs e)
        {
            GoToClick?.Invoke(this, EventArgs.Empty);
            InitField();
        }

        private void GoToPrompt_Shown(object sender, EventArgs e)
        {
            InitField();
        }

        private void InitField()
        {
            LineNumber = OriginalLineNumber;
            lineNumber.SelectAll();
        }
    }
}