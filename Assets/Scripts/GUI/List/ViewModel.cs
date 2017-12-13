using System;
using System.Collections;
using System.Collections.Generic;

namespace GUI.List
{
    public interface IViewModel
    {
        event Action onChanged;

        int Count { get; }

        object Get(int index);
    }

    public class ViewModel<T> : IViewModel
    {
        #region - State
        readonly List<T> m_Collection = new List<T>();
        #endregion

        #region - Public
        public event Action onChanged;

        public int Count { get { return m_Collection.Count; } }

        public void Clear()
        {
            m_Collection.Clear();

            if (onChanged != null) {
                onChanged();
            }
        }

        public void Append(IEnumerable<T> collection)
        {
            m_Collection.AddRange(collection);

            if (onChanged != null) {
                onChanged();
            }
        }

        public object Get(int index)
        {
            return m_Collection[index];
        }

        public T Find(Predicate<T> match)
        {
            return m_Collection.Find(match);
        }
        #endregion
    }
}