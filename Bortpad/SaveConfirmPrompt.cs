using System.Windows.Forms;

namespace Bortpad
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows7.0")]
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