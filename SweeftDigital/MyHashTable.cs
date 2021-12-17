using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweeftDigital
{

  public struct KeyValue<K, V>
  {
    public K Key { get; set; }
    public V Value { get; set; }
  }

  internal class MyHashTable<K, V>
  {
    private readonly int _size;
    private readonly LinkedList<KeyValue<K, V>>[] _items;

    internal MyHashTable(int size)
    {
      this._size = size;
      _items = new LinkedList<KeyValue<K, V>>[size];
    }

    protected int GetArrayPosition(K key)
    {
      int position = key.GetHashCode() % _size;
      return Math.Abs(position);
    }

    internal V Find(K key)
    {
      int position = GetArrayPosition(key);
      LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
      foreach (KeyValue<K, V> item in linkedList)
      {
        if (item.Key.Equals(key))
        {
          return item.Value;
        }
      }

      return default(V);
    }

    internal void Add(K key, V value)
    {
      int position = GetArrayPosition(key);
      LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
      KeyValue<K, V> item = new KeyValue<K, V>() { Key = key, Value = value };
      linkedList.AddLast(item);
    }

    internal void Remove(K key)
    {
      int position = GetArrayPosition(key);
      LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
      bool itemFound = false;
      KeyValue<K, V> foundItem = default(KeyValue<K, V>);
      foreach (KeyValue<K, V> item in linkedList)
      {
        if (item.Key.Equals(key))
        {
          itemFound = true;
          foundItem = item;
        }
      }

      if (itemFound)
      {
        linkedList.Remove(foundItem);
      }
    }

    protected LinkedList<KeyValue<K, V>> GetLinkedList(int position)
    {
      LinkedList<KeyValue<K, V>> linkedList = _items[position];
      if (linkedList == null)
      {
        linkedList = new LinkedList<KeyValue<K, V>>();
        _items[position] = linkedList;
      }

      return linkedList;
    }
  }
}
