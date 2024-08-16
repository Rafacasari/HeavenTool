namespace HeavenTool.Forms
{
    partial class BCSVSearchBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BCSVSearchBox));
            this.searchValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.findButton = new System.Windows.Forms.Button();
            this.containsButton = new System.Windows.Forms.RadioButton();
            this.exactlyButton = new System.Windows.Forms.RadioButton();
            this.matchesCount = new System.Windows.Forms.Label();
            this.reverseDirectionCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.caseSensitivivtyCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchValue
            // 
            this.searchValue.Location = new System.Drawing.Point(67, 10);
            this.searchValue.Name = "searchValue";
            this.searchValue.Size = new System.Drawing.Size(295, 20);
            this.searchValue.TabIndex = 0;
            this.searchValue.TextChanged += new System.EventHandler(this.searchValue_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Value";
            // 
            // findButton
            // 
            this.findButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.findButton.Enabled = false;
            this.findButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.findButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.findButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            this.findButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findButton.ForeColor = System.Drawing.Color.White;
            this.findButton.Location = new System.Drawing.Point(218, 113);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(144, 24);
            this.findButton.TabIndex = 2;
            this.findButton.Text = "Next";
            this.findButton.UseVisualStyleBackColor = false;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // containsButton
            // 
            this.containsButton.AutoSize = true;
            this.containsButton.Checked = true;
            this.containsButton.ForeColor = System.Drawing.Color.White;
            this.containsButton.Location = new System.Drawing.Point(11, 19);
            this.containsButton.Name = "containsButton";
            this.containsButton.Size = new System.Drawing.Size(66, 17);
            this.containsButton.TabIndex = 3;
            this.containsButton.TabStop = true;
            this.containsButton.Text = "Contains";
            this.containsButton.UseVisualStyleBackColor = true;
            // 
            // exactlyButton
            // 
            this.exactlyButton.AutoSize = true;
            this.exactlyButton.ForeColor = System.Drawing.Color.White;
            this.exactlyButton.Location = new System.Drawing.Point(83, 19);
            this.exactlyButton.Name = "exactlyButton";
            this.exactlyButton.Size = new System.Drawing.Size(59, 17);
            this.exactlyButton.TabIndex = 4;
            this.exactlyButton.Text = "Exactly";
            this.exactlyButton.UseVisualStyleBackColor = true;
            // 
            // matchesCount
            // 
            this.matchesCount.AutoSize = true;
            this.matchesCount.ForeColor = System.Drawing.Color.White;
            this.matchesCount.Location = new System.Drawing.Point(13, 119);
            this.matchesCount.Name = "matchesCount";
            this.matchesCount.Size = new System.Drawing.Size(94, 13);
            this.matchesCount.TabIndex = 5;
            this.matchesCount.Text = "No matches found";
            // 
            // reverseDirectionCheckbox
            // 
            this.reverseDirectionCheckbox.AutoSize = true;
            this.reverseDirectionCheckbox.ForeColor = System.Drawing.Color.White;
            this.reverseDirectionCheckbox.Location = new System.Drawing.Point(11, 42);
            this.reverseDirectionCheckbox.Name = "reverseDirectionCheckbox";
            this.reverseDirectionCheckbox.Size = new System.Drawing.Size(111, 17);
            this.reverseDirectionCheckbox.TabIndex = 6;
            this.reverseDirectionCheckbox.Text = "Reverse Direction";
            this.reverseDirectionCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.caseSensitivivtyCheckbox);
            this.groupBox1.Controls.Add(this.exactlyButton);
            this.groupBox1.Controls.Add(this.reverseDirectionCheckbox);
            this.groupBox1.Controls.Add(this.containsButton);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 71);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // caseSensitivivtyCheckbox
            // 
            this.caseSensitivivtyCheckbox.AutoSize = true;
            this.caseSensitivivtyCheckbox.Location = new System.Drawing.Point(128, 42);
            this.caseSensitivivtyCheckbox.Name = "caseSensitivivtyCheckbox";
            this.caseSensitivivtyCheckbox.Size = new System.Drawing.Size(96, 17);
            this.caseSensitivivtyCheckbox.TabIndex = 7;
            this.caseSensitivivtyCheckbox.Text = "Case Sensitive";
            this.caseSensitivivtyCheckbox.UseVisualStyleBackColor = true;
            // 
            // BCSVSearchBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(374, 149);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.matchesCount);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BCSVSearchBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Search";
            this.Activated += new System.EventHandler(this.BCSVSearchBox_Activated);
            this.Deactivate += new System.EventHandler(this.BCSVSearchBox_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BCSVSearchBox_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}