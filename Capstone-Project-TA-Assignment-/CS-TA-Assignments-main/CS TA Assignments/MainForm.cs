using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace cwu.cs.TaAssignments
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.Text += " (" + Application.ProductVersion + ")";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtResults.Text = Path.Combine(Application.StartupPath, "results.csv");

            txtApplications.Text = FindFile("applications.csv");
            txtGrades.Text = FindFile("grades.csv");
            txtSchedule.Text = FindFile("schedule.csv");
        }

        string FindFile(string name)
        {
            string[] searchDirs = new string[]
            {
                Application.StartupPath,
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            foreach (string dir in searchDirs)
            {
                if (File.Exists(Path.Combine(dir, name))) return Path.Combine(dir, name);
            }

            return string.Empty;
        }

        void OnTextUpdate(object sender, EventArgs e)
        {
            txtLog.Text = TextLog.ToString();
        }

        private void btnApplications_Click(object sender, EventArgs e)
        {
            if (dlgOpenCsv.ShowDialog() != DialogResult.OK) return;
            txtApplications.Text = dlgOpenCsv.FileName;
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            if (dlgOpenCsv.ShowDialog() != DialogResult.OK) return;
            txtSchedule.Text = dlgOpenCsv.FileName;
        }

        private void btnGrades_Click(object sender, EventArgs e)
        {
            if (dlgOpenCsv.ShowDialog() != DialogResult.OK) return;
            txtGrades.Text = dlgOpenCsv.FileName;
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            if (dlgSaveCsv.ShowDialog() != DialogResult.OK) return;
            txtResults.Text = dlgSaveCsv.FileName;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            TextLog.Clear();

            string studentsCsv = txtApplications.Text;
            string scheduleCsv = txtSchedule.Text;
            string gradesCsv = txtGrades.Text;
            string resultsCsv = txtResults.Text;

#if DEBUG
#else
            try
#endif
            {
                Assignments.Compute(new string[] { studentsCsv, scheduleCsv, gradesCsv, resultsCsv });
            }
#if DEBUG
#else
            catch (Exception ex)
            {
                TextLog.WriteLine();
                TextLog.WriteLine("*** Error: " + ex.GetType().Name + " ***");
                TextLog.WriteLine(ex.Message);
                TextLog.WriteLine();
                TextLog.WriteLine(ex.StackTrace);

                MessageBox.Show
                (
                    ex.Message,
                    ex.GetType().Name,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
#endif
            txtLog.Text = TextLog.ToString();
        }

        private void gbxOutput_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string exePath = Path.GetDirectoryName(Application.ExecutablePath);
            string debugFolder = Directory.GetParent(exePath).FullName;
            string additionalFolder = Path.Combine(debugFolder, "AdditionalFolder");

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = additionalFolder;

            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|Word Documents (*.docx;*.doc)|*.docx;*.doc";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Process.Start(new ProcessStartInfo(openFileDialog.FileName) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            
        }
    }
}
