using System;
using System.Collections.Generic;


namespace cwu.cs.TaAssignments
{
    class GraphBuilder
    {
        Section[] allSections;
        Student[] allStudents;

        public HashSet<Student> noPotentialMatach = null;
        public HashSet<Student> notEnoughTime = null;
        public HashSet<Student> noGrades = null;

        public Graph graph = null;

        public int startId = -1;
        public int endId = -1;

        public Dictionary<int, int> studentToVertex = null;
        public Dictionary<int, int> vertexToSection = null;

        private static HashSet<int> noGradeNeeded = new HashSet<int> { 105, 109, 367 };
        private static HashSet<int> coreCourses = new HashSet<int>
        {
            110, 111, 112,
            301, 302, 311, 312, 325, 361, 362, 380,
            420, 427, 450, 480, 481, 489
        };
        private static HashSet<int> electiveCourses = new HashSet<int>
        {
            351, 370, 428, 430, 440, 445, 446, 455,
            456, 457, 460, 465, 467, 471, 473, 475
        };

        public GraphBuilder
        (
            Section[] allSections,
            Student[] allStudents
        )
        {
            this.allSections = allSections;
            this.allStudents = allStudents;
        }

        public void buildGraph()
        {
            List<List<int>> edges = new List<List<int>>();
            List<List<int>> weights = new List<List<int>>();
            List<List<int>> capacities = new List<List<int>>();

            // Start vertex.
            startId = 0;
            edges.Add(new List<int>());
            weights.Add(new List<int>());
            capacities.Add(new List<int>());

            // End vertex.
            endId = 1;
            edges.Add(new List<int>());
            weights.Add(new List<int>());
            capacities.Add(new List<int>());


            // Add students.
            studentToVertex = new Dictionary<int, int>(allStudents.Length);
            Dictionary<int, int> vertexToStudent = new Dictionary<int, int>(allStudents.Length);

            notEnoughTime = new HashSet<Student>();

            for (int i = 0; i < allStudents.Length; i++)
            {
                Student student = allStudents[i];

                // Weight based on graduation date.
                int studWeight = 10000 * student.GetBufferQuarters();

                if (studWeight < 0)
                {
                    notEnoughTime.Add(student);
                    studWeight = 0;
                }


                int vertId = edges.Count;

                studentToVertex.Add(student.ID, vertId);
                vertexToStudent.Add(vertId, student.ID);

                edges.Add(new List<int>());
                weights.Add(new List<int>());
                capacities.Add(new List<int>());

                // Edge from start vertex to student.
                edges[startId].Add(vertId);
                weights[startId].Add(studWeight);
                capacities[startId].Add(1);
            }


            // Add sections.
            Dictionary<int, int[]> sectionToVertex = new Dictionary<int, int[]>(allSections.Length);
            vertexToSection = new Dictionary<int, int>(allSections.Length);

            const int TA392_1 = 1;
            const int TA392_2 = 2;
            const int TA492_1 = 3;

            for (int i = 0; i < allSections.Length; i++)
            {
                Section section = allSections[i];

                int secId = section.ID;
                int vertId = edges.Count;

                // One vertex for the section, three for possible students.
                // 392 (1st), 392 (2nd), 492
                int[] vertIDs = new int[] { vertId, vertId + TA392_1, vertId + TA392_2, vertId + TA492_1 };

                sectionToVertex.Add(secId, vertIDs);
                vertexToSection.Add(vertId + TA392_1, secId);
                vertexToSection.Add(vertId + TA392_2, secId);
                vertexToSection.Add(vertId + TA492_1, secId);

                // Vertex for section.
                edges.Add(new List<int>());
                weights.Add(new List<int>());
                capacities.Add(new List<int>());

                // Edge from section to end vertex.

                int secWeight = 1000; // Scale of weights.
                if (coreCourses.Contains(section.number))
                {
                    secWeight *= 0;
                }
                else if (electiveCourses.Contains(section.number))
                {
                    secWeight *= 1;
                }
                else
                {
                    secWeight *= 2;
                }

                edges[vertId].Add(endId);
                weights[vertId].Add(secWeight);
                capacities[vertId].Add(2); // At most two TAs

                // Edges from student-slot to section.
                foreach (int taSlot in new int[] { TA392_1, TA392_2, TA492_1 })
                {
                    edges.Add(new List<int>());
                    weights.Add(new List<int>());
                    capacities.Add(new List<int>());

                    edges[vertId + taSlot].Add(vertId);
                    capacities[vertId + taSlot].Add(1); // One TA per slot.
                }

                // ToDo: Determine proper weights.
                weights[vertId + TA392_1].Add(1);
                weights[vertId + TA392_2].Add(1000); // High weight to ensure only used if no other option.
                weights[vertId + TA492_1].Add(1);
            }

            noPotentialMatach = new HashSet<Student>();
            noGrades = new HashSet<Student>();

            // Match students with sections.
            for (int i = 0; i < allStudents.Length; i++)
            {
                Student student = allStudents[i];
                int studVerId = studentToVertex[student.ID];

                if (student.grades == null)
                {
                    noGrades.Add(student);
                    continue;
                }

                for (int j = 0; j < allSections.Length; j++)
                {
                    Section section = allSections[j];
                    int secId = section.ID;
                    int[] secVerIDs = sectionToVertex[secId];


                    bool hasTime = student.availableTimes >= section.labTimes;

                    bool hasGrade =
                        student.grades.ContainsKey(section.number) &&
                        student.grades[section.number] <= 9 /* C */;

                    bool hasOtherExp = false;
                    if (noGradeNeeded.Contains(section.number))
                    {
                        if (section.number == 105) hasOtherExp = student.vbExperience;
                        if (section.number == 109) hasOtherExp = student.pyExperience;
                        if (section.number == 105) hasOtherExp = student.vbExperience;
                    }

                    bool canAttend = !section.inEburg || student.inEburg;


                    if (!hasTime || !(hasGrade || hasOtherExp) || !canAttend) continue;


                    // Student can be TA for this course.

                    // Update weight based on grades.
                    // Is in range [3, 13] with 3 = A and 13 = D-.
                    int gradeScore =
                        student.grades.ContainsKey(section.number)
                        ?
                            student.grades[section.number]
                        :
                            9; // C if student has no grade.

                    // Increase by 2 grades if class is not listed by student.
                    if (student.coursesTaken.Contains(section.number))
                    {
                        gradeScore += 6;
                    }

                    int totalScore = 2 * gradeScore;

                    if (student.course == 392)
                    {
                        edges[studVerId].Add(secVerIDs[TA392_1]);
                        weights[studVerId].Add(totalScore);
                        capacities[studVerId].Add(1);

                        edges[studVerId].Add(secVerIDs[TA392_2]);
                        weights[studVerId].Add(totalScore);
                        capacities[studVerId].Add(1);
                    }
                    else if (student.course == 492)
                    {
                        edges[studVerId].Add(secVerIDs[TA492_1]);
                        weights[studVerId].Add(totalScore);
                        capacities[studVerId].Add(2);
                    }
                    else
                    {
                        noPotentialMatach.Add(student);
                    }
                }


                if (edges[studVerId].Count == 0)
                {
                    noPotentialMatach.Add(student);
                }
            }

            // Studnet matched with classes.
            // Graph complete.

            graph = new Graph();
            graph.noOfVertices = edges.Count;
            graph.edges = toIntArrayArray(edges);
            graph.weights = toIntArrayArray(weights);
            graph.capacities = toIntArrayArray(capacities);
        }

        /**
         * Determines the default weight of a student based on its graduation time.
         * (Summer quarters will not be considered as potential quarters to do 3/492.)
         */

        private static int[][] toIntArrayArray(List<List<int>> list)
        {
            int[][] arr = new int[list.Count][];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = list[i].ToArray();
            }

            return arr;
        }
    }
}
