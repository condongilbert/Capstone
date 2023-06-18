using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cwu.cs.TaAssignments
{
    class CsvBuilder
    {
        const string CsvSep = ",";

        List<string[]> lines = new List<string[]>();

        public void AddLine()
        {
            lines.Add(new string[] { });
        }

        public void AddLine(string field)
        {
            AddLine(new string[] { field });
        }

        public void AddLine(string[] line)
        {
            if (line == null)
            {
                AddLine();
                return;
            }

            lines.Add(line);
        }

        public void ToFile(string path)
        {
            StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

            for (int i = 0; i < lines.Count; i++)
            {
                string[] line = lines[i];

                if (line == null || line.Length == 0)
                {
                    writer.WriteLine();
                    continue;
                }

                writer.Write(line[0]);
                for (int j = 1; j < line.Length; j++)
                {
                    writer.Write(CsvSep);
                    writer.Write(Escape(line[j]));
                }
                writer.WriteLine();
            }

            writer.Dispose();
        }

        private string Escape(string field)
        {
            bool hasQts = field.Contains("\"");
            bool hasSep = field.Contains(CsvSep);

            string escQts = hasQts || hasSep ? "\"" : string.Empty;

            return
                escQts +
                (hasQts ? field.Replace("\"", "\"\"") : field) +
                escQts;
        }
    }
}
