using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace cwu.cs.TaAssignments
{
    class Assignments
    {
        public static void StartWatching(string fileStudents, string fileSections, string fileGrades, string fileResult)
        {
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(fileStudents),
                Filter = "*.csv",
                NotifyFilter = NotifyFilters.LastWrite
            };

            watcher.Changed += (source, e) =>
            {
                // Ensure the changed file is one of the input files
                if (new[] { fileStudents, fileSections, fileGrades }.Contains(e.FullPath))
                {
                    Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
                    Compute(new[] { fileStudents, fileSections, fileGrades, fileResult });
                }
            };

            watcher.EnableRaisingEvents = true;
        }
        public static void Compute(string[] args)
        {
            TextLog.WriteLine("--- TA-Assignment ---");

            string fileStudents = args[0];
            string fileSections = args[1];
            string fileGrades = args[2];
            string fileResult = args[3];

            TextLog.WriteLine("Student Applications: " + fileStudents);
            TextLog.WriteLine("    Offered Sections: " + fileSections);
            TextLog.WriteLine("              Grades: " + fileGrades);
            TextLog.WriteLine("              Output: " + fileResult);
            TextLog.WriteLine();

            List<List<string>> studentData = CsvParser.parseFile(fileStudents);
            List<List<string>> sectionData = CsvParser.parseFile(fileSections);
            List<List<string>> gradesData = CsvParser.parseFile(fileGrades);

            cleanData(studentData);
            cleanData(sectionData);
            cleanData(gradesData);

            Dictionary<int, Section> sectionById = Section.fromCsvFile(sectionData);
            Section[] allSections = sectionById.Values.ToArray();
            Array.Sort(allSections);


            // ----------------------------------------
            // Output all offered sections.

            TextLog.WriteLine("*** Sections available for TAs:");

            int[] schClasses = getScheduleClasses(allSections);

            for (int i = 0; i < allSections.Length; i++)
            {
                Section sec = allSections[i];
                TextLog.WriteLine(sec.number + "." + sec.section + " " + sec.title);
            }
            TextLog.WriteLine();

            TextLog.WriteLine(allSections.Length + " total sections");
            TextLog.WriteLine(schClasses.Length + " total classes");
            TextLog.WriteLine();


            // ----------------------------------------
            // Parse students.

            Student[] allStudents = Student.fromCsvFile(studentData, gradesData);

            TextLog.WriteLine(allStudents.Length + " total students");


            // ----------------------------------------
            // Build graph and compute matching.

            GraphBuilder gb = new GraphBuilder(allSections, allStudents);
            gb.buildGraph();
            Graph g = gb.graph;
            int[][] flow = g.minCostMaxFlow(gb.startId, gb.endId);


            // Output students without grades.
            if (gb.noGrades.Count > 0)
            {
                TextLog.WriteLine();
                TextLog.WriteLine("*** Students without any grades:");

                foreach (Student student in gb.noGrades)
                {
                    TextLog.WriteLine
                    (
                        student.ID.ToString() + " " +
                        student.lastName + ", " + student.firstName
                    );
                }
            }



            // ----------------------------------------
            // Interprete result.

            List<Student> noMatch = new List<Student>();

            // All matches "ordered" by Section-ID.
            Dictionary<int, List<Student>> matches = new Dictionary<int, List<Student>>();   while 

            for (int i = 0; i < allStudents.Length; i++)
            {
                Student student = allStudents[i];
                int vId = gb.studentToVertex[student.ID];

                int[] neighs = g.edges[vId];
                int[] capacities = flow[vId];

                bool matchFound = false;

                for (int j = 0; j < capacities.Length; j++)
                {
                    if (capacities[j] == 0) continue;

                    matchFound = true;
                    int secId = gb.vertexToSection[neighs[j]];

                    if (!matches.ContainsKey(secId)) matches.Add(secId, new List<Student>());
                    matches[secId].Add(student);
                }

                if (!matchFound)
                {
                    noMatch.Add(student);
                }
            }

            HashSet<Student> needMatch = new HashSet<Student>();

            foreach (Student stu in noMatch)
            {
                if (stu.GetBufferQuarters() <= 0)
                {
                    needMatch.Add(stu);
                }
            }

            if (needMatch.Count > 0)
            {
                TextLog.WriteLine();
                TextLog.WriteLine("*** Students not assigned but need assignment to graduate in time.");

                foreach (Student stu in needMatch)
                {
                    TextLog.WriteLine
                    (
                        stu.ID.ToString() + " " +
                        stu.lastName + ", " + stu.firstName + " " +
                        stu.course + " "
                    );
                }
            }


            // ----------------------------------------
            // Output results.

            HashSet<Section> noStudent = new HashSet<Section>();
            CsvBuilder AssignedOutput = new CsvBuilder();
            CsvBuilder UnassignedStudentOutput = new CsvBuilder();
            CsvBuilder UnassignedCourseOutput = new CsvBuilder();

            AssignedOutput.AddLine(new string[]
            {
                "class",

                // Faculty
                "FirstName",
                "FacultyName",
                "facultyEmail",

                // Student 1
                "StudentName1",
                "StudentFirstName1",
                "Course1",
                "numCredits",
                "studentEmail1",

                // Student 2
                "StudentName2",
                "StudentFirstName2",
                "Course2",
                "numCredits",
                "studentEmail2",

                // Time and Room
                "day",
                "startTime",
                "endTime",
                "Room"
            });

            for (int i = 0; i < allSections.Length; i++)
            {
                Section sec = allSections[i];
                if (!matches.ContainsKey(sec.ID))
                {
                    noStudent.Add(sec);
                    continue;
                }

                List<Student> stuLst = matches[sec.ID];
                if (stuLst[0].course == 492) stuLst.Reverse();

                List<string> line = new List<string>();

                line.Add(sec.number + "." + sec.section);

                // Faculty
                line.Add(sec.instructor.firstName);
                line.Add(sec.instructor.lastName);
                line.Add(sec.instructor.email);

                // Students
                for (int j = 0; j < 2; j++)
                {
                    if (j < stuLst.Count)
                    {
                        Student stu = stuLst[j];

                        line.Add(stu.lastName);
                        line.Add(stu.firstName);
                        line.Add(stu.course.ToString());
                        line.Add(stu.course == 392 ? "1" : "2");
                        line.Add(stu.email);
                    }
                    else
                    {
                        line.Add(string.Empty);
                        line.Add(string.Empty);
                        line.Add(string.Empty);
                        line.Add(string.Empty);
                        line.Add(string.Empty);
                    }
                }

                // Time and Room
                line.Add(sec.labTimes.GetLongDayString());
                line.Add(sec.labTimes.GetStartTimes());
                line.Add(sec.labTimes.GetEndTimes());
                line.Add(sec.room);

                AssignedOutput.AddLine(line.ToArray());
            }


            // -- Students not assigned. --

            UnassignedStudentOutput.AddLine();
            UnassignedStudentOutput.AddLine("*** Students without assignment ***");

            UnassignedStudentOutput.AddLine(new string[]
            {
                "ID",
                "Last Name",
                "First Name",
                "Email",
                "Graduation Quarter",
                "Graduation Year",
                "Course Applied",
                "Needs assignment?",
            });

            for (int i = 0; i < noMatch.Count; i++)
            {
                Student stud = noMatch[i];

                UnassignedStudentOutput.AddLine(new string[]
                {
                    stud.ID.ToString(),
                    stud.lastName,
                    stud.firstName,
                    stud.email,
                    stud.getGradQuarter(),
                    (stud.graduation / 3).ToString(),
                    $"CS { stud.course }",
                    needMatch.Contains(stud) ? "YES" : "no"
                });
            }


            // -- Classes without students. --

            UnassignedCourseOutput.AddLine();
            UnassignedCourseOutput.AddLine("*** Classes without students ***");

            UnassignedCourseOutput.AddLine(new string[]
            {
                "Class",
                "Section",
            });

            foreach (Section sec in noStudent)
            {
                UnassignedCourseOutput.AddLine(new string[]
                {
                    sec.number.ToString(),
                    sec.section,
                });
            }

            //creates 3 different files for user
            TextLog.WriteLine();
            AssignedOutput.ToFile("AssignedStudents.csv");
            UnassignedStudentOutput.ToFile("UnassignedStudents.csv");
            UnassignedCourseOutput.ToFile("UnassignedCourses.csv");

        }

        private static int[] getScheduleClasses(Section[] allSections)
        {
            HashSet<int> classNums = new HashSet<int>();

            for (int i = 0; i < allSections.Length; i++)
            {
                classNums.Add(allSections[i].number);
            }

            return classNums.ToArray();
        }

        private static int[] getApplicationClasses(List<string> lst)
        {
            HashSet<int> classNums = new HashSet<int>();

            for (int i = 0; i < lst.Count; i++)
            {
                string cell = lst[i];

                if (!cell.Matches("CS \\d\\d\\d - .*")) continue;

                string classStr = cell.Substring(3, 3);
                int classNum = int.Parse(classStr);
                classNums.Add(classNum);
            }

            return classNums.ToArray();
        }

        /**
         * Remove leading and tailing spaces from csv-data.
         */
        private static void cleanData(List<List<string>> data)
        {
            if (data == null) return;

            foreach (List<string> line in data)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    line[i] = line[i].Trim();
                }
            }
        }
    }
}
