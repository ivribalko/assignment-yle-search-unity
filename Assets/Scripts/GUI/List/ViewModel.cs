using System;
using System.Collections;
using System.Collections.Generic;

namespace GUI.List
{
    public interface IViewModel
    {
        event Action<IEnumerable> onAppended;
        event Action onCleared;

        int Count { get; }
    }

    public class ViewModel<T> : IViewModel
    {
        #region - State
        List<T> m_Collection;
        #endregion

        #region - Public
        public event Action<IEnumerable> onAppended;
        public event Action onCleared;

        public int Count { get { return m_Collection == null ? 0 : m_Collection.Count; } }

        public void Clear()
        {
            m_Collection = null;

            if (onCleared != null) {
                onCleared();
            }
        }

        public void Append(IEnumerable<T> collection)
        {
            if (collection != null) {
                if (m_Collection == null) {
                    m_Collection = new List<T>(collection);
                } else {
                    m_Collection.AddRange(collection);
                }
            }

            if (onAppended != null) {
                onAppended(collection);
            }
        }

        public T Find(Predicate<T> match)
        {
            return m_Collection.Find(match);
        }
        #endregion
    }
}