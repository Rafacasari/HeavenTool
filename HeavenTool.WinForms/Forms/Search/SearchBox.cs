using HeavenTool.Forms.Search;
using System;
using System.Windows.Forms;

namespace HeavenTool.Forms
{
    public partial class SearchBox : Form
    {
        // TODO: Update this form to be compatible with any file type (form)

        private Form CallerForm;
        private ISearchable SearchableForm;

        public SearchBox(Form caller)
        {
            InitializeComponent();

            CallerForm = caller;
            Owner = caller;

            if (caller is ISearchable searchable)
                SearchableForm = searchable;
            else
                MessageBox.Show("fodase");

            CenterToParent();
        }

        public new void Show()
        {
            // Return original form ownership
            Owner = CallerForm;
            base.Show();
        }

        private void BCSVSearchBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            SearchableForm.SearchClosing();

            // Remove original form ownership, so it can actually close without any issue
            Owner = null;

            // Cancel closing, we want to just hide it
            e.Cancel = true;

            Hide();
        }

        private void BCSVSearchBox_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void BCSVSearchBox_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.5d;
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            SearchableForm.Search(searchValue.Text, exactlyButton.Checked ? SearchType.Exactly : SearchType.Contains, reverseDirectionCheckbox.Checked, caseSensitivivtyCheckbox.Checked);
        }

        public void UpdateMatchesFound(int quantity, int currentIndex)
        {
            if (quantity > 1)
                matchesCount.Text = $"{quantity} matches found ({currentIndex + 1}/{quantity})";
            else if (quantity == 1)
                matchesCount.Text = "1 match found";
            else
                matchesCount.Text = "No matches found";
        }

        private void searchValue_TextChanged(object sender, EventArgs e)
        {
            findButton.Enabled = !string.IsNullOrEmpty(searchValue.Text);
        }
    }

    public enum SearchType
    {
        Contains, Exactly
    }
}
