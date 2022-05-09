using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
