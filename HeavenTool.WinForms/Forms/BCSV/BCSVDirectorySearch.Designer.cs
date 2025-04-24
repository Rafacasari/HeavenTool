namespace HeavenTool.Forms
{
    partial class BCSVDirectorySearch
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
            directoryPath = new System.Windows.Forms.TextBox();
            selectDirectoryButton = new System.Windows.Forms.Button();
            searchButton = new System.Windows.Forms.Button();
            openFolderDialog = new System.Windows.Forms.OpenFileDialog();
            containButton = new System.Windows.Forms.RadioButton();
            exactlyButton = new System.Windows.Forms.RadioButton();
            searchField = new System.Windows.Forms.TextBox();
            textLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            foundHits = new System.Windows.Forms.TreeView();
            SuspendLayout();
            // 
            // directoryPath
            // 
            directoryPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            directoryPath.BackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            directoryPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            directoryPath.Font = new System.Drawing.Font("Segoe UI", 14F);
            directoryPath.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            directoryPath.Location = new System.Drawing.Point(14, 12);
            directoryPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            directoryPath.Name = "directoryPath";
            directoryPath.Size = new System.Drawing.Size(244, 25);
            directoryPath.TabIndex = 0;
            // 
            // selectDirectoryButton
            // 
            selectDirectoryButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            selectDirectoryButton.BackColor = System.Drawing.Color.FromArgb(70, 70, 70);
            selectDirectoryButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 80, 80);
            selectDirectoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            selectDirectoryButton.ForeColor = System.Drawing.Color.FromArgb(230, 230, 230);
            selectDirectoryButton.Location = new System.Drawing.Point(266, 12);
            selectDirectoryButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            selectDirectoryButton.Name = "selectDirectoryButton";
            selectDirectoryButton.Size = new System.Drawing.Size(127, 25);
            selectDirectoryButton.TabIndex = 1;
            selectDirectoryButton.Text = "Select Directory";
            selectDirectoryButton.UseVisualStyleBackColor = false;
            selectDirectoryButton.Click += SelectDirectoryButton_Click;
            // 
            // searchButton
            // 
            searchButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            searchButton.BackColor = System.Drawing.Color.FromArgb(70, 70, 70);
            searchButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 80, 80);
            searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            searchButton.ForeColor = System.Drawing.Color.FromArgb(230, 230, 230);
            searchButton.Location = new System.Drawing.Point(235, 71);
            searchButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            searchButton.Name = "searchButton";
            searchButton.Size = new System.Drawing.Size(157, 26);
            searchButton.TabIndex = 2;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = false;
            searchButton.Click += SearchButton_Click;
            // 
            // openFolderDialog
            // 
            openFolderDialog.FileName = "openFileDialog1";
            // 
            // containButton
            // 
            containButton.AutoSize = true;
            containButton.Checked = true;
            containButton.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            containButton.Location = new System.Drawing.Point(14, 75);
            containButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            containButton.Name = "containButton";
            containButton.Size = new System.Drawing.Size(67, 19);
            containButton.TabIndex = 3;
            containButton.TabStop = true;
            containButton.Text = "Contain";
            containButton.UseVisualStyleBackColor = true;
            // 
            // exactlyButton
            // 
            exactlyButton.AutoSize = true;
            exactlyButton.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            exactlyButton.Location = new System.Drawing.Point(92, 75);
            exactlyButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            exactlyButton.Name = "exactlyButton";
            exactlyButton.Size = new System.Drawing.Size(61, 19);
            exactlyButton.TabIndex = 4;
            exactlyButton.Text = "Exactly";
            exactlyButton.UseVisualStyleBackColor = true;
            // 
            // searchField
            // 
            searchField.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            searchField.BackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            searchField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            searchField.Font = new System.Drawing.Font("Segoe UI", 10F);
            searchField.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            searchField.Location = new System.Drawing.Point(53, 44);
            searchField.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            searchField.Name = "searchField";
            searchField.Size = new System.Drawing.Size(339, 18);
            searchField.TabIndex = 5;
            // 
            // textLabel
            // 
            textLabel.AutoSize = true;
            textLabel.Location = new System.Drawing.Point(17, 46);
            textLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            textLabel.Name = "textLabel";
            textLabel.Size = new System.Drawing.Size(28, 15);
            textLabel.TabIndex = 6;
            textLabel.Text = "Text";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            label1.Location = new System.Drawing.Point(14, 102);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(31, 15);
            label1.TabIndex = 8;
            label1.Text = "Hits:";
            // 
            // foundHits
            // 
            foundHits.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            foundHits.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            foundHits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            foundHits.Location = new System.Drawing.Point(14, 120);
            foundHits.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            foundHits.Name = "foundHits";
            foundHits.Size = new System.Drawing.Size(378, 114);
            foundHits.TabIndex = 10;
            // 
            // BCSVDirectorySearch
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(407, 248);
            Controls.Add(foundHits);
            Controls.Add(label1);
            Controls.Add(textLabel);
            Controls.Add(searchField);
            Controls.Add(exactlyButton);
            Controls.Add(containButton);
            Controls.Add(searchButton);
            Controls.Add(selectDirectoryButton);
            Controls.Add(directoryPath);
            ForeColor = System.Drawing.Color.FromArgb(230, 230, 230);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BCSVDirectorySearch";
            ShowIcon = false;
            Text = "Search in Directory";
            ResumeLayout(false);
            PerformLayout();
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