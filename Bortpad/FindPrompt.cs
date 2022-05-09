using System;
using System.Windows.Forms;

namespace Bortpad
{
    public partial class FindPrompt : Form
    {
        private BortForm BortParent;
        public FindPrompt()
        {
            InitializeComponent();
        }
        private void FindPrompt_Shown(object sender, EventArgs e)
        {
            BortParent = (BortForm)Owner;
            searchQuery.Text = BortParent.getSearchQuery();
            up.Checked = BortParent.getSearchReverse();
            matchCase.Checked = BortParent.getSearchMatchCase();
            wrapAround.Checked = BortParent.getSearchWrapAround();
        }

        private int findNext()
        {
            return BortParent.findFromPrompt(searchQuery.Text, up.Checked, matchCase.Checked, wrapAround.Checked);
        }
        private void findNext_Click(object sender, EventArgs e)
        {
            findNext();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void searchQuery_TextChanged(object sender, EventArgs e)
        {
            findNextButton.Enabled = searchQuery.Text.Length > 0;
        }

        private void FindPrompt_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                searchQuery.Select();
            }
        }
    }
}
