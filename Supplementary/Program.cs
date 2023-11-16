using System;
using System.Collections.Generic;
using System.Linq;
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
            obj.SumOfPositives(5);
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
            foreach (int i in array)
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
    }
}