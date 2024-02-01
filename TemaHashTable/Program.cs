using TemaHashTable.HashTable;
using TemaHashTable.HashTable.interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        //9. Design HashMap: Design a HashMap without using any built-in hash table libraries.
        IHashTable<string,string> hashTable = new HashTable<string,string>(100);
        hashTable.ToString();
    }
}