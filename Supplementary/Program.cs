using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Supplementary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            //obj.PrintHello();
            //obj.PrintSum();
            //int area = obj.CalculateArea(200, 300);
            //Console.WriteLine(area);
            //obj.PrintSwapPrint();
            //int max = obj.GetMaxValue();
            //Console.WriteLine(max);
            //obj.PrintGrades(3);
            //obj.CalculateFactorial();
            //for (int i = 3; i < 40; i++)
            //{
            //    bool prime = obj.IsPrime(i);
            //    Console.WriteLine(i + " = "+ prime);
            //}
            //obj.GeneratePrimes(100);
            //obj.GeneratePrimeSeries(40);
            //obj.SumOfPositives(5);
            //int[,] multiplicationTables = obj.MultiplicationTables(2, 6, 5);
            //Console.WriteLine(obj.LeastCommonMultiple());
            //obj.QuibbleSort("923453174270".ToCharArray());
            obj.QuibbleSort("3D4431DBA69".ToCharArray());
            Console.ReadKey();
        }

        void PrintHello()
        {
            Console.WriteLine("Hello!");
        }

        void PrintSum()
        {
            Console.WriteLine("Enter two numbers to add: ");
            int number1 = Int16.Parse(Console.ReadLine());
            int number2 = Int16.Parse(Console.ReadLine());
            int sum = number1 + number2;
            Console.WriteLine(sum);
        }

        int CalculateArea(int width, int height)
        {
            if (width < 0 || height < 0)
            {
                throw new Exception("Invalid input");
            }
            int area = width * height;
            return area;
        }

        void PrintSwapPrint()
        {
            string s1 = Console.ReadLine();
            string s2 = Console.ReadLine();
            Console.WriteLine(s1);
            Console.WriteLine(s2);
            string temp = s1;
            s1 = s2;
            s2 = temp;
            Console.WriteLine(s1);
            Console.WriteLine(s2);
        }

        int GetMaxValue()
        {
            int max = 0;
            int i = 0;
            while (i < 5)
            {
                int number = Int16.Parse(Console.ReadLine());
                if (number > max)
                {
                    max = number;
                }
                i++;
            }
            return max;
        }

        void PrintGrades(int n)
        {
            if (n > 0)
            {
                int i = 1;
                while (i <= n)
                {
                    Console.WriteLine("Enter marks: ");
                    int marks = Int16.Parse(Console.ReadLine());
                    if (marks < 0 || marks > 100)
                    {
                        Console.WriteLine("Invalid number");
                    }
                    else
                    {
                        if (marks < 60)
                            Console.WriteLine("F");
                        else if (marks <= 65)
                            Console.WriteLine("C");
                        else if (marks <= 71)
                            Console.WriteLine("C+");
                        else if (marks <= 77)
                            Console.WriteLine("B");
                        else if (marks <= 86)
                            Console.WriteLine("B+");
                        else
                            Console.WriteLine("A");
                        i++;
                    }
                }
            }
        }

        void CalculateFactorial()
        {
            Console.WriteLine("Enter a number to calculate factorial: ");
            int number = Int16.Parse(Console.ReadLine());
            long factorial = number;
            for (int i = 2; i < number; i++)
            {
                factorial *= i;
            }
            Console.WriteLine(factorial);
        }

        bool IsPrime(int n)
        {
            for (int i = 2; i < n; i++)
            {
                int rem = n % i;
                if (rem == 0)
                {
                    return false;
                }
            }
            return true;
        }

        void GeneratePrimes(int n)
        {
            for (int i = 2; i < n; i++) 
            {
                bool isPrime = IsPrime(i);
                if (isPrime)
                {
                    Console.WriteLine(i);
                }
            }
        }

        void GeneratePrimeSeries(int n)
        {
            int i = 2;
            while (i <= n)
            {
                int j = 2;
                bool prime = true;
                while (j < i)
                {
                    int rem = i % j;
                    if (rem == 0)
                    {
                        prime = false;
                        break;
                    }
                    j++;
                }
                if (prime)
                {
                    Console.WriteLine(i);
                }
                i++;
            }
        }

        void SumOfPositives(int n)
        {
            int[] array = new int[n];
            Console.WriteLine($"Input {n} numbers: ");
            for (int i = 0; i < n; i++)
            {
                int number = Int16.Parse(Console.ReadLine());
                array[i] = number;
            }
            int sum = 0;
            int positives = 0;
            foreach(int i in array)
            {
                if (i > 0)
                {
                    positives++;
                    sum += i;
                }
            }
            float average = sum / positives;
            Console.WriteLine(average);
        }

        int[,] MultiplicationTables(int start, int end, int range)
        {
            int j = 0;
            int[,] table = new int[end-start + 1, range];
            while (start <= end)
            {
                for (int i = 1; i <= range; i++)
                {
                    table[j, i - 1] = start * i;
                }
                start++;
                j++;
            }
            return table;
        }
        
        int LeastCommonMultiple()
        {
            Console.WriteLine($"Input the numbers to find out LCM: ");
            int num1 = Int16.Parse(Console.ReadLine());
            int num2 = Int16.Parse(Console.ReadLine());
            int gcd = 0;
            int a = num1, b = num2;
            int steps = 0;
            if (num1 > num2)
            {
                a = num2;
                b = num1;
            }
            while (b % a != 0)
            {
                gcd = a;
                a = b % a;
                b = gcd;
                steps++;
            }
            Console.WriteLine("Steps: " + steps);
            int lcm = (num1 * num2) / gcd;
            return lcm;
        }

        char[] OneStepBubbleSort(char[] data, int numBase)
        {
            for (int j = 1; j < data.Length - 1; j += 2)
            {
                int num1 = Convert.ToInt32(data[j].ToString(), numBase);
                int num2 = Convert.ToInt32(data[j - 1].ToString(), numBase);
                if (num2 > num1)
                {
                    char temp = data[j];
                    data[j] = data[j - 1];
                    data[j - 1] = temp;
                }
            }
            return data;
        }

        char[] QuibbleSort(char[] data)
        {
            if (data.Length <= 1)
            {
                return data;
            }
            foreach (char c in data)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine("Executing Bubble sort");
            // Single step Bubble sort
            data = OneStepBubbleSort(data, 16);
            foreach (char c in data)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
            char pivot = data[0];
            int i = 1;
            int j = data.Length - 1;
            while (i < j)
            {
                while (i < data.Length && data[i] < pivot)
                { i++; }
                while (j >= i && data[j] >= pivot)
                { j--; }
                // Swap elements at indices i and j
                Console.WriteLine($"Swapping i: {data[i]} and j: {data[j]}");
                if (j > i)
                {
                    char temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                }
            }
            // Swap the pivot element with the element at index j
            data[0] = data[j];
            data[j] = pivot;
            // Recursively sort the two partitions
            char[] left = QuibbleSort(data.Take(j).ToArray());
            char[] mid = new char[] { data[j] };
            char[] right = QuibbleSort(data.Skip(j + 1).ToArray());
            // Merge the left, mid and right partitions
            string leftStr = "";
            string rightStr = "";
            string midStr = mid[0].ToString();
            foreach (char l in left)
                leftStr += l.ToString() + ' ';
            foreach (char l in right)
                rightStr += l.ToString() + ' ';
            Console.WriteLine($"Merging [{leftStr}], [{midStr}], and [{rightStr}]");
            return left.Concat(mid).Concat(right).ToArray();
        }
    }
}