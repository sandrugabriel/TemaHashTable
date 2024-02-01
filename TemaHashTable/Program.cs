using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using TemaHashTable.HashTable;
using TemaHashTable.HashTable.interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{

    public static bool caractereUnice(IHashTable<int,char> hashTable,string text)
    {

        for (int i = 0; i < text.Length; i++)
        {
            int nr = text[i];
           hashTable.Put(nr, text[i]);
        }

        for (int i = 0; i < 256; i++){

            List<char> values = hashTable.GetValues(i);
            if (values.Count >= 2)
            {
                return false;
            }
        }

        return true;
    }

    public static int primaLiteraNeRpetabila(IHashTable<int, char> hashTable, string text)
    {

        for (int i = 0; i < text.Length; i++)
        {
            int nr = text[i];
            hashTable.Put(nr, text[i]);
        }

        for (int i = 97; i < 256; i++)
        {

            List<char> values = hashTable.GetValues(i);
            if (values.Count == 1)
            {
               // Console.WriteLine(values[0]);

                return i;
            }
        }

        return -1;    
    }

    static int GetAnagramKey(string word)
    {
        char[] chars = word.ToCharArray();
        Array.Sort(chars);
        return new string(chars).GetHashCode();
    }

    private static void Main(string[] args)
    {
        //9. Design HashMap: Design a HashMap without using any built-in hash table libraries.
        IHashTable<int,char> hashTable = new HashTable<int,char>(100);
        hashTable.ToString();


        //1. Unique Characters: Determine if a string has all unique characters without using additional data structures.
        Console.WriteLine("Problema 1:");
        string text = "asdfag";
        Console.WriteLine(caractereUnice(hashTable,text));


        //2. Group Anagrams: Given an array of strings, group anagrams together.
        Console.WriteLine("\n\nProblema 2:");
        string[] words = { "listen", "silent", "enlist", "eat", "tea", "ate" };
        IHashTable<int, string> anagramGroups = new HashTable<int, string>(100);
        foreach (string word in words)
        {
            int key = GetAnagramKey(word);
            if (!anagramGroups.GetValues(key).Contains(word))
            {
                anagramGroups.Put(key, word);
            }
        }
        Console.WriteLine("Grupurile de anagrame:");
        for (int i = 97; i < 256; i++)
        {
            List<string> anagrams = anagramGroups.GetValues(i);
            if (anagrams.Count > 1)
            {
                Console.WriteLine(string.Join(", ", anagrams));
            }
        }


        //3. First Non-Repeating Character: Find the first non-repeating character in a string and return its index.If it doesn't exist, return -1.
        Console.WriteLine("\n\nProblema 3:");
        IHashTable<int, char> hashTable1 = new HashTable<int, char>(100);
        string text1 = "abab";
        Console.WriteLine("Pozitia primei litere care apare doar odata este "+primaLiteraNeRpetabila(hashTable1,text1));


        //4. Two Sum: Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target
        Console.WriteLine("\n\nProblema 4:");
        EqualsTarget();


        //5. Longest Consecutive Sequence: Given an unsorted array of integers nums, return the length of the longest consecutive elements sequence.
        Console.WriteLine("\n\nProblema 5:");
        int[] nums1 = { 100, 1, 2, 3, 4, 200 };
        Console.WriteLine("Lungimea celei mai lungi secvente consecutive: " + secventaMaxi(nums1));


        //6. Subarray Sum Equals K: Given an array of integers and an integer k, find the total number of continuous subarrays whose sum equals to k
        Console.WriteLine("\n\nProblema 6:");
        int[] nums2 = { 1, 2, 3, 4, 5 };
        int k = 9;
        Console.WriteLine("Numarul total de subarray-uri cu suma " + k + ": " + SubarraySumEqualsK(nums2, k));

        //7. Longest Substring Without Repeating Characters: Given a string s, find the length of the longest substring without repeating characters
        Console.WriteLine("\n\nProblema 7:");
        string s = "abcabcbb";
        Console.WriteLine("Lungimea celui mai lung subsir fara caractere repetate: " + secventaLunga(s));


        //8. Copy List with Random Pointer: A linked list is given where each node contains an additional random pointer which could point to any node in the list or null.Return a deep copy of the list.
        Console.WriteLine("\n\nProblema 8:");
        Node head = new Node(1);
        head.next = new Node(2);
        head.next.next = new Node(3);
        head.random = head.next.next;
        head.next.random = head;
        Node deepCopy = CopyRandomList(head);
        Console.WriteLine("Lista originala:");
        PrintList(head);
        Console.WriteLine("\nCopia adanca:");
        PrintList(deepCopy);




    }

    static int secventaLunga(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        int maxLength = 0;
        int left = 0;
        IHashTable<int, char> charIndexTable = new HashTable<int, char>(100);

        for (int right = 0; right < s.Length; right++)
        {
            char currentChar = s[right];

            if (charIndexTable.ContainsKey(currentChar) && charIndexTable.Get(currentChar) >= left)
            {
                left = charIndexTable.Get(currentChar) + 1;
            }

            charIndexTable.Put(right, currentChar);

            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }

    static int SubarraySumEqualsK(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        int count = 0;
        int sum = 0;

        IHashTable<int, int> sumCountTable = new HashTable<int, int>(100);
        sumCountTable.Put(0, 1);

        for (int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];

            if (sumCountTable.ContainsKey(sum - k))
            {
                count += sumCountTable.Get(sum - k);
            }

            sumCountTable.Put(sum, sumCountTable.ContainsKey(sum) ? sumCountTable.Get(sum) + 1 : 1);
        }

        return count;
    }

    static void EqualsTarget()
    {

        int[] nums = { 2, 7, 11, 15 };
        int target = 9;
        IHashTable<int, int> numTable = new HashTable<int, int>(100);
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            if (numTable.TryGetValue(complement, out int complementIndex))
            {
                Console.WriteLine($"Indicii care aduna la target: {complementIndex} si {i}");
                return;
            }

            numTable.Put(nums[i], i);
        }
        Console.WriteLine("Nu s-au găsit indici care adună la target.");
    }

    static int secventaMaxi(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        IHashTable<int, bool> numTable = new HashTable<int, bool>(100);

        foreach (int num in nums)
        {
            numTable.Put(num, true);
        }

        int maxLength = 0;

        foreach (int num in nums)
        {
            if (numTable.ContainsKey(num - 1))
            {
                continue;
            }

            int currentNum = num;
            int currentLength = 1;

            while (numTable.ContainsKey(currentNum + 1))
            {
                currentNum++;
                currentLength++;
            }

            maxLength = Math.Max(maxLength, currentLength);
        }

        return maxLength;
    }

    public class Node : IComparable<Node>
    {
        public int val;
        public Node next;
        public Node random;

        public Node(int val)
        {
            this.val = val;
            this.next = null;
            this.random = null;
        }

        public override string? ToString()
        {
            return (val).ToString();
        }

        int IComparable<Node>.CompareTo(Node? other)
        {
            return ((IComparable<Node>)next).CompareTo(other);
        }

    }
    static Node CopyRandomList(Node head)
    {
        if (head == null)
        {
            return null;
        }

        IHashTable<Node, Node> nodeMapping = new HashTable<Node, Node>(1000);

        Node current = head;
        while (current != null)
        {
            nodeMapping.Put(current, new Node(current.val));
            current = current.next;
        }

        current = head;
        while (current != null)
        {
            Node newNode = nodeMapping.Get(current);

            if (current.next != null)
            {
                newNode.next = nodeMapping.ContainsKey(current.next) ? nodeMapping.Get(current.next) : null;
            }

            if (current.random != null)
            {
                newNode.random = nodeMapping.ContainsKey(current.random) ? nodeMapping.Get(current.random) : null;
            }

            current = current.next;
        }

        return nodeMapping.Get(head);
    }
    static void PrintList(Node head)
    {
        Node current = head;
        while (current != null)
        {
            Console.Write($"{current.val} (Random: {(current.random != null ? current.random.val.ToString() : "null")}) ");
            current = current.next;
        }
        Console.WriteLine();
    }
}