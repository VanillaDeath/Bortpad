using System;
using System.Windows.Forms;

namespace Bortpad
{
    public partial class ReplacePrompt : Form
    {
        private BortForm BortParent;
        public ReplacePrompt()
        {
            InitializeComponent();
        }
        private void FindPrompt_Shown(object sender, EventArgs e)
        {
            BortParent = (BortForm)Owner;
            searchQuery.Text = BortParent.getSearchQuery();
            replaceWith.Text = BortParent.getReplaceString();
            matchCase.Checked = BortParent.getSearchMatchCase();
            wrapAround.Checked = BortParent.getSearchWrapAround();
        }

        private int findNext()
        {
            return BortParent.findFromPrompt(searchQuery.Text, false, matchCase.Checked, wrapAround.Checked);
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
            bool hasText = searchQuery.Text.Length > 0;
            findNextButton.Enabled = hasText;
            replaceButton.Enabled = hasText;
            replaceAllButton.Enabled = hasText;
        }

        private void FindPrompt_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                searchQuery.Select();
            }
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            replace();
        }
        private int replace()
        {
            return BortParent.replaceFromPrompt(searchQuery.Text, replaceWith.Text, matchCase.Checked, wrapAround.Checked);
        }

        private void replaceAllButton_Click(object sender, EventArgs e)
        {
            replaceAll();
        }
        private void replaceAll()
        {
            BortParent.replaceAll(searchQuery.Text, replaceWith.Text, matchCase.Checked, wrapAround.Checked);
        }
    }
}
