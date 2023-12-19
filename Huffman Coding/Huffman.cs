using System;
using System.Collections.Generic;
using System.Linq;

namespace Huffman_Coding
{
    internal class DoublyNode
    {
        public char data;
        public int freq;
        public DoublyNode left, right;

        public DoublyNode(char data, int freq)
        {
            left = right = null;
            this.data = data;
            this.freq = freq;
        }
    }

    internal class Huffman
    {
        static void Main(string[] args)
        {
            Huffman obj = new Huffman();
            string s = "MUHAMMADBINAHMEDABUREHANALBERUNI";
            Dictionary<char, int> dictionary = obj.GetFrequencyDistributionTable(s);
            foreach (char c in dictionary.Keys)
            {
                Console.WriteLine($"{c}: {dictionary[c]}");
            }
            Dictionary<string, string> codes = obj.GetHuffmanCodes(s);
            Console.WriteLine("\nHuffman Codes:");
            foreach (var code in codes)
            {
                Console.WriteLine($"{code.Key}: {code.Value}");
            }
            Console.ReadKey();
        }

        public Dictionary<Char, int> GetFrequencyDistributionTable(string s)
        {
            Dictionary<Char, int> dictionary = new Dictionary<char, int>();
            foreach (char c in s)
            {
                if (dictionary.ContainsKey(c))
                    dictionary[c]++;
                else
                    dictionary.Add(c, 1);
            }
            var sortedDictionary = dictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return sortedDictionary;
        }

        public Dictionary<string, string> GetHuffmanCodes(string s)
        {
            Dictionary<Char, int> frequencyTable = GetFrequencyDistributionTable(s);
            DoublyNode left, right, parent;

            SortedSet<DoublyNode> heap = new SortedSet<DoublyNode>(
                frequencyTable.Select(entry => new DoublyNode(entry.Key, entry.Value)),
                Comparer<DoublyNode>.Create((a, b) => a.freq.CompareTo(b.freq))
            );
            while (heap.Count > 1)
            {
                left = heap.First();
                heap.Remove(left);
                right = heap.First();
                heap.Remove(right);
                parent = new DoublyNode('_', left.freq + right.freq);
                parent.left = left;
                parent.right = right;
                heap.Add(parent);
            }
            Dictionary<string, string> codes = new Dictionary<string, string>();
            GetCodes(codes, heap.First(), "");
            return codes;
        }

        public void GetCodes(Dictionary<string, string> codes, DoublyNode root, string str)
        {
            if (root == null)
                return;
            if (root.data != '_')
                codes.Add(root.data.ToString(), str);
            GetCodes(codes, root.left, str + "0");
            GetCodes(codes, root.right, str + "1");
        }
    }
}
