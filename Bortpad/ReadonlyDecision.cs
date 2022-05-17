using System.Windows.Forms;

namespace Bortpad
{
    public partial class ReadonlyDecision : Form
    {
        public ReadonlyDecision()
        {
            InitializeComponent();
            DontSaveButton.Select();
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void SaveAsButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void DontSaveButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void CancelBtn_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}