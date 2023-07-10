using System.Collections.Generic;
using UnityEngine;

namespace Definitions
{
    public abstract class AbstractDefs<T> : ScriptableObject where T:class
    {
        [SerializeField] protected List<T> _items;

        public int Count => _items.Count;

        public abstract T Get(string id);

        public T Get(int index)
        {
            return _items[index];
        }

        public List<T> GetAllItems()
        {
            return new List<T>(_items);
        }
    }
}