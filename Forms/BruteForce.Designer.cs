namespace HeavenTool.Forms
{
    partial class BruteForce
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.directoryPath = new System.Windows.Forms.TextBox();
            this.selectDirectoryButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.openFolderDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startBruteForceButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.exportMissingHashesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // directoryPath
            // 
            this.directoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryPath.Location = new System.Drawing.Point(12, 11);
            this.directoryPath.Name = "directoryPath";
            this.directoryPath.Size = new System.Drawing.Size(269, 20);
            this.directoryPath.TabIndex = 0;
            // 
            // selectDirectoryButton
            // 
            this.selectDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectDirectoryButton.Location = new System.Drawing.Point(287, 11);
            this.selectDirectoryButton.Name = "selectDirectoryButton";
            this.selectDirectoryButton.Size = new System.Drawing.Size(107, 20);
            this.selectDirectoryButton.TabIndex = 1;
            this.selectDirectoryButton.Text = "Select Directory";
            this.selectDirectoryButton.UseVisualStyleBackColor = true;
            this.selectDirectoryButton.Click += new System.EventHandler(this.selectDirectoryButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(400, 11);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(81, 20);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // openFolderDialog
            // 
            this.openFolderDialog.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Unknown Hashes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Found Hashes";
            // 
            // startBruteForceButton
            // 
            this.startBruteForceButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startBruteForceButton.Location = new System.Drawing.Point(174, 347);
            this.startBruteForceButton.Name = "startBruteForceButton";
            this.startBruteForceButton.Size = new System.Drawing.Size(307, 29);
            this.startBruteForceButton.TabIndex = 11;
            this.startBruteForceButton.Text = "Start Brute Force";
            this.startBruteForceButton.UseVisualStyleBackColor = true;
            this.startBruteForceButton.Click += new System.EventHandler(this.startBruteForceButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 50);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(156, 290);
            this.listBox1.TabIndex = 12;
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(174, 50);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(307, 290);
            this.listBox2.TabIndex = 13;
            // 
            // exportMissingHashesButton
            // 
            this.exportMissingHashesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportMissingHashesButton.Location = new System.Drawing.Point(12, 347);
            this.exportMissingHashesButton.Name = "exportMissingHashesButton";
            this.exportMissingHashesButton.Size = new System.Drawing.Size(156, 29);
            this.exportMissingHashesButton.TabIndex = 14;
            this.exportMissingHashesButton.Text = "Export Missing Hashes";
            this.exportMissingHashesButton.UseVisualStyleBackColor = true;
            this.exportMissingHashesButton.Click += new System.EventHandler(this.exportMissingHashesButton_Click);
            // 
            // BruteForce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 388);
            this.Controls.Add(this.exportMissingHashesButton);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.startBruteForceButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.selectDirectoryButton);
            this.Controls.Add(this.directoryPath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BruteForce";
            this.ShowIcon = false;
            this.Text = "Brute Force Unknown Hashes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox directoryPath;
        private System.Windows.Forms.Button selectDirectoryButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.OpenFileDialog openFolderDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startBruteForceButton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button exportMissingHashesButton;
    }
}