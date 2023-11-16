using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Groupify
{
    internal class Student
    {
        public int id { get; set; }
        public int rank { get; set; }

        public Student(int id, int rank) 
        {
            this.id = id;
            this.rank = rank;
        }

        public string ToString()
        {
            return $"{id}, {rank}";
        }
    }

    internal class Group
    {
        public List<Student> students { get; set; }

        public Double rankSum { get; set; }

        public Group(List<Student> students, Double rankSum)
        {
            this.students = students;
            this.rankSum = rankSum;
        }

        public void SumRanks()
        {
            rankSum = 0;
            foreach (Student student in students)
            {
                rankSum += student.rank;
            }
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Student student in students)
            {
                sb.Append($"{student.ToString()}; ");
            }
            SumRanks();
            sb.AppendLine(rankSum.ToString());
            return sb.ToString();
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            students.Add(new Student(12347, 1));
            students.Add(new Student(11729, 2));
            students.Add(new Student(11422, 3));
            students.Add(new Student(12363, 4));
            students.Add(new Student(11954, 5));
            students.Add(new Student(11956, 6));
            students.Add(new Student(11955, 7));
            students.Add(new Student(12398, 7));
            students.Add(new Student(13042, 7));
            students.Add(new Student(10724, 8));
            students.Add(new Student(12359, 9));
            students.Add(new Student(11696, 10));
            students.Add(new Student(10870, 11));
            students.Add(new Student(13143, 11));
            students.Add(new Student(11873, 12));
            students.Add(new Student(64044, 13));
            students.Add(new Student(11453, 14));
            students.Add(new Student(11475, 14));
            students.Add(new Student(11969, 15));
            students.Add(new Student(11943, 16));
            students.Add(new Student(13050, 16));
            students.Add(new Student(11965, 17));
            students.Add(new Student(11860, 18));
            //students.Add(new Student(12584, 19));
            //students.Add(new Student(11413, 20));
            //students.Add(new Student(12633, 21));
            //students.Add(new Student(11952, 22));
            //students.Add(new Student(10856, 23));
            //students.Add(new Student(11717, 24));
            //students.Add(new Student(12104, 25));
            //students.Add(new Student(10959, 26));
            //students.Add(new Student(11363, 27));
            //students.Add(new Student(10521, 28));
            //students.Add(new Student(11972, 28));
            //students.Add(new Student(11407, 29));
            //students.Add(new Student(10930, 30));
            //students.Add(new Student(11949, 31));
            //students.Add(new Student(10994, 32));
            //students.Add(new Student(12121, 33));
            //students.Add(new Student(10384, 34));
            //students.Add(new Student(61348, 35));
            //students.Add(new Student(11946, 36));
            //students.Add(new Student(63117, 36));
            //students.Add(new Student(64016, 37));
            //students.Add(new Student(62053, 38));
            //students.Add(new Student(61459, 39));
            List<Group> groups;
            int i = 1;
            int groupLimit = 4;

            //Console.Write($"\n\nGroupifying linearly: ");
            //groups = Groupify(students, groupLimit);
            //foreach (Group group in groups)
            //{
            //    Console.Write($"\nGroup No. {i++}: ");
            //    Console.Write(group.ToString());
            //}
            //Console.Write($"Standard deviation is: {CalculateStandardDeviation(groups)}");

            //i = 1;
            //Console.Write($"\n\nGroupifying using Brute force: ");
            //groups = GroupifyBruteForce(students, groupLimit);
            //foreach (Group group in groups)
            //{
            //    Console.Write($"\nGroup No. {i++}: ");
            //    Console.Write(group.ToString());
            //}
            //Console.Write($"Standard deviation is: {CalculateStandardDeviation(groups)}");

            //i = 1;
            //Console.Write($"\n\nGroupifying using Greedy: ");
            //groups = GroupifyGreedy(students, groupLimit);
            //foreach (Group group in groups)
            //{
            //    Console.Write($"\nGroup No. {i++}: ");
            //    Console.Write(group.ToString());
            //}
            //Console.Write($"Standard deviation is: {CalculateStandardDeviation(groups)}");

            i = 1;
            Console.Write($"\n\nGroupifying using Divide and Conquer: ");
            groups = GroupifyDivideAndConquer(students, groupLimit);
            foreach (Group group in groups)
            {
                Console.Write($"\nGroup No. {i++}: ");
                Console.Write(group.ToString());
            }
            Console.Write($"Standard deviation is: {CalculateStandardDeviation(groups)}");

            Console.ReadKey();
        }

        static double CalculateStandardDeviation(List<Group> groups)
        {
            if (groups == null || groups.Count == 0)
            {
                return double.MaxValue;
            }
            // This part was generated via ChatGPT
            double mean = groups.Average(group => group.rankSum);
            double variance = groups.Select(group => Math.Pow(group.rankSum - mean, 2)).Average();
            return Math.Sqrt(variance);
        }

        static List<Group> Groupify(List<Student> students, int groupLimit)
        {
            // Divide into n groups such that there are no more than allowed limit per item
            int n = students.Count / groupLimit;    // Take integer, or apply floor function
            int m = n + (students.Count % groupLimit);  // If there'current a remainder, then there'll be an additional item
            Console.WriteLine($"A total of {m} groups will be formed.");
            List<Group> groups = new List<Group>(m);
            Queue<Student> T = new Queue<Student>();
            Queue<Student> B = new Queue<Student>();
            Queue<Student> M = new Queue<Student>();
            int i = 0, j = students.Count - 1;
            for (; i < m; i++, j--)
            {
                T.Enqueue(students[i]);
                B.Enqueue(students[j]);
            }
            for (; i < students.Count - m; i++)
            {
                M.Enqueue(students[i]);
            }
            //Console.WriteLine("\nTop:");
            // Rule 1: From the Top m students, no more than 1 s1 in one item
            // Rule 2: At least 1 s1 from Botton m students in each item
            while (T.Count > 0)
            {
                Student s1 = T.Dequeue();
                Student s2 = B.Dequeue();
                List<Student> list = new Student[] {s1, s2 }.ToList();
                Group g = new Group(list, 0);
                g.SumRanks();
                groups.Add(g);
            }
            // Rule 3: Up to 2 students from the remaining groups
            while (true)
            {
                foreach (Group group in groups)
                {
                    if (M.Count == 0)
                    {
                        return groups;
                    }
                    group.students.Add(M.Dequeue());
                }
            }
        }

        static List<Group> GroupifyBruteForce(List<Student> students, int groupLimit)
        {
            int n = students.Count / groupLimit;    // Take integer, or apply floor function
            int m = n + (students.Count % groupLimit);  // If there'current a remainder, then there'll be an additional item
            Console.WriteLine($"A total of {m} groups will be formed.");
            List<Group> groups = new List<Group>(m);
            Queue<Student> T = new Queue<Student>();
            Queue<Student> B = new Queue<Student>();
            int i = 0;
            for (; i < m; i++)
            {
                T.Enqueue(students[i]);
            }
            for (; i < students.Count; i++)
            {
                B.Enqueue(students[i]);
            }
            // First of all, create groups with one student from T in each
            while (T.Count > 0)
            {
                groups.Add(new Group(new List<Student> { T.Dequeue() }, 0));
            }
            // Iteratively update the groups by adding m batches of students each time
            List<Student> batch = new List<Student>();
            while (B.Count > 0)
            {
                batch.Add(B.Dequeue());
                if (batch.Count == m)
                {
                    groups = FindOptimalByBruteForce(groups, batch);
                    batch = new List<Student>();
                }
            }
            return groups;
        }

        static List<Group> FindOptimalByBruteForce(List<Group> groups, List<Student> studentPool)
        {
            int startFrom = 0;
            List<Group> optimal = new List<Group>();
            // Form all possible result of T with B and update optimal list of groups whenever a match is found
            while (startFrom < studentPool.Count)
            {
                List<Group> currentSet = new List<Group>(groups.Count);
                // Since this is a shallow copy, we would need to make a clone of each students list
                foreach (Group group in groups)
                {
                    Group groupCopy = new Group(new List<Student>(group.students), 0);
                    currentSet.Add(groupCopy);
                }
                int i = startFrom;
                foreach (Group currentSetGroup in currentSet)
                {
                    currentSetGroup.students.Add(studentPool[i]);
                    currentSetGroup.SumRanks();
                    // So that the index resets and beings from 0, otherwise increment
                    i = i == studentPool.Count - 1 ? 0 : i + 1;
                }
                startFrom++;
                double currentSetAverage = CalculateStandardDeviation(currentSet);
                double optimalAverage = CalculateStandardDeviation(optimal);
                if (currentSetAverage < optimalAverage)
                {
                    Console.WriteLine($"Updating optimal group since the average of current group {currentSetAverage} < {optimalAverage}.");
                    optimal = new List<Group>(currentSet);
                }
            }
            return optimal;
        }

        static List<Group> GroupifyGreedy(List<Student> students, int groupLimit)
        {
            int n = students.Count / groupLimit;    // Take integer, or apply floor function
            int m = n + (students.Count % groupLimit);  // If there'current a remainder, then there'll be an additional item
            Console.WriteLine($"A total of {m} groups will be formed.");
            List<Group> groups = new List<Group>(m);
            Queue<Student> T = new Queue<Student>();
            Queue<Student> B = new Queue<Student>();
            int i = 0;
            for (; i < m; i++)
            {
                T.Enqueue(students[i]);
            }
            // Copy the remaining in B group
            for (; i < students.Count; i++)
            {
                B.Enqueue(students[i]);
            }
            // First of all, create groups with one student from T in each
            while (T.Count > 0)
            {
                groups.Add(new Group(new List<Student> { T.Dequeue() }, 0));
            }
            return FindOptimalByGreedy(groups, B.ToList());
        }

        static List<Group> FindOptimalByGreedy(List<Group> groups, List<Student> studentPool)
        {
            while (studentPool.Count > 0)
            {
                // Identify the student with last rank
                Student lastRanked = studentPool[0];
                foreach (Student student in studentPool)
                {
                    if (student.rank > lastRanked.rank)
                    {
                        lastRanked = student;
                    }
                }
                Group bestCandidate = groups[0];
                foreach (Group group in groups)
                {
                    // Pick the group with lowest sum
                    if (group.rankSum < bestCandidate.rankSum)
                    {
                        bestCandidate = group;
                    }
                }
                // Assign lastRanked student to bestCandidate group
                bestCandidate.students.Add(lastRanked);
                bestCandidate.SumRanks();
                // Remove from the pool
                studentPool.Remove(lastRanked);
            }
            return groups;
        }

        static List<Group> GroupifyDivideAndConquer(List<Student> students, int groupLimit)
        {
            int n = students.Count / groupLimit;    // Take integer, or apply floor function
            int m = n + (students.Count % groupLimit);  // If there'current a remainder, then there'll be an additional item
            Console.WriteLine($"A total of {m} groups will be formed.");
            List<Group> groups = new List<Group>(m);
            Queue<Student> T = new Queue<Student>();
            Queue<Student> B = new Queue<Student>();
            int i = 0;
            for (; i < m; i++)
            {
                T.Enqueue(students[i]);
            }
            // Copy the remaining in B group
            for (; i < students.Count; i++)
            {
                B.Enqueue(students[i]);
            }
            // First of all, create groups with one student from T in each
            while (T.Count > 0)
            {
                groups.Add(new Group(new List<Student> { T.Dequeue() }, 0));
            }
            return FindOptimalByDivideAndConquer(groups, B.ToList(), groupLimit);
        }

        static List<Group> FindOptimalByDivideAndConquer(List<Group> groups, List<Student> studentPool, int groupLimit)
        {
            // Find all result of 3 students in studentPool
            List<Group> combinations = new List<Group>();
            List<int[]> result = new List<int[]>();
            int[] studentIds = studentPool.Select(student => student.id).ToArray();
            GenerateCombinations(studentIds, new int[] { }, groupLimit - 1, result);
            // Print combinations (sit back and relax!)
            Console.WriteLine($"There are {result.Count} combinations...");
            foreach (int[] studentList in result)
            {
                List<Student> students = new List<Student>();
                foreach (int studentId in studentList)
                {
                    students.Add(studentPool.Find(s => s.id == studentId));
                }
                Group g = new Group(students, 0);
                g.SumRanks();
                Console.Write(g.ToString());
            }

            foreach (int[] studentList in result)
            {
                Group group = new Group(new List<Student>(), 0);
                foreach (int studentId in studentList)
                {
                    Student student = studentPool.FirstOrDefault(s => s.id == studentId);
                    group.students.Add(student);
                }
                group.SumRanks();
                combinations.Add(group);
            }
            // Pick the best combination
            combinations = combinations.OrderBy(g => g.rankSum).ToList();
            // Question! Can you simply pick the top m from this list?
            return groups;
        }

        static void GenerateCombinations(int[] remaining, int[] current, int r, List<int[]> result)
        {
            if (remaining.Length == 0)
            {
                if (current.Length == r)
                {
                    int[] combination = new int[r];
                    Array.Copy(current, combination, r);
                    result.Add(combination);
                }
            }
            else
            {
                // For strings
                //GenerateCombinations(remaining.Substring(1), result, current + remaining[0]);
                //GenerateCombinations(remaining.Substring(1), result, current);

                // For int array
                int[] newRemaining = new int[remaining.Length - 1];
                Array.Copy(remaining, 1, newRemaining, 0, remaining.Length - 1);

                int[] newCurrent = new int[current.Length + 1];
                Array.Copy(current, newCurrent, current.Length);
                newCurrent[current.Length] = remaining[0];

                GenerateCombinations(newRemaining, newCurrent, r, result);
                GenerateCombinations(newRemaining, current, r, result);
            }
        }
    }
}
