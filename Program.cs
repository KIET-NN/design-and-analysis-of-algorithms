using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.IO;


namespace workspace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Demonstrating Password cracking */
            //DemoPasswordCracking();

            /* Demonstrating String matching */
            //DemoStringMatching();

            /* Demonstrating Matrix multiplication */
            //DemoMatrixMultiplication();

            /* Demonstrate Map-Reduce mock */
            //DemoMapReduce();

            /* Demonstrate Merge Sort */
            //DemoMergeSort();

            /* Demonstrate Quick Sort */
            //DemoQuickSort();

            /* Demonstrate N-queen solution */
            //DemoNQueen();

            /* Demonstrate Fibonacci using Dynamic programming */
            DemoDynamicProgramming();

            Console.ReadKey();
        }

        static void DemoMatrixMultiplication()
        {
            int[,] A = { { 9, 6 }, { 2, 2 } };
            int[,] B = { { 5, 4 }, { 0, 4 } };
            int[,] C = { { 1, 5 }, { 5, 4 } };
            int[,] D = { { 7, 6 }, { 3, 2 } };
            int[,] result =
                MultiplyMatrices(
                    MultiplyMatrices(
                        MultiplyMatrices(A, B), C), D);
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Console.Write(result[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void DemoStringMatching()
        {
            string target =
                            "GTACCGGTAATCTAGGCTAGTTGCGCATGACCTTACGGTACCTGAGTAGCGGTTTGGACCGATAGTGTAGCTTAGCGGACGCTGCTACGTATAGCGGATA" +
                            "GCGGTACCGCTACGTTGCGCATAGCGGTTGGTGTAGCTTAGGGTACCTTACGGTACCGCTACGGTACCGGTTTGGGTTGGGTTGGTGTAGCTTAGGACCG" +
                            "TTTGGGTTGGACCGGACCTACGGTGTAGCTTAGCGGACGCTGACCGTTTGGTACCGACCTTACGTTGGACCGGACCTACGGTTTGGTGTAGCTTAGGACC" +
                            "GGACCTTACGGTGTAGCTTACGGTGTAGCTTACGGTTTGGTACCGGACCTGAGTGTAGCTTACGGTGTAGCTTACGGTGTAGCTTACGGGACCGTTTGGT" +
                            "GTAGCTGACCGGACCTACGGTGTAGCTTAGGACCGACCTTAGGTTGGACCGTTTGGACCGGTTTGGTACCGGACCTTAGGACCTTACGGTACCGTTTGGG" +
                            "TTGGACCTTAGCGGACGCTGCTTAGGACCGTTGGTACCGTTGGTACCGGTTTGGACCGGACCTTAGGTTGGTGTAGCTTAGGTTGGACCGGACCTACGGT" +
                            "GTAGCTTACGGTTTGGACCGTTTGGTACCGACCTTACGGTGTAGCTTAGGACCGTTGGACCGTTTGGGTTGGGTTGGACCTTAGGTTGGTACCGTTGGTA" +
                            "CCGTTGGACCGACCTTAGGTTGGACCGTTGGGTTGGTACCGACCTTAGGTTGGACCGTTTGGACCTTACGGTGTAGCTTAGCGGTTGGTACCGACCTTAC" +
                            "GGTACCGGTTTGGACCTTAGCGGACGCTGCTTAGGTTGGACCGGACCTTAGGTTGGTACCGACCTTACGGTTTGGTACCGGACCTTAGGTTGGTGTAGCT" +
                            "TAGGTTGGACCGTTTGGGTTGGACCTTAGGTTGGTACCGTTGGTACCGTTGGACCGACCTTAGGTTGGACCGTTTGGACCTTACGGTGTAGCTTAGCGGT" +
                            "TGGTACCGACCTTACGGTACCGGTTTGGACCTTAGCGGACGCTGCTTAGGTTGGACCGGACCTTAGGTTGGTACCGACCTTACGGTTTGGTACCGGACCT" +
                            "TAGGTTGGTGTAGCTTAGGTTGGACCGTTTGGGTTGGACCTTAGGTTGGTACCGTTGGTACCGTTGGACCGACCTTAGGTTGGACCGTTTGGACCTTACG" +
                            "GTGTAGCTTAGCGGTTGGTACCGACCTTACGGTACCGGTTTGGACCTTAGCGGACGCTGCTTAGGTTGGACCGGACCTTAGGTTGGTACCGACCTTACGG" +
                            "TTTGGTACCGGACCTTAGGTTGGTGTAGCTTAGGTTGGACCGTTTGGGTTGGACCTTAGGTTGGTACCGTTGGTACCGTTGGACCGACCTTAGGTTGGAC" +
                            "CGTTTGGACCTTACGGTGTAGCTTAGCGGTTGGTACCGACCTTACGGTACCGGTTTGGACCTTAGCGGACGCTGCTTAGGTTGGACCGGACCTTAGGTTG" +
                            "GTACCGACCTTACGGTTTGGTACCGGACCTTAGGTTGGTGTAGCTTAGGTTGGACCGTTTGGGTTGGACCTTAGGTTGGTACCGTTGGTACCGTTGGACC" +
                            "GACCTTAGGTTGGACCGTTTGGACCTTACGGTGTAGCTTAGCGGTTGGTACCGACCTTACGGTACCGGTTTGGACCTTAGCGGACGCTGCTTAGGTTGGA" +
                            "CCGGACCTTAGGTTGGTACCGACCTTACGGTTTGGTACCGGACCTTAGGTTGGTGTAGCTTAGGTTGGACCGTTTGGGTTGGACCTTAGGTTGGTACCGT" +
                            "TGGTACCGTTGGACCGACCTTAGGTTGGACCGTTTGGACCTTACGGTGTAGCTTAGCGGTTGGTACCGACCTTACGGTACCGGTTTGGACCTTAGCGGAC" +
                            "GCTGCTTAGGTTGGACCGGACCTTAGGTTGGTACCGACCTTACGGTTTGGTACCGGACCTTAGGTTGGTGTAGCTTAGGTTGGACCGTTTGGGTTGGACC" +
                            "TTAGGTTGGTACCGTTGGTACCGTTGGACCGACCTTAGGTTGGACCGTTTGGACCTTACGGTGTAGCTTAGCGGTTGGTACCGACCTTACGGTACCGGTT" +
                            "TGGACCTTAGCGGACGCTGCTTAGGTTGGACCGGACCTTAGGTTGGTACCGACCTTACGGTTTGGTACCGGACCTTAGGTTGGTGTAGCTTAGGTTGGAC" +
                            "CGTTTGGGTTGGACCTTAGGTTGGTACCGTTGGTACCGTTGGACCGACCTTAGGTTGGACCGTTTGGACCTTACGGTGTAGCTTAGCGGTTGGTACCGAC" +
                            "CTTACGGTACCGGTTTGGACCTTAGCGGACGCTGCTTAGGTTGGACCGGACCTTAGGTTGGTACCGACCTTACGGTTTGGTACCGGACCTTAGGTTGGTG" +
                            "TAGCTTAGGTTGGACCGTTTGGGTTGGACCTTAGGTTGGTACCGTTGGTACCGTTGGACCGACCTTAGGTTGGACCGTTTGGACCTTACGGTGTAGCTTA" +
                            "GCGGTTGGTACCGACCTTACGGTACCGGTTTGGACCTTAGCGGACGCTGCTTAGGTTGGACCGGACCTTAGGTTGGTACCGACCTTACGGTTTGGTACCG" +
                            "GACCTTAGGTTGGTGTAGCTTAGGTTGGACCGTTTGGGTTGGACCTTAGGTTGGTACCGTTGGTACCGTTGGACCGACCTTAGGTTGGACCGTTTGGACC" +
                            "TTACGGTGTAGCTTAGCGGTTGGTACCGACCTT";
            //target = "ACDCTGTACTGCDTTGCCACDTGACTGTACTTGTACACGATC";
            string pattern = "TGTA";
            List<int> indices = MatchStrings(target, pattern);
            foreach (int i in indices)
            {
                Console.WriteLine(i);
            }
        }

        static List<int> MatchStrings(string target, string pattern)
        {
            List<int> matches = new List<int>();
            int i = 0;
            while (target.Length > pattern.Length + i)
            {
                string slice = target.Substring(i, pattern.Length);
                if (slice.Equals(pattern))
                {
                    matches.Add(i);
                }
                i++;
            }
            return matches;
        }

        static int[,] MultiplyMatrices(int[,] A, int[,] B)
        {
            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);
            int rowsB = B.GetLength(0);
            int colsB = B.GetLength(1);
            if (colsA != rowsB) throw new ArgumentException("Dimensions incompatible");
            int[,] C = new int[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    C[i, j] = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return C;
        }

        static void DemoPasswordCracking()
        {
            int n = 5;
            char[] charset = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            Console.WriteLine("\nThis will generate " + CalculateCombinations(charset.Length, n) + " combinations. Press any key to continue...");
            Console.ReadKey();
            GenerateStrings(charset, n, new char[n], 0);
            Console.ReadKey();
        }

        static long CalculateCombinations(int n, int r)
        {
            if (r < 0 || r > n)
            {
                return 0;
            }
            long result = 1;
            int smaller = Math.Min(r, n - r);
            for (int i = 1; i <= smaller; i++)
            {
                result *= n--;
                result /= i;
            }
            return result;
        }

        static char[] GenerateStrings(char[] characters, int length, char[] result, int currentIndex)
        {
            if (currentIndex == length)
            {
                Console.Write(result);
                Console.Write("\t");
                return result;
            }
            foreach (char c in characters)
            {
                result[currentIndex] = c;
                GenerateStrings(characters, length, result, currentIndex + 1);
            }
            return null;
        }

        static void DemoMapReduce()
        {
            String[] filePaths = Directory.GetFiles("../../LOTR", "*.txt");
            var mapOutput = new ConcurrentBag<ConcurrentDictionary<string, int>>();
            MapFiles(filePaths, mapOutput);
            var combinedWordFrequencies = ReduceWordFrequencies(mapOutput);
            foreach (var entry in combinedWordFrequencies)
            {
                if (entry.Value > 10)
                {
                    Console.WriteLine($"\"{entry.Key}\" occurred {entry.Value} times");
                }
            }
        }

        static void MapFiles(string[] filePaths, ConcurrentBag<ConcurrentDictionary<string, int>> mapOutput)
        {
            Parallel.ForEach(filePaths, filePath =>
            {
                var wordFrequencies = CalculateWordFrequencies(filePath);
                mapOutput.Add(wordFrequencies);
            });
        }

        static ConcurrentDictionary<string, int> CalculateWordFrequencies(string filePath)
        {
            var wordFrequencies = new ConcurrentDictionary<string, int>();
            foreach (var line in File.ReadLines(filePath))
            {
                var words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(word => word.ToLower())
                                .Where(word => !string.IsNullOrWhiteSpace(word));
                foreach (var word in words)
                {
                    wordFrequencies.AddOrUpdate(word, 1, (_, count) => count + 1);
                }
            }
            return wordFrequencies;
        }

        static ConcurrentDictionary<string, int> ReduceWordFrequencies(ConcurrentBag<ConcurrentDictionary<string, int>> mapOutput)
        {
            var combinedWordFrequencies = new ConcurrentDictionary<string, int>();
            Parallel.ForEach(mapOutput, wordFrequency =>
            {
                foreach (var entry in wordFrequency)
                {
                    combinedWordFrequencies.AddOrUpdate(entry.Key, entry.Value, (_, count) => count + entry.Value);
                }
            });
            return combinedWordFrequencies;
        }

        static void DemoMergeSort()
        {
            int[] array = { 994, 1949, 1422, 1475, 870, 1729, 1954, 2363, 1965, 1952, 1696, 0873, 1413, 1969, 1717 };
            int[] sorted = MergeSort(array, 0, array.Length - 1);
            foreach (int i in sorted)
            {
                Console.Write(i + "\t");
            }
        }

        static int[] MergeSort(int[] array, int start, int end)
        {
            if (start < end)
            {
                int mid = (end + start) / 2;
                int[] left = MergeSort(array, start, mid);
                int[] right = MergeSort(array, mid + 1, end);
                return Merge(left, right);
            }
            return new int[] { array[start] };
        }

        static int[] Merge(int[] left, int[] right)
        {
            int[] merged = new int[left.Length + right.Length];
            int i = 0, l = 0, r = 0;
            // Run for the items which match in length
            while (l < left.Length && r < right.Length)
            {
                if (left[l] < right[r])
                {
                    merged[i] = left[l++];
                }
                else
                {
                    merged[i] = right[r++];
                }
                i++;
            }
            // Copy remaining items from left
            while (l < left.Length)
            {
                merged[i++] = left[l++];
            }
            // Copy remaining items from right
            while (r < right.Length)
            {
                merged[i++] = right[r++];
            }
            return merged;
        }

        static void DemoQuickSort()
        {
            //int[] array = { 994, 1949, 1422, 1475, 870, 1729, 1954, 2363, 1965, 1952, 1696, 0873, 1413, 1969, 1717 };
            int[] array = { 9, 8, 6, 10, 1, 6, 15, 2, 13, 14 };

            int[] sorted = Quicksort(array);
            foreach (int i in sorted)
            {
                Console.Write(i + "\t");
            }
        }

        static int[] Quicksort(int[] array)
        {
            if (array.Length <= 1)
            {
                return array;
            }
            int pivot = array[0];
            int i = 1;
            int j = array.Length - 1;
            while (i < j)
            {
                while (i < array.Length && array[i] < pivot)
                { i++; }
                while (array[j] > pivot)
                { j--; }
                // Swap elements at indices i and j
                if (j > i)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            // Swap the pivot element with the element at index j
            array[0] = array[j];
            array[j] = pivot;
            // Recursively sort the two partitions
            int[] left = Quicksort(array.Take(j).ToArray());
            int[] mid = new int[] { array[j] };
            int[] right = Quicksort(array.Skip(j + 1).ToArray());
            // Merge the left, mid and right partitions
            string leftStr = "";
            string rightStr = "";
            string midStr = mid[0].ToString();
            foreach (int l in left)
                leftStr += l.ToString() + ' ';
            foreach (int l in right)
                rightStr += l.ToString() + ' ';
            Console.WriteLine($"Merging [{leftStr}], [{midStr}], and [{rightStr}]");
            return left.Concat(mid).Concat(right).ToArray();
        }

        static void DemoNQueen()
        {
            int n = 8;
            int[,] positions = new int[n, n];
            positions = NQueens(n, 1, positions);
        }

        static int[,] NQueens(int n, int current, int[,] positions)
        {
            // Termination condition. All queens have been placed
            if (current == n) return positions;
            // See if any queens are attacking each other
            int i = 0;
            while (i < n)
            {
                positions[current - 1, i] = 1;
                if (IsAttacking(positions))
                {
                    positions[current - 1, i] = 0;
                    i++;
                }
            }
            // Recursive step
            return NQueens(n, current + 1, positions);
        }

        static bool IsAttacking(int[,] positions)
        {
            int horizontalSum = 0;
            int verticalSum = 0;
            for (int i = 0; i < positions.Length; i++)
            {
                for (int j = 0; j < positions.Length; j++)
                {
                    horizontalSum += positions[j, i];
                    verticalSum += positions[i, j];
                }
            }
            if (verticalSum > 1 || horizontalSum > 1)
            {
                return true;
            }
            return false;
        }
    
        static void DemoDynamicProgramming()
        {
            Console.WriteLine("Simple recursive implementation:");
            for (int i = 1; i < 40; i++)
            {
                Console.Write(Fibonacci(i) + " ");
            }
            Console.WriteLine("\nDynamic programming implementation:");
            for (int i = 1; i < 40; i++)
            {
                Console.Write(FibonacciDynamicProgramming(i, new int[i]) + " ");
            }
        }

        static int Fibonacci(int n)
        {
            if (n <= 2) return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        static int FibonacciDynamicProgramming(int n, int[] cache)
        {
            if (n <= 2) return 1;
            if (cache[n - 1] > 0) return cache[n - 1];
            int fib = FibonacciDynamicProgramming(n - 1, cache) + FibonacciDynamicProgramming(n - 2, cache);
            cache[n - 1] = fib;
            return fib;
        }
    }
}
