using System;
using System.Windows.Forms;

namespace Bortpad
{
    public partial class ReplacePrompt : Form
    {
        public event EventHandler FindClick;

        public event EventHandler ReplaceClick;

        public event EventHandler ReplaceAllClick;

        public string SearchQuery
        {
            get => searchQuery.Text;
            private set => searchQuery.Text = value;
        }

        public string ReplaceWith
        {
            get => replaceWith.Text;
            private set => replaceWith.Text = value;
        }

        public bool MatchCase
        {
            get => matchCase.Checked;
            private set => matchCase.Checked = value;
        }

        public bool WrapAround
        {
            get => wrapAround.Checked;
            private set => wrapAround.Checked = value;
        }

        public ReplacePrompt(string setQuery = "", string setReplace = "", bool setMatchCase = false, bool setWrapAround = false)
        {
            InitializeComponent();
            SearchQuery = setQuery;
            ReplaceWith = setReplace;
            MatchCase = setMatchCase;
            WrapAround = setWrapAround;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FindNext_Click(object sender, EventArgs e)
        {
            FindClick?.Invoke(this, e);
        }

        private void FindPrompt_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                searchQuery.Select();
            }
        }

        private void ReplaceAllButton_Click(object sender, EventArgs e)
        {
            ReplaceAllClick?.Invoke(this, EventArgs.Empty);
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            ReplaceClick?.Invoke(this, EventArgs.Empty);
        }

        private void SearchQuery_TextChanged(object sender, EventArgs e)
        {
            bool hasText = SearchQuery.Length > 0;
            findNextButton.Enabled = hasText;
            replaceButton.Enabled = hasText;
            replaceAllButton.Enabled = hasText;
        }
    }
}