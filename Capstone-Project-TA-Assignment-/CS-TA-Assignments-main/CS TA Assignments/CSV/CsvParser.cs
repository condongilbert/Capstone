using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace cwu.cs.TaAssignments
{

    class CsvParser
    {
        // Based on: https://agiletribe.wordpress.com/2012/11/23/the-only-class-you-need-for-csv-files/.

        public static List<string> parseLine(StreamReader r)
        {
            List<string> store = new List<string>();

            string line = r.ReadLine();
            if (line == null) return store;

            StringBuilder curVal = new StringBuilder();

            bool inquotes = false;
            bool started = false;

            for (int i = 0; /* Check is at start of loop */; i++)
            {
                if (i >= line.Length)
                {
                    // End of line in file reached.
                    // Are we still in a string?
                    if (!inquotes)
                    {
                        // No. End parsing.
                        break;
                    }

                    // Yes. Continue with next line.
                    do
                    {
                        curVal.AppendLine();
                        line = r.ReadLine();
                    }
                    while (line.Length == 0);
                    i = 0;
                }

                char ch = line[i];

                if (inquotes)
                {
                    started = true;
                    if (ch == '\"')
                    {
                        inquotes = false;
                    }
                    else
                    {
                        curVal.Append(ch);
                    }
                }
                else
                {
                    if (ch == '\"')
                    {
                        inquotes = true;
                        if (started)
                        {
                            // If this is the second quote in a value, add a quote.
                            // This is for the double quote in the middle of a value
                            curVal.Append('\"');
                        }
                    }
                    else if (ch == ',' || ch == ';')
                    {
                        store.Add(curVal.ToString());
                        curVal = new StringBuilder();
                        started = false;
                    }
                    else
                    {
                        curVal.Append(ch);
                    }
                }
            }

            store.Add(curVal.ToString());
            return store;
        }

        public static List<List<string>> parseFile(string fileName)
        {
            List<List<string>> fileContent = new List<List<string>>();
            if (string.IsNullOrEmpty(fileName)) return fileContent;

            for (StreamReader reader = new StreamReader(fileName); !reader.EndOfStream;)
            {
                List<string> line = parseLine(reader);
                if (line == null) break;

                fileContent.Add(line);
            }

            return fileContent;
        }
    }
}
