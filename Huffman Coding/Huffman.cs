using System;
using System.Collections.Generic;
using System.Linq;

namespace Huffman_Coding
{
    internal class DoublyNode
    {
        public int data;
        public DoublyNode left;
        public DoublyNode right;
        public DoublyNode parent;

        public DoublyNode() { }
        public DoublyNode(int data)
        { this.data = data; }
        public DoublyNode(int data, DoublyNode left, DoublyNode right, DoublyNode parent)
        { this.data = data; this.left = left; this.right = right; this.parent = parent; }
        public DoublyNode copy()
        { return new DoublyNode(data, left, right, parent); }
    }

    internal class Huffman
    {
        static void Main(string[] args)
        {
            Huffman obj = new Huffman();
            string s = "MUHAMMADBINAHMEDABUREHANALBERUNI";
            s = "GCTATCAGGCT.GGTTAC.GTAGC";
            Dictionary<char, int> dictionary = obj.GetFrequencyDistributionTable(s);
            foreach (char c in dictionary.Keys)
            {
                Console.WriteLine($"{c}: {dictionary[c]}");
            }
            obj.GetHuffmanCodes(s);
            Console.ReadKey();
        }

        public Dictionary<Char, int> GetFrequencyDistributionTable(string s)
        {
            Dictionary<Char, int> dictionary = new Dictionary<char, int>();
            foreach (char c in s)
            {
                if (dictionary.ContainsKey(c))
                {
                    dictionary[c]++;
                }
                else
                {
                    dictionary.Add(c, 1);
                }
            }
            var sortedDictionary = dictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return sortedDictionary;
        }

        public DoublyNode GetMinParentNode(LinkedList<DoublyNode> list)
        {
            DoublyNode min = list.Last();
            foreach(DoublyNode node in list)
            {
                if (node.parent == null && node.data < min.data)
                {
                    min = node;
                }
            }
            return min;
        }

        public int CountRootNodes(LinkedList<DoublyNode> list)
        {
            int roots = 0;
            foreach(DoublyNode node in list)
            {
                if (node.parent == null)
                {
                    roots++;
                }
            }
            return roots;
        }

        public Dictionary<string, string> GetHuffmanCodes(string s)
        {
            Dictionary<Char, int> frequencyTable = GetFrequencyDistributionTable(s);
            LinkedList<DoublyNode> list = new LinkedList<DoublyNode>();
            Dictionary<char, int>.Enumerator enumerator = frequencyTable.GetEnumerator();
            // Add first node
            enumerator.MoveNext();
            list.AddLast(new DoublyNode(enumerator.Current.Value));

            int i = 0;
            while (enumerator.MoveNext())
            {
                list.AddLast(new DoublyNode(enumerator.Current.Value));
            }
            int roots = list.Count;
            while (roots > 1)
            {
                // Get first min
                DoublyNode min = GetMinParentNode(list);
                DoublyNode minCopy = min.copy();
                list.Remove(min);
                // Get second min
                min = GetMinParentNode(list);
                DoublyNode min2Copy = min.copy();
                list.Remove(min);
                // Create a parent and insert all 3 to list
                DoublyNode parent = new DoublyNode();
                parent.data = minCopy.data + min2Copy.data;
                parent.left = minCopy; parent.right = min2Copy;
                minCopy.parent = min2Copy.parent = parent;
                list.AddLast(minCopy);
                list.AddLast(min2Copy);
                list.AddLast(parent);
                roots = CountRootNodes(list);
            }

            Dictionary<string, string> codes = new Dictionary<string, string>();
            // Parse from root to each leaf node and collect codes
            enumerator = frequencyTable.GetEnumerator();
            while (enumerator.MoveNext())
            {

            }

            //foreach (char c in s)
            //{
            //    string binaryString = Convert.ToString((int)c, 2).PadLeft(8, '0');
            //    codes.Add(binaryString, "");
            //}
            return codes;
        }
    }
}
