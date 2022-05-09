using System.Windows.Forms;

namespace Bortpad
{
    public partial class SaveConfirmPrompt : Form
    {
        public SaveConfirmPrompt(string filename = "Untitiled")
        {
            InitializeComponent();
            SaveQuestion.Text = "Do you want to save changes to " + filename + "?";
        }

    }
}
