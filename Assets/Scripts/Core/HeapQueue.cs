using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Core
{
    public class HeapQueue<T> where T : IComparable<T>
    {
        List<T> items;

        public HeapQueue()
        {
            items = new List<T>();
        }

        public int Count { get { return items.Count; } }
        public void Clear() => items.Clear();
        public T Peek() => items[0];
        public void Push(T item)
        {
            items.Add(item);
            SiftDown(0, items.Count - 1);
        }
        public T Pop()
        {
            T item;
            var last = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            if (items.Count > 0)
            {
                item = items[0];
                items[0] = last;
                SiftUp();
            }
            else
            {
                item = last;
            }
            return item;
        }

        int Compare(T A, T B) => A.CompareTo(B);
        void SiftDown(int startpos, int pos)
        {
            var newitem = items[pos];
            while (pos > startpos)
            {
                var parentpos = (pos - 1) >> 1;
                var parent = items[parentpos];
                if (Compare(parent, newitem) <= 0)
                    break;
                items[pos] = parent;
                pos = parentpos;

            }
            items[pos] = newitem;
        }
        void SiftUp()
        {
            var endpos = items.Count;
            var startpos = 0;
            var newitem = items[0];
            var childpos = 1;
            var pos = 0;

            while (childpos < endpos)
            {
                var rightpos = childpos + 1;
                if (rightpos < endpos && Compare(items[rightpos], items[childpos]) <= 0)
                    childpos = rightpos;

                items[pos] = items[childpos];
                pos = childpos;
                childpos = 2 * pos + 1;
            }
            items[pos] = newitem;
            SiftDown(startpos, pos);
        }
    }
}
