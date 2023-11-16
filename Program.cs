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
            DemoMapReduce();

            Console.ReadKey();
        }

        private static void DemoMatrixMultiplication()
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

        private static void DemoStringMatching()
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

        private static void DemoPasswordCracking()
        {
            int n = 5;
            char[] charset = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            Console.WriteLine("\nThis will generate " + CalculateCombinations(charset.Length, n) + " combinations. Press any key to continue...");
            Console.ReadKey();
            GenerateStrings(charset, n, new char[n], 0);
            Console.ReadKey();
        }

        private static long CalculateCombinations(int n, int r)
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

        private static char[] GenerateStrings(char[] characters, int length, char[] result, int currentIndex)
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

        private static void DemoMapReduce()
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

        private static void MapFiles(string[] filePaths, ConcurrentBag<ConcurrentDictionary<string, int>> mapOutput)
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

    }
}
