using System;
using System.Windows.Forms;

namespace HeavenTool
{
    public partial class HeavenMain : Form
    {
        public HeavenMain()
        {
            InitializeComponent();
        }

        // Forms
        public static MainFrm bcsvEditor = new MainFrm();

        private void bcsvEditorButton_Click(object sender, EventArgs e)
        {
            bcsvEditor.Show();
            bcsvEditor.BringToFront();
        }
    }
}
