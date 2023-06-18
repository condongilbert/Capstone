
namespace cwu.cs.TaAssignments
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtApplications = new System.Windows.Forms.TextBox();
            this.lblApplications = new System.Windows.Forms.Label();
            this.btnApplications = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.txtSchedule = new System.Windows.Forms.TextBox();
            this.btnGrades = new System.Windows.Forms.Button();
            this.lblGrades = new System.Windows.Forms.Label();
            this.txtGrades = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.dlgOpenCsv = new System.Windows.Forms.OpenFileDialog();
            this.gbxInput = new System.Windows.Forms.GroupBox();
            this.gbxOutput = new System.Windows.Forms.GroupBox();
            this.btnResults = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.lblResults = new System.Windows.Forms.Label();
            this.dlgSaveCsv = new System.Windows.Forms.SaveFileDialog();
            this.Results = new System.Windows.Forms.Button();
            this.gbxInput.SuspendLayout();
            this.gbxOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.SystemColors.GrayText;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.SystemColors.Window;
            this.txtLog.Location = new System.Drawing.Point(11, 210);
            this.txtLog.Margin = new System.Windows.Forms.Padding(2);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(748, 430);
            this.txtLog.TabIndex = 1;
            this.txtLog.WordWrap = false;
            // 
            // txtApplications
            // 
            this.txtApplications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplications.BackColor = System.Drawing.SystemColors.GrayText;
            this.txtApplications.ForeColor = System.Drawing.SystemColors.Window;
            this.txtApplications.Location = new System.Drawing.Point(73, 20);
            this.txtApplications.Margin = new System.Windows.Forms.Padding(2);
            this.txtApplications.Name = "txtApplications";
            this.txtApplications.Size = new System.Drawing.Size(586, 20);
            this.txtApplications.TabIndex = 2;
            // 
            // lblApplications
            // 
            this.lblApplications.AutoSize = true;
            this.lblApplications.Location = new System.Drawing.Point(5, 23);
            this.lblApplications.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApplications.Name = "lblApplications";
            this.lblApplications.Size = new System.Drawing.Size(64, 13);
            this.lblApplications.TabIndex = 3;
            this.lblApplications.Text = "Applications";
            this.lblApplications.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnApplications
            // 
            this.btnApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplications.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnApplications.Location = new System.Drawing.Point(663, 18);
            this.btnApplications.Margin = new System.Windows.Forms.Padding(2);
            this.btnApplications.Name = "btnApplications";
            this.btnApplications.Size = new System.Drawing.Size(75, 23);
            this.btnApplications.TabIndex = 4;
            this.btnApplications.Text = "open";
            this.btnApplications.UseVisualStyleBackColor = false;
            this.btnApplications.Click += new System.EventHandler(this.btnApplications_Click);
            // 
            // btnSchedule
            // 
            this.btnSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSchedule.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnSchedule.Location = new System.Drawing.Point(663, 45);
            this.btnSchedule.Margin = new System.Windows.Forms.Padding(2);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(75, 23);
            this.btnSchedule.TabIndex = 7;
            this.btnSchedule.Text = "open";
            this.btnSchedule.UseVisualStyleBackColor = false;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.Location = new System.Drawing.Point(17, 50);
            this.lblSchedule.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(52, 13);
            this.lblSchedule.TabIndex = 6;
            this.lblSchedule.Text = "Schedule";
            this.lblSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSchedule
            // 
            this.txtSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSchedule.BackColor = System.Drawing.SystemColors.GrayText;
            this.txtSchedule.ForeColor = System.Drawing.SystemColors.Window;
            this.txtSchedule.Location = new System.Drawing.Point(73, 47);
            this.txtSchedule.Margin = new System.Windows.Forms.Padding(2);
            this.txtSchedule.Name = "txtSchedule";
            this.txtSchedule.Size = new System.Drawing.Size(586, 20);
            this.txtSchedule.TabIndex = 5;
            // 
            // btnGrades
            // 
            this.btnGrades.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrades.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnGrades.Location = new System.Drawing.Point(663, 72);
            this.btnGrades.Margin = new System.Windows.Forms.Padding(2);
            this.btnGrades.Name = "btnGrades";
            this.btnGrades.Size = new System.Drawing.Size(75, 23);
            this.btnGrades.TabIndex = 10;
            this.btnGrades.Text = "open";
            this.btnGrades.UseVisualStyleBackColor = false;
            this.btnGrades.Click += new System.EventHandler(this.btnGrades_Click);
            // 
            // lblGrades
            // 
            this.lblGrades.AutoSize = true;
            this.lblGrades.Location = new System.Drawing.Point(28, 77);
            this.lblGrades.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGrades.Name = "lblGrades";
            this.lblGrades.Size = new System.Drawing.Size(41, 13);
            this.lblGrades.TabIndex = 9;
            this.lblGrades.Text = "Grades";
            this.lblGrades.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGrades
            // 
            this.txtGrades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGrades.BackColor = System.Drawing.SystemColors.GrayText;
            this.txtGrades.ForeColor = System.Drawing.SystemColors.Window;
            this.txtGrades.Location = new System.Drawing.Point(73, 74);
            this.txtGrades.Margin = new System.Windows.Forms.Padding(2);
            this.txtGrades.Name = "txtGrades";
            this.txtGrades.Size = new System.Drawing.Size(586, 20);
            this.txtGrades.TabIndex = 8;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.AutoSize = true;
            this.btnRun.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnRun.Location = new System.Drawing.Point(674, 183);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 11;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // dlgOpenCsv
            // 
            this.dlgOpenCsv.Filter = "csv-File|*.csv";
            // 
            // gbxInput
            // 
            this.gbxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxInput.Controls.Add(this.btnApplications);
            this.gbxInput.Controls.Add(this.txtApplications);
            this.gbxInput.Controls.Add(this.lblGrades);
            this.gbxInput.Controls.Add(this.btnGrades);
            this.gbxInput.Controls.Add(this.lblSchedule);
            this.gbxInput.Controls.Add(this.txtGrades);
            this.gbxInput.Controls.Add(this.lblApplications);
            this.gbxInput.Controls.Add(this.btnSchedule);
            this.gbxInput.Controls.Add(this.txtSchedule);
            this.gbxInput.Location = new System.Drawing.Point(11, 12);
            this.gbxInput.Name = "gbxInput";
            this.gbxInput.Size = new System.Drawing.Size(743, 104);
            this.gbxInput.TabIndex = 12;
            this.gbxInput.TabStop = false;
            this.gbxInput.Text = "Input";
            // 
            // gbxOutput
            // 
            this.gbxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxOutput.Controls.Add(this.btnResults);
            this.gbxOutput.Controls.Add(this.txtResults);
            this.gbxOutput.Controls.Add(this.lblResults);
            this.gbxOutput.Location = new System.Drawing.Point(11, 122);
            this.gbxOutput.Name = "gbxOutput";
            this.gbxOutput.Size = new System.Drawing.Size(743, 56);
            this.gbxOutput.TabIndex = 13;
            this.gbxOutput.TabStop = false;
            this.gbxOutput.Text = "Output";
            this.gbxOutput.Enter += new System.EventHandler(this.gbxOutput_Enter);
            // 
            // btnResults
            // 
            this.btnResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResults.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnResults.Location = new System.Drawing.Point(663, 18);
            this.btnResults.Margin = new System.Windows.Forms.Padding(2);
            this.btnResults.Name = "btnResults";
            this.btnResults.Size = new System.Drawing.Size(75, 23);
            this.btnResults.TabIndex = 7;
            this.btnResults.Text = "save";
            this.btnResults.UseVisualStyleBackColor = false;
            this.btnResults.Click += new System.EventHandler(this.btnResults_Click);
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.BackColor = System.Drawing.SystemColors.GrayText;
            this.txtResults.ForeColor = System.Drawing.SystemColors.Window;
            this.txtResults.Location = new System.Drawing.Point(73, 20);
            this.txtResults.Margin = new System.Windows.Forms.Padding(2);
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(586, 20);
            this.txtResults.TabIndex = 5;
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(27, 23);
            this.lblResults.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(42, 13);
            this.lblResults.TabIndex = 6;
            this.lblResults.Text = "Results";
            this.lblResults.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgSaveCsv
            // 
            this.dlgSaveCsv.Filter = "csv-File|*.csv";
            // 
            // Results
            // 
            this.Results.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Results.AutoSize = true;
            this.Results.BackColor = System.Drawing.SystemColors.GrayText;
            this.Results.Location = new System.Drawing.Point(362, 183);
            this.Results.Margin = new System.Windows.Forms.Padding(2);
            this.Results.Name = "Results";
            this.Results.Size = new System.Drawing.Size(81, 23);
            this.Results.TabIndex = 14;
            this.Results.Text = "Open Results";
            this.Results.UseVisualStyleBackColor = false;
            this.Results.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Crimson;
            this.ClientSize = new System.Drawing.Size(766, 647);
            this.Controls.Add(this.Results);
            this.Controls.Add(this.gbxOutput);
            this.Controls.Add(this.gbxInput);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtLog);
            this.ForeColor = System.Drawing.Color.Snow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "CWU CS TA Assignment";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbxInput.ResumeLayout(false);
            this.gbxInput.PerformLayout();
            this.gbxOutput.ResumeLayout(false);
            this.gbxOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtApplications;
        private System.Windows.Forms.Label lblApplications;
        private System.Windows.Forms.Button btnApplications;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.TextBox txtSchedule;
        private System.Windows.Forms.Button btnGrades;
        private System.Windows.Forms.Label lblGrades;
        private System.Windows.Forms.TextBox txtGrades;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.OpenFileDialog dlgOpenCsv;
        private System.Windows.Forms.GroupBox gbxInput;
        private System.Windows.Forms.GroupBox gbxOutput;
        private System.Windows.Forms.Button btnResults;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.SaveFileDialog dlgSaveCsv;
        private System.Windows.Forms.Button Results;
    }
}

