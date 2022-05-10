using System;
using System.Windows.Forms;

namespace Bortpad
{
    public partial class ReplacePrompt : Form
    {
        private BortForm BortParent;

        public ReplacePrompt(string setQuery = "", string setReplace = "", bool setMatchCase = false, bool setWrapAround = false)
        {
            InitializeComponent();
            searchQuery.Text = setQuery;
            replaceWith.Text = setReplace;
            matchCase.Checked = setMatchCase;
            wrapAround.Checked = setWrapAround;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int findNext()
        {
            return BortParent.findFromPrompt(searchQuery.Text, false, matchCase.Checked, wrapAround.Checked);
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

        private int replace()
        {
            return BortParent.replaceFromPrompt(searchQuery.Text, replaceWith.Text, matchCase.Checked, wrapAround.Checked);
        }

        private void replaceAll()
        {
            BortParent.replaceAll(searchQuery.Text, replaceWith.Text, matchCase.Checked, wrapAround.Checked);
        }

        private void replaceAllButton_Click(object sender, EventArgs e)
        {
            replaceAll();
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            replace();
        }

        private void searchQuery_TextChanged(object sender, EventArgs e)
        {
            bool hasText = searchQuery.Text.Length > 0;
            findNextButton.Enabled = hasText;
            replaceButton.Enabled = hasText;
            replaceAllButton.Enabled = hasText;
        }
    }
}