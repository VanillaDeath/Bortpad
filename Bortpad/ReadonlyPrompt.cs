﻿using System.Windows.Forms;

namespace Bortpad
{
    public partial class ReadonlyPrompt : Form
    {
        public ReadonlyPrompt(bool currentlyReadOnly = false)
        {
            InitializeComponent();
            if (!currentlyReadOnly)
            {
                editBtn.Text = "OK";
                buttonPanel.Controls.Remove(cancelBtn);
            }
            else
            {
                cancelBtn.Select();
            }
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