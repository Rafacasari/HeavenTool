namespace HeavenTool.Forms
{
    partial class BCSVCompareWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BCSVCompareWindow));
            this.compareDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.compareDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // compareDataGrid
            // 
            this.compareDataGrid.AllowUserToAddRows = false;
            this.compareDataGrid.AllowUserToDeleteRows = false;
            this.compareDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.compareDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.compareDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareDataGrid.EnableHeadersVisualStyles = false;
            this.compareDataGrid.Location = new System.Drawing.Point(0, 0);
            this.compareDataGrid.MultiSelect = false;
            this.compareDataGrid.Name = "compareDataGrid";
            this.compareDataGrid.ReadOnly = true;
            this.compareDataGrid.RowHeadersVisible = false;
            this.compareDataGrid.ShowEditingIcon = false;
            this.compareDataGrid.Size = new System.Drawing.Size(582, 301);
            this.compareDataGrid.TabIndex = 0;
            // 
            // BCSVCompareWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 301);
            this.Controls.Add(this.compareDataGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BCSVCompareWindow";
            this.Text = "BCSVCompareWindow";
            ((System.ComponentModel.ISupportInitialize)(this.compareDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView compareDataGrid;
    }
}