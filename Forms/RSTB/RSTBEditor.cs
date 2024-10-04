using HeavenTool.Properties;
using HeavenTool.Utility.FileTypes.RSTB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HeavenTool.Forms.RSTB
{
    public partial class RSTBEditor : Form
    {
        public RSTBEditor()
        {
            InitializeComponent();

            Text = $"Heaven Tool | {Program.VERSION} | RSTB Editor";

            //var fileNameColumn = mainDataGridView.Columns.Add("fileName", "File Name");
            mainDataGridView.Columns["fileName"].ValueType = typeof(string);
            mainDataGridView.Columns["fileSize"].ValueType = typeof(uint);
            mainDataGridView.Columns["DLC"].ValueType = typeof(uint);
            //var sizeColumn = mainDataGridView.Columns.Add("Size", "Size");
            //var dlcColumn = mainDataGridView.Columns.Add("DLC", "DLC");
        }

        ResourceTableReader LoadedFile;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainDataGridView.Rows.Clear();

            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a .srsizetable file",
                Filter = "RSTB (*.srsizetable)|*.srsizetable",
                DefaultExt = "*.srsizetable",
                FilterIndex = 1,
                RestoreDirectory = true,
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                LoadedFile = new ResourceTableReader(filePath);

                if (!LoadedFile.IsLoaded) return;

                DrawingControl.SuspendDrawing(mainDataGridView);

                foreach (var entry in LoadedFile.UniqueEntries)
                {
                    var values = new List<object>() { RomFsNameManager.GetValue(entry.CRCHash), entry.FileSize };

                    if (LoadedFile.HEADER == "RSTC")
                        values.Add(entry.DLC);

                    mainDataGridView.Rows.Add([.. values]);
                }

                foreach (var entry in LoadedFile.RepeatedHashesEntries)
                {
                    var values = new List<object>() { entry.FileName, entry.FileSize };

                    if (LoadedFile.HEADER == "RSTC")
                        values.Add(entry.DLC);

                    mainDataGridView.Rows.Add([.. values]);
                }

                DrawingControl.ResumeDrawing(mainDataGridView);
            }
        }

        private void updateHashesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFolderDialog = new FolderBrowserDialog()
            {
                ShowNewFolderButton = false,
                Description = "Select a RomFs directory",
                SelectedPath = Settings.Default.LastSelectedRomFsDirectory ?? ""

            };

            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = openFolderDialog.SelectedPath;
                Settings.Default.LastSelectedRomFsDirectory = selectedPath;
                Settings.Default.Save();

                var files = Directory.GetFiles(selectedPath, "*", SearchOption.AllDirectories);

                files = files.Select(x =>
                {
                    var text = x.Replace('\\', '/');

                    text = text.Substring(selectedPath.Length + 1);

                    if (text.EndsWith(".zs"))
                        text = text[..^3]; // Same as text.Substring(0, text.Length - 3);

                    return text;
                }).ToArray();

                // Save
                RomFsNameManager.Update(files);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedFile == null) return;

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "RSTB/RSTC (*.srsizetable)|*.srsizetable",
                FilterIndex = 1,
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                LoadedFile.SaveTo(saveFileDialog.FileName);
        }
    }
}
