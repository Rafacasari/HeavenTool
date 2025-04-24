namespace HeavenTool.Forms
{
    partial class SearchBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchBox));
            searchValue = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            findButton = new System.Windows.Forms.Button();
            containsButton = new System.Windows.Forms.RadioButton();
            exactlyButton = new System.Windows.Forms.RadioButton();
            matchesCount = new System.Windows.Forms.Label();
            reverseDirectionCheckbox = new System.Windows.Forms.CheckBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            caseSensitivivtyCheckbox = new System.Windows.Forms.CheckBox();
            panel1 = new System.Windows.Forms.Panel();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // searchValue
            // 
            searchValue.BackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            searchValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            searchValue.ForeColor = System.Drawing.SystemColors.ScrollBar;
            searchValue.Location = new System.Drawing.Point(4, 4);
            searchValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            searchValue.Name = "searchValue";
            searchValue.Size = new System.Drawing.Size(343, 16);
            searchValue.TabIndex = 0;
            searchValue.TextChanged += searchValue_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.Color.FromArgb(210, 210, 210);
            label1.Location = new System.Drawing.Point(15, 16);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 15);
            label1.TabIndex = 1;
            label1.Text = "Value";
            // 
            // findButton
            // 
            findButton.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            findButton.Enabled = false;
            findButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(30, 30, 30);
            findButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            findButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(50, 50, 60);
            findButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            findButton.ForeColor = System.Drawing.Color.Gainsboro;
            findButton.Location = new System.Drawing.Point(254, 130);
            findButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            findButton.Name = "findButton";
            findButton.Size = new System.Drawing.Size(168, 28);
            findButton.TabIndex = 2;
            findButton.Text = "Next";
            findButton.UseVisualStyleBackColor = false;
            findButton.Click += findButton_Click;
            // 
            // containsButton
            // 
            containsButton.AutoSize = true;
            containsButton.Checked = true;
            containsButton.ForeColor = System.Drawing.Color.Gainsboro;
            containsButton.Location = new System.Drawing.Point(13, 22);
            containsButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            containsButton.Name = "containsButton";
            containsButton.Size = new System.Drawing.Size(72, 19);
            containsButton.TabIndex = 3;
            containsButton.TabStop = true;
            containsButton.Text = "Contains";
            containsButton.UseVisualStyleBackColor = true;
            // 
            // exactlyButton
            // 
            exactlyButton.AutoSize = true;
            exactlyButton.ForeColor = System.Drawing.Color.Gainsboro;
            exactlyButton.Location = new System.Drawing.Point(97, 22);
            exactlyButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            exactlyButton.Name = "exactlyButton";
            exactlyButton.Size = new System.Drawing.Size(61, 19);
            exactlyButton.TabIndex = 4;
            exactlyButton.Text = "Exactly";
            exactlyButton.UseVisualStyleBackColor = true;
            // 
            // matchesCount
            // 
            matchesCount.AutoSize = true;
            matchesCount.ForeColor = System.Drawing.Color.FromArgb(210, 210, 210);
            matchesCount.Location = new System.Drawing.Point(15, 137);
            matchesCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            matchesCount.Name = "matchesCount";
            matchesCount.Size = new System.Drawing.Size(106, 15);
            matchesCount.TabIndex = 5;
            matchesCount.Text = "No matches found";
            // 
            // reverseDirectionCheckbox
            // 
            reverseDirectionCheckbox.AutoSize = true;
            reverseDirectionCheckbox.ForeColor = System.Drawing.Color.Gainsboro;
            reverseDirectionCheckbox.Location = new System.Drawing.Point(13, 48);
            reverseDirectionCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            reverseDirectionCheckbox.Name = "reverseDirectionCheckbox";
            reverseDirectionCheckbox.Size = new System.Drawing.Size(117, 19);
            reverseDirectionCheckbox.TabIndex = 6;
            reverseDirectionCheckbox.Text = "Reverse Direction";
            reverseDirectionCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(caseSensitivivtyCheckbox);
            groupBox1.Controls.Add(exactlyButton);
            groupBox1.Controls.Add(reverseDirectionCheckbox);
            groupBox1.Controls.Add(containsButton);
            groupBox1.ForeColor = System.Drawing.Color.FromArgb(210, 210, 210);
            groupBox1.Location = new System.Drawing.Point(14, 42);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(408, 82);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Options";
            // 
            // caseSensitivivtyCheckbox
            // 
            caseSensitivivtyCheckbox.AutoSize = true;
            caseSensitivivtyCheckbox.ForeColor = System.Drawing.Color.Gainsboro;
            caseSensitivivtyCheckbox.Location = new System.Drawing.Point(149, 48);
            caseSensitivivtyCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            caseSensitivivtyCheckbox.Name = "caseSensitivivtyCheckbox";
            caseSensitivivtyCheckbox.Size = new System.Drawing.Size(100, 19);
            caseSensitivivtyCheckbox.TabIndex = 7;
            caseSensitivivtyCheckbox.Text = "Case Sensitive";
            caseSensitivivtyCheckbox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            panel1.Controls.Add(searchValue);
            panel1.Location = new System.Drawing.Point(71, 12);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(351, 24);
            panel1.TabIndex = 8;
            // 
            // SearchBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            ClientSize = new System.Drawing.Size(436, 172);
            Controls.Add(panel1);
            Controls.Add(groupBox1);
            Controls.Add(matchesCount);
            Controls.Add(findButton);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SearchBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Search";
            Activated += BCSVSearchBox_Activated;
            Deactivate += BCSVSearchBox_Deactivate;
            FormClosing += BCSVSearchBox_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox searchValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.RadioButton containsButton;
        private System.Windows.Forms.RadioButton exactlyButton;
        private System.Windows.Forms.Label matchesCount;
        private System.Windows.Forms.CheckBox reverseDirectionCheckbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox caseSensitivivtyCheckbox;
        private System.Windows.Forms.Panel panel1;
    }
}