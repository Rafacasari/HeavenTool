namespace HeavenTool.Forms.BCSV
{
    partial class BCSVEnumEditor
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
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            richTextBox2 = new System.Windows.Forms.RichTextBox();
            statusLabel = new System.Windows.Forms.Label();
            newStatusLabel = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBox1.Location = new System.Drawing.Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(776, 120);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // richTextBox2
            // 
            richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBox2.Location = new System.Drawing.Point(12, 172);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new System.Drawing.Size(776, 136);
            richTextBox2.TabIndex = 1;
            richTextBox2.Text = "";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.ForeColor = System.Drawing.Color.White;
            statusLabel.Location = new System.Drawing.Point(12, 135);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(66, 15);
            statusLabel.TabIndex = 2;
            statusLabel.Text = "statusLabel";
            // 
            // newStatusLabel
            // 
            newStatusLabel.AutoSize = true;
            newStatusLabel.ForeColor = System.Drawing.Color.White;
            newStatusLabel.Location = new System.Drawing.Point(12, 311);
            newStatusLabel.Name = "newStatusLabel";
            newStatusLabel.Size = new System.Drawing.Size(89, 15);
            newStatusLabel.TabIndex = 3;
            newStatusLabel.Text = "newStatusLabel";
            // 
            // BCSVEnumEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(newStatusLabel);
            Controls.Add(statusLabel);
            Controls.Add(richTextBox2);
            Controls.Add(richTextBox1);
            Name = "BCSVEnumEditor";
            Text = "BCSVEnumEditor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label newStatusLabel;
    }
}