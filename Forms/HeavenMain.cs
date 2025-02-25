using HeavenTool.Forms.Pack;
using HeavenTool.Forms.RSTB;
using HeavenTool.Forms.SARC;
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
        public static BCSVForm bcsvEditor = new();
        public static RSTBEditor rstbEditor = new();
        public static SarcEditor sarcEditor = new();
        public static ItemIDHelper itemIDHelper = new();

        private void bcsvEditorButton_Click(object sender, EventArgs e)
        {
            if (bcsvEditor.IsDisposed) bcsvEditor = new();

            bcsvEditor.Show();
            bcsvEditor.BringToFront();
        }

        private void rstbEditorButton_Click(object sender, EventArgs e)
        {
            if (rstbEditor.IsDisposed) rstbEditor = new();

            rstbEditor.Show();
            rstbEditor.BringToFront();
        }

        private void sarcEditorButton_Click(object sender, EventArgs e)
        {
            if (sarcEditor.IsDisposed) sarcEditor = new();

            sarcEditor.Show();
            sarcEditor.BringToFront();
        }

        private void itemParamHelperButton_Click(object sender, EventArgs e)
        {
            if (itemIDHelper.IsDisposed) itemIDHelper = new();

            itemIDHelper.Show();
            itemIDHelper.BringToFront();
        }
    }
}
