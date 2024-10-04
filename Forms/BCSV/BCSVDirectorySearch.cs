using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HeavenTool.Forms
{
    public partial class BCSVDirectorySearch : Form
    {
        public BCSVDirectorySearch()
        {
            InitializeComponent();
        }

        private void selectDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
                directoryPath.Text = folderBrowserDialog.SelectedPath;

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            searchButton.Enabled = false;
            if (Directory.Exists(directoryPath.Text))
            {
                
                try
                {
                    foreach (var file in Directory.GetFiles(directoryPath.Text))
                    {
                        if (Path.GetExtension(file) != ".bcsv")
                            continue;

                        ReadBCSVFileAndSearch(file);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("Error while searching:\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }

            searchButton.Enabled = true;
        }


        private void ReadBCSVFileAndSearch(string path)
        {
            if (!File.Exists(path))
                return;

            var bcsvFile = new Utility.FileTypes.BCSV.BinaryCSV(path);

            foreach(var header in bcsvFile.Fields)
            {
                var name = header.GetTranslatedNameOrHash();
                if ((containButton.Checked && name.ToLower().Contains(searchField.Text.ToLower())) || name == searchField.Text)
                {
                    var key = Path.GetFileNameWithoutExtension(path);
                    if (!foundHits.Nodes.ContainsKey(key))
                        foundHits.Nodes.Add(key, key);

                    var node = foundHits.Nodes[key];
                    node.Nodes.Add($"Header: {name}");
                }
            }
            
            foreach(var (entry, index) in bcsvFile.Entries.Select((x, y) => (x, y)))
            {
                foreach (var item in entry)
                {
                    var value = item.Value.ToString();
                    if ((containButton.Checked && value.ToLower().Contains(searchField.Text.ToLower())) || value == searchField.Text)
                    {
                        //hitsFound.Items.Add($"{Path.GetFileNameWithoutExtension(path)}: {value} (value)");
                        var key = Path.GetFileNameWithoutExtension(path);
                        if (!foundHits.Nodes.ContainsKey(key))
                            foundHits.Nodes.Add(key, key);

                        var node = foundHits.Nodes[key];
                        var entryField = bcsvFile.GetFieldByHashedName(item.Key);
                        if (entryField != null)
                            node.Nodes.Add($"Entry: {index} | Header: {entryField.GetTranslatedNameOrHash()} | {value} (value)");
                        else 
                            node.Nodes.Add($"Entry: {index} | {value} (value)");
                    }
                }
            }

            bcsvFile = null;
            GC.Collect();
        }
    }
}
