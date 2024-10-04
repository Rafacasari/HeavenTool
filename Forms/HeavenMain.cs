using HeavenTool.Forms.RSTB;
using System;
using System.Windows.Forms;

namespace HeavenTool
{
    public partial class HeavenMain : Form
    {
        public HeavenMain()
        {
            InitializeComponent();

            Text = $"Heaven Tool | {Program.VERSION}";
        }

        // Forms
        public static BCSVForm bcsvEditor = new BCSVForm();
        public static RSTBEditor rstbEditor = new RSTBEditor();

        private void bcsvEditorButton_Click(object sender, EventArgs e)
        {
            if (bcsvEditor.IsDisposed) bcsvEditor = new BCSVForm();

            bcsvEditor.Show();
            bcsvEditor.BringToFront();
        }

        private void rstbEditorButton_Click(object sender, EventArgs e)
        {
            if (rstbEditor.IsDisposed) rstbEditor = new RSTBEditor();

            rstbEditor.Show();
            rstbEditor.BringToFront();
        }
    }
}
