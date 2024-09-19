namespace HeavenTool.Forms
{
    partial class DirectorySearch
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
            this.containButton = new System.Windows.Forms.RadioButton();
            this.exactlyButton = new System.Windows.Forms.RadioButton();
            this.searchField = new System.Windows.Forms.TextBox();
            this.textLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.foundHits = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // directoryPath
            // 
            this.directoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryPath.Location = new System.Drawing.Point(12, 11);
            this.directoryPath.Name = "directoryPath";
            this.directoryPath.Size = new System.Drawing.Size(210, 20);
            this.directoryPath.TabIndex = 0;
            // 
            // selectDirectoryButton
            // 
            this.selectDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectDirectoryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.selectDirectoryButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.selectDirectoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectDirectoryButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.selectDirectoryButton.Location = new System.Drawing.Point(228, 9);
            this.selectDirectoryButton.Name = "selectDirectoryButton";
            this.selectDirectoryButton.Size = new System.Drawing.Size(109, 23);
            this.selectDirectoryButton.TabIndex = 1;
            this.selectDirectoryButton.Text = "Select Directory";
            this.selectDirectoryButton.UseVisualStyleBackColor = false;
            this.selectDirectoryButton.Click += new System.EventHandler(this.selectDirectoryButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.searchButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.searchButton.Location = new System.Drawing.Point(228, 62);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(109, 23);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // openFolderDialog
            // 
            this.openFolderDialog.FileName = "openFileDialog1";
            // 
            // containButton
            // 
            this.containButton.AutoSize = true;
            this.containButton.Checked = true;
            this.containButton.Location = new System.Drawing.Point(12, 65);
            this.containButton.Name = "containButton";
            this.containButton.Size = new System.Drawing.Size(61, 17);
            this.containButton.TabIndex = 3;
            this.containButton.TabStop = true;
            this.containButton.Text = "Contain";
            this.containButton.UseVisualStyleBackColor = true;
            // 
            // exactlyButton
            // 
            this.exactlyButton.AutoSize = true;
            this.exactlyButton.Location = new System.Drawing.Point(79, 65);
            this.exactlyButton.Name = "exactlyButton";
            this.exactlyButton.Size = new System.Drawing.Size(59, 17);
            this.exactlyButton.TabIndex = 4;
            this.exactlyButton.Text = "Exactly";
            this.exactlyButton.UseVisualStyleBackColor = true;
            // 
            // searchField
            // 
            this.searchField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchField.Location = new System.Drawing.Point(46, 37);
            this.searchField.Name = "searchField";
            this.searchField.Size = new System.Drawing.Size(291, 20);
            this.searchField.TabIndex = 5;
            // 
            // textLabel
            // 
            this.textLabel.AutoSize = true;
            this.textLabel.Location = new System.Drawing.Point(12, 40);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(28, 13);
            this.textLabel.TabIndex = 6;
            this.textLabel.Text = "Text";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Hits:";
            // 
            // foundHits
            // 
            this.foundHits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.foundHits.Location = new System.Drawing.Point(12, 104);
            this.foundHits.Name = "foundHits";
            this.foundHits.Size = new System.Drawing.Size(325, 99);
            this.foundHits.TabIndex = 10;
            // 
            // DirectorySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(349, 215);
            this.Controls.Add(this.foundHits);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textLabel);
            this.Controls.Add(this.searchField);
            this.Controls.Add(this.exactlyButton);
            this.Controls.Add(this.containButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.selectDirectoryButton);
            this.Controls.Add(this.directoryPath);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DirectorySearch";
            this.ShowIcon = false;
            this.Text = "Search in Directory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox directoryPath;
        private System.Windows.Forms.Button selectDirectoryButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.OpenFileDialog openFolderDialog;
        private System.Windows.Forms.RadioButton containButton;
        private System.Windows.Forms.RadioButton exactlyButton;
        private System.Windows.Forms.TextBox searchField;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView foundHits;
    }
}