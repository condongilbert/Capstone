using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;


namespace cwu.cs.TaAssignments
{
    class Student
    {
        public int ID;

        public string lastName;
        public string firstName;
        public string email;

        public DateTime submissionTime;

        public int graduation;

        public int course;

        // Each bit represents one timeslot.
        // One day has 8 bits.
        // Least significant bit represents Monday 8am.
        // Most significant bit represents Thursday 3pm.
        public Time availableTimes;

        public bool pyExperience;
        public bool vbExperience;

        public bool inEburg;

        public HashSet<int> coursesTaken;
        public Dictionary<int, int> grades;


        public Student(List<string> csvLine)
        {
            const int subTimeCol = 6;

            const int idCol = 2;
            const int nameCol = 0;
            const int emailCol = 8;
            const int gradQuarCol = 10;
            const int applyForCol = 12;
            const int inEburgCol = 14;

            const int MoCol = 18;
            const int TuCol = 20;
            const int WeCol = 22;
            const int ThCol = 24;

            const int pyCol = 26;
            // const int vbCol = 28;
            const int classesCol = 28;


            ID = int.Parse(csvLine[idCol]);


            string[] allNames =
                csvLine[nameCol].Split
                (
                    new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries
                );

            lastName = allNames[1];
            firstName = allNames[0];
            email = csvLine[emailCol];

            submissionTime = DateTime.ParseExact
            (
                csvLine[subTimeCol],
                "yyyy-MM-dd HH:mm:ss UTC",
                CultureInfo.InvariantCulture
            );

            // Format: yyyy,quarter
            string gradYQ = csvLine[gradQuarCol];

            // Ensure a valid graduation date.
            if (!gradYQ.Matches(@"\d\d\d\d\,(Winter|Spring|Summer|Fall)"))
            {
                // If no graduation date given, set it in 10 years.
                gradYQ = $"{ DateTime.Now.Year + 10 },Spring";
            }

            int gradYear = int.Parse(gradYQ.Substring(0, 4));
            string gradQuarter = gradYQ.Substring(5);

            switch (gradQuarter)
            {
                case "Winter":
                    graduation = gradYear * 3 + 0;
                    break;

                case "Spring":
                case "Summer":
                    graduation = gradYear * 3 + 1;
                    break;

                case "Fall":
                    graduation = gradYear * 3 + 2;
                    break;
            }

            switch (csvLine[applyForCol])
            {
                case "CS 392":
                    course = 392;
                    break;

                case "CS 492":
                    course = 492;
                    break;
            }

            inEburg = csvLine[inEburgCol].StartsWith("Yes");


            // -- Parse time on each day of the week. --

            int[] cols = { MoCol, TuCol, WeCol, ThCol };

            long timeSlots = 0;
            for (int d = 0; d < Time.DaysPerWeek; d++)
            {
                string dayData = csvLine[cols[d]].ToLower();

                Regex timeRex = new Regex(@"(\d?\d)(a|p)m_open");

                foreach (Match m in timeRex.Matches(dayData))
                {
                    int hour = int.Parse(m.Groups[1].Value);
                    if (m.Groups[2].Value == "p" && hour < 12 /* 12pm is noon */) hour += 12;

                    int bit = d * Time.SlotsPerDay + (hour - Time.FirstClassHour);
                    timeSlots |= (1L << bit);
                }
            }
            availableTimes = new Time(timeSlots);

            pyExperience = csvLine[pyCol] == "Yes";
            vbExperience = false; // csvLine[vbCol] == "Yes";


            coursesTaken = new HashSet<int>();
            string classList = csvLine[classesCol];

            Regex classRex = new Regex(@"CS \d\d\d");

            foreach (Match m in classRex.Matches(classList))
            {
                coursesTaken.Add(int.Parse(m.Value.Substring(3)));
            }
        }

        public string getGradQuarter()
        {
            switch (graduation % 3)
            {
                case 0: return "Winter";
                case 1: return "Spring/Summer";
                case 2: return "Fall";
            }

            return null;
        }

        public int GetBufferQuarters()
        {
            DateTime today = DateTime.Today;

            int year = today.Year;
            int month = today.Month;

            int currQuarter = -1;

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    // Currently Winter quarter.
                    currQuarter = year * 3 + 0;
                    break;

                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    // Currently Spring/Summer quarter.
                    currQuarter = year * 3 + 1;
                    break;

                case 10:
                case 11:
                case 12:
                    // Currently Fall quarter.
                    currQuarter = year * 3 + 2;
                    break;
            }

            int quartersLeft = graduation - currQuarter;
            int quartersNeeded = course == 492 ? 1 : 2;

            return quartersLeft - quartersNeeded;
        }

        public static Student[] fromCsvFile(List<List<string>> studentData, List<List<string>> gradesData)
        {
            Dictionary<int, Dictionary<int, int>> allGrades = parseGrades(gradesData);
            Dictionary<int, Student> parsedStudents = new Dictionary<int, Student>();

            Dictionary<Student, HashSet<int>> noClassGrades = new Dictionary<Student, HashSet<int>>();

            for (int i = 1 /* ignore header line */; i < studentData.Count; i++)
            {
                Student student = new Student(studentData[i]);

                // Replaces previous entry if student already in table.
                if (!parsedStudents.ContainsKey(student.ID))
                {
                    parsedStudents.Add(student.ID, student);
                }
                else if (parsedStudents[student.ID].submissionTime < student.submissionTime)
                {
                    parsedStudents[student.ID] = student;
                }


                // ----------------------------------------
                // Check grades for student and their selected classes.

                if (!allGrades.ContainsKey(student.ID)) continue;

                student.grades = allGrades[student.ID];

                foreach (int course in student.coursesTaken)
                {
                    if (student.grades.ContainsKey(course)) continue;

                    if (!noClassGrades.ContainsKey(student))
                    {
                        noClassGrades.Add(student, new HashSet<int>());
                    }

                    noClassGrades[student].Add(course);
                }
            }


            // ----------------------------------------
            // Print students with errors in grades.

            if (noClassGrades.Count > 0)
            {
                TextLog.WriteLine("*** Students without grades for classes they want to TA: ");

                foreach (Student student in noClassGrades.Keys)
                {
                    List<int> classLst = new List<int>(noClassGrades[student]);
                    classLst.Sort();

                    TextLog.Write(student.ID + " ");
                    TextLog.Write(student.lastName + ", ");
                    TextLog.Write(student.firstName + ":");

                    for (int i = 0; i < classLst.Count; i++)
                    {
                        TextLog.Write(" " + classLst[i]);
                    }

                    TextLog.WriteLine();
                }
                TextLog.WriteLine();
            }


            // ----------------------------------------
            // Return result.

            return parsedStudents.Values.ToArray();
        }

        private static Dictionary<int, Dictionary<int, int>> parseGrades(List<List<string>> gradesData)
        {
            // --- Read Columns ---

            Dictionary<string, int> columns = new Dictionary<string, int>();

            {
                List<string> tableHead = gradesData[0];

                for (int i = 0; i < tableHead.Count; i++)
                {
                    columns.Add(tableHead[i], i);
                }
            }

            Dictionary<int, Dictionary<int, int>> allGrades = new Dictionary<int, Dictionary<int, int>>();

            int idCol = columns["ID"];
            int subjectCol = columns["Subject"];
            int courseCol = columns["Catalog"];
            int gradetCol = columns["Grade"];
            int categoryCol = columns["Grade Category"];

            for (int i = 1; i < gradesData.Count; i++)
            {
                List<string> csvLine = gradesData[i];

                if (csvLine[subjectCol] != "CS") continue;
                if (!csvLine[idCol].Matches("\\d+")) continue;
                if (!csvLine[courseCol].Matches("\\d\\d\\d")) continue;
                if (csvLine[categoryCol].Contains("Transfer")) continue;

                int id = int.Parse(csvLine[idCol]);
                int course = int.Parse(csvLine[courseCol]);

                // ----------------------------------------
                // Translate grade into number.
                string gradeStr = csvLine[gradetCol];

                if (string.IsNullOrEmpty(gradeStr)) continue;

                if (!gradeStr.Matches("EP|[ABCDFIS](\\+|-)?"))
                {
                    TextLog.WriteLine("unable to parse grade: " + gradeStr);
                    continue;
                }

                int gradeScore = 0;

                switch (gradeStr[0])
                {
                    case 'A':
                        gradeScore = 1;
                        break;

                    case 'B':
                        gradeScore = 2;
                        break;

                    case 'C':
                    case 'S':
                    case 'E' /* EP */:
                        gradeScore = 3;
                        break;

                    case 'D':
                        gradeScore = 4;
                        break;

                    default:
                        // Non-passing grade.
                        continue;
                }

                // Multiply with 3 to account for + and -.
                gradeScore *= 3;

                if (gradeStr.Length > 1)
                {
                    if (gradeStr[1] == '+') gradeScore--;
                    if (gradeStr[1] == '-') gradeScore++;
                }


                // ----------------------------------------
                // Add grade to "list".
                if (!allGrades.ContainsKey(id))
                {
                    allGrades.Add(id, new Dictionary<int, int>());
                }

                Dictionary<int, int> studGrades = allGrades[id];

                if (studGrades.ContainsKey(course))
                {
                    studGrades[course] = Math.Min(studGrades[course], gradeScore);
                }
                else
                {
                    studGrades.Add(course, gradeScore);
                }
            }

            return allGrades;
        }

        public override bool Equals(object obj)
        {
            Student other = obj as Student;
            return other != null && this.ID == other.ID;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }
    }
}
