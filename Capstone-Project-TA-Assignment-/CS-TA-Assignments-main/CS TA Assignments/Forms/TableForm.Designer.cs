
namespace cwu.cs.TaAssignments
{
    partial class TableForm
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
            this.ltvTable = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // ltvTable
            // 
            this.ltvTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ltvTable.FullRowSelect = true;
            this.ltvTable.GridLines = true;
            this.ltvTable.HideSelection = false;
            this.ltvTable.Location = new System.Drawing.Point(12, 12);
            this.ltvTable.Name = "ltvTable";
            this.ltvTable.Size = new System.Drawing.Size(1015, 677);
            this.ltvTable.TabIndex = 0;
            this.ltvTable.UseCompatibleStateImageBehavior = false;
            this.ltvTable.View = System.Windows.Forms.View.Details;
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 711);
            this.Controls.Add(this.ltvTable);
            this.Name = "TableForm";
            this.Text = "TableForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ltvTable;
    }
}