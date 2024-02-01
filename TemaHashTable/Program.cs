using System;
using System.Collections;
using System.Linq;
using TemaHashTable.HashTable;
using TemaHashTable.HashTable.interfaces;

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



    }
}