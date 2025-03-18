using HeavenTool.Forms.Pack;
using HeavenTool.Forms.RSTB;
using HeavenTool.Forms.SARC;
using HeavenTool.Utility.IO.Compression;
using System;
using System.IO;
using System.Text;
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

        private void yaz0DecompressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Multiselect = false
            };

            var selectedFile = openFileDialog.ShowDialog();

            if (selectedFile == DialogResult.OK
                && !string.IsNullOrEmpty(openFileDialog.FileName)
                && File.Exists(openFileDialog.FileName))
            {
                using (var fileStream = File.OpenRead(openFileDialog.FileName))
                {
                    MemoryStream memoryStream = new();


                    if (!Yaz0CompressionAlgorithm.TryToDecompress(fileStream, out byte[] decompressedBytes))
                    {
                        MessageBox.Show("Failed to decompress Yaz0", "Failed to open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    var saveFileDialog = new SaveFileDialog() { 
                        FileName = openFileDialog.FileName,
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var savePath = saveFileDialog.FileName;
                        File.WriteAllBytes(savePath, decompressedBytes);
                    }
                }
            }
        }
    }
}
