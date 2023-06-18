using System;
using System.Collections.Generic;
using System.Text;


namespace cwu.cs.TaAssignments
{
    /// <summary>
    /// A log of text data that replaces the terminal output of the original Java program.
    /// </summary>
    class TextLog
    {
        // Stores the output.
        static StringBuilder log = new StringBuilder();

        /// <summary>
        /// Is caused when the text of the log changes.
        /// </summary>
        public static event EventHandler TextUpdate;

        public static void WriteLine()
        {
            log.AppendLine();
            TextUpdate?.Invoke(null, new EventArgs());
        }

        public static void WriteLine(object obj)
        {
            log.AppendLine(obj.ToString());
            TextUpdate?.Invoke(null, new EventArgs());
        }

        public static void Write(object obj)
        {
            log.Append(obj.ToString());
            TextUpdate?.Invoke(null, new EventArgs());
        }

        public static void Clear()
        {
            log.Clear();
            TextUpdate?.Invoke(null, new EventArgs());
        }

        public new static string ToString()
        {
            return log.ToString();
        }
    }
}
