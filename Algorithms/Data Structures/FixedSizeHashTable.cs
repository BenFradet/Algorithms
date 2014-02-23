using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public struct KeyValue<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }

    public class FixedSizeHashTable<K, V>
    {
        private int size;
        private LinkedList<KeyValue<K, V>>[] items;

        public FixedSizeHashTable(int size)
        {
            this.size = size;
            items = new LinkedList<KeyValue<K, V>>[size];
        }

        public V Search(K key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = items[position];
            foreach (KeyValue<K, V> pair in linkedList)
            {
                if (pair.Key.Equals(key))
                {
                    return pair.Value;
                }
            }
            return default(V);
        }

        public void Add(KeyValue<K, V> keyValue)
        {
            int position = GetArrayPosition(keyValue.Key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            linkedList.AddLast(keyValue);
        }

        public void Remove(K key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            bool found = false;
            KeyValue<K, V> foundItem = default(KeyValue<K, V>);
            foreach (KeyValue<K, V> keyValue in linkedList)
            {
                if (keyValue.Key.Equals(key))
                {
                    found = true;
                    foundItem = keyValue;
                }
            }
            if (found)
            {
                linkedList.Remove(foundItem);
            }
        }

        private LinkedList<KeyValue<K, V>> GetLinkedList(int position)
        {
            LinkedList<KeyValue<K, V>> linkedList = items[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<KeyValue<K, V>>();
                items[position] = linkedList;
            }
            return linkedList;
        }

        private int GetArrayPosition(K key)
        {
            int position = key.GetHashCode() % size;
            return Math.Abs(position);
        }
    }
}
