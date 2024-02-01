using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TemaHashTable.HashTable.interfaces;

namespace TemaHashTable.HashTable
{
    public class HashTable<K, V> : IHashTable<K, V> where K : IComparable<K>
    {

        private readonly List<Stored<K, V>>[] hashtable;
        private List<string> cuvinte;
        private List<string> cuvinteGresite;


        public HashTable(int size)
        {

            hashtable = new List<Stored<K, V>>[size];

            for (int i = 0; i < size; i++)
            {
                hashtable[i] = new List<Stored<K, V>>();
            }
        }

        private int HashKey(K key)
        {
            return int.Parse(key.ToString()) % hashtable.Length;
        }

        public void Put(K key, V value)
        {

            int hashkey = HashKey(key);
            var entry = new Stored<K, V>(key, value);

            hashtable[hashkey].Add(entry);
        }

        public V Get(K key)
        {

            int hashedKey = HashKey(key);
            foreach (var entry in hashtable[hashedKey])
            {
                if (entry.Key.Equals(key))
                {
                    return entry.Value;
                }
            }
            return default(V);
        }

        public bool Remove(K key)
        {
            int hashedKey = HashKey(key);
            var bucket = hashtable[hashedKey];
            var itemToRemove = bucket.Find(entry => entry.Key.Equals(key));

            if (itemToRemove != null)
            {
                return bucket.Remove(itemToRemove);
            }

            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < hashtable.Length; i++)
            {
                sb.AppendLine($"{i}:");
                foreach (var entry in hashtable[i])
                {
                    sb.AppendLine($"-->{entry.Value}");
                }
            }
            return sb.ToString();
        }
    }
}
