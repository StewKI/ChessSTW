using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public class ThreadSafeList<T> : IEnumerable<T>
    {
        private readonly List<T> internalList = new List<T>();
        private readonly object lockObject = new object();


        public void Add(T item)
        {
            lock (lockObject)
            {
                internalList.Add(item);
            }
        }

        public bool Remove(T item)
        {
            lock (lockObject)
            {
                return internalList.Remove(item);
            }
        }

        public List<T> ToList()
        {
            lock (lockObject)
            {
                return new List<T>(internalList);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (lockObject)
            {
                return new List<T>(internalList).GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
