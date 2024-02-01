using System.Collections;
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

    private static void Main(string[] args)
    {
        //9. Design HashMap: Design a HashMap without using any built-in hash table libraries.
        IHashTable<int,char> hashTable = new HashTable<int,char>(100);
        hashTable.ToString();

        //1. Unique Characters: Determine if a string has all unique characters without using additional data structures.
        string text = "asdfag";
       Console.WriteLine(caractereUnice(hashTable,text));


    }
}