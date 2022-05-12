using System;
using System.Windows.Forms;

namespace Bortpad
{
    public partial class FindPrompt : Form
    {
        private BortForm BortParent;

        public FindPrompt(string setQuery = "", bool setUp = false, bool setMatchCase = false, bool setWrapAround = false)
        {
            InitializeComponent();
            searchQuery.Text = setQuery;
            up.Checked = setUp;
            matchCase.Checked = setMatchCase;
            wrapAround.Checked = setWrapAround;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int findNext()
        {
            return BortParent.FindFromPrompt(searchQuery.Text, up.Checked, matchCase.Checked, wrapAround.Checked);
        }

        private void findNext_Click(object sender, EventArgs e)
        {
            findNext();
        }

        private void FindPrompt_Shown(object sender, EventArgs e)
        {
            BortParent = (BortForm)Owner;
        }

        private void FindPrompt_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                searchQuery.Select();
            }
        }

        private void searchQuery_TextChanged(object sender, EventArgs e)
        {
            findNextButton.Enabled = searchQuery.Text.Length > 0;
        }
    }
}