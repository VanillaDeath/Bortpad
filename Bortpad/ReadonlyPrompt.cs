using System.Windows.Forms;

namespace Bortpad
{
    public partial class ReadonlyPrompt : Form
    {
        public ReadonlyPrompt()
        {
            InitializeComponent();
        }

        private void EditBtn_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void CancelBtn_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}