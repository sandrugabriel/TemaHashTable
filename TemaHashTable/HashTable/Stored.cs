using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaHashTable.HashTable
{
    public class Stored<K, V> : IComparable<Stored<K, V>> where K : IComparable<K>
    {

        public K Key { get; set; }
        public V Value { get; set; }

        public Stored(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(Stored<K, V>? other)
        {
            if (other == null) return 1;

            return Key.CompareTo(other.Key);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Stored<K, V> other)
            {
                return EqualityComparer<K>.Default.Equals(Key, other.Key) &&
                    EqualityComparer<V>.Default.Equals(Value, other.Value);

            }


            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Value);
        }

    }
}
