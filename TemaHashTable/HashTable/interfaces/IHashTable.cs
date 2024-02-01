using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaHashTable.HashTable.interfaces
{
    public interface IHashTable<K, V>
    {

        void Put(K key, V value);

        V Get(K key);

        bool Remove(K key);

        string ToString();

        List<V> GetValues(int key);
    }
}
