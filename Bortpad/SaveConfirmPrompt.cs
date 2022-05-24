using System.Windows.Forms;

namespace Bortpad
{
    public partial class SaveConfirmPrompt : Form
    {
        private string _fileName;

        public SaveConfirmPrompt(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
            private set
            {
                SaveQuestion.Text = "Do you want to save changes to " + (_fileName = value) + "?";
            }
        }
    }
}