using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace cwu.cs.TaAssignments
{
    public partial class TableForm : Form
    {
        public TableForm()
        {
            InitializeComponent();
            ltvTable
                .GetType()
                .GetProperty("DoubleBuffered", (System.Reflection.BindingFlags)(-1))
                .SetValue(ltvTable, true);
        }

        public void SetTable(List<List<string>> table)
        {
            ltvTable.SuspendLayout();
            ltvTable.Items.Clear();
            ltvTable.Columns.Clear();

            for (int r = 0; r < table.Count; r++)
            {
                List<string> rowData = table[r];
                ListViewItem rowItem = new ListViewItem();

                while (ltvTable.Columns.Count < rowData.Count)
                {
                    ltvTable.Columns.Add(string.Empty);
                }

                if (rowData.Count > 0)
                {
                    rowItem.Text = rowData[0];
                }

                for (int c = 1; c < rowData.Count; c++)
                {
                    rowItem.SubItems.Add(rowData[c]);
                }

                ltvTable.Items.Add(rowItem);
            }

            ltvTable.ResumeLayout();
        }
    }
}
