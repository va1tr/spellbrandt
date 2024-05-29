using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        [SerializeField]
        private List<T> items = new List<T>();

        public int Count()
        {
            return items.Count;
        }

        public void Clear()
        {
            items.Clear();
        }

        public T GetItem(int index)
        {
            return items[index];
        }

        public void AddToList(T itemToAdd)
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }

        public void RemoveFromList(T itemToRemove)
        {
            if (items.Contains(itemToRemove))
            {
                items.Remove(itemToRemove);
            }
        }
    }
}
