using System;
using System.Collections.Generic;

namespace cwu.cs.TaAssignments
{
    partial class Section
    {
        // List of courses that do not get TAs assigned.
        private static HashSet<int> ignoredCourses = new HashSet<int> { 184, 325, 392, 481, 489, 492, 495, 496 };

        // List of courses whos labs are a different section.
        private static HashSet<int> labCourses = new HashSet<int> {105};

        static bool IsCourseIgnored(int number)
        {
            return
                number >= 500 || // Ignore graduate courses.
                ignoredCourses.Contains(number);
        }

        public static Dictionary<int, Section> fromCsvFile(List<List<string>> sectionData)
        {
            Dictionary<int, Section> sections = new Dictionary<int, Section>();

            for (int i = 1 /* skip headers */; i < sectionData.Count; i++)
            {
                List<string> line = sectionData[i];

                // Check status.
                if (line[3] != "A") continue;

                // Ignore arranged courses.
                if (line[24].Contains("ARR_ARRANGED")) continue;

                Section sec = new Section();
                sec.title = line[8];
                sec.instructor = Instructor.ByName(line[9]);
                sec.ID = int.Parse(line[4]);
                sec.number = int.Parse(line[6]);
                sec.section = line[7].PadLeft(3, '0');
                sec.labTimes = new Time(line[16], line[17]);
                sec.room = line[19];
                sec.inSamLab = sec.room.StartsWith("SAMU");
                sec.inEburg = line[20] == "EBURG";

                if (IsCourseIgnored(sec.number)) continue;


                // Check if it is a LEP or LAB course.
                // If the lab is a different section, pick the entry which has 0 credits.
                if (labCourses.Contains(sec.number) && int.Parse(line[11]) != 0) continue;


                if (sections.ContainsKey(sec.ID))
                {
                    Section other = sections[sec.ID];

                    if (sec.inSamLab == other.inSamLab)
                    {
                        // Not clear what the lab is.
                        // Merge both into one.
                        other.inEburg |= sec.inEburg;
                        other.labTimes |= sec.labTimes;
                    }
                    else if (sec.inSamLab)
                    {
                        // Replace other.
                        sections[sec.ID] = sec;
                    }
                }
                else
                {
                    sections.Add(sec.ID, sec);
                }
            }

            return sections;
        }
    }
}
