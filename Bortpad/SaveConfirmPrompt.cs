using System.Windows.Forms;

namespace Bortpad
{
    public partial class SaveConfirmPrompt : Form
    {
        public SaveConfirmPrompt(string filename = BortForm._DEFAULT_FILENAME, bool isReadOnly = false)
        {
            InitializeComponent();
            SaveQuestion.Text = "Do you want to save changes to " + filename + "?";
            if (isReadOnly)
            {
                SaveButton.Text = "Save...";
            }
        }
    }
}