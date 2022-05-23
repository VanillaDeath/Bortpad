using System;
using System.Windows.Forms;

namespace Bortpad
{
    public partial class FindPrompt : Form
    {
        // private BortForm BortParent;
        public event EventHandler FindClick;

        public string SearchQuery
        {
            get => searchQuery.Text;
            private set => searchQuery.Text = value;
        }

        public bool Up
        {
            get => up.Checked;
            private set => up.Checked = value;
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

        public FindPrompt(string setQuery = "", bool setUp = false, bool setMatchCase = false, bool setWrapAround = false)
        {
            InitializeComponent();
            SearchQuery = setQuery;
            Up = setUp;
            MatchCase = setMatchCase;
            WrapAround = setWrapAround;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void findNext_Click(object sender, EventArgs e)
        {
            // findNext();
            FindClick?.Invoke(this, EventArgs.Empty);
        }

        private void FindPrompt_Shown(object sender, EventArgs e)
        {
            // BortParent = (BortForm)Owner;
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
            findNextButton.Enabled = SearchQuery.Length > 0;
        }
    }
}