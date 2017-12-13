using System;
using System.Collections.Generic;

using UnityEngine;

namespace Auxiliary
{
    public class Pool<T> : IDisposable where T : MonoBehaviour
    {
        #region - LifeCycle
        public Pool(T prefab)
        {
            m_Prefab = prefab;
        }
        #endregion

        #region - State
        readonly List<T> m_Items = new List<T>();
        readonly T m_Prefab;
        int m_Current;
        #endregion

        #region - Public
        public T Get()
        {
            T result;

            if (m_Current >= m_Items.Count) {
                result = CreateNew();
            } else {
                result = m_Items[m_Current];
            }

            if (!result.gameObject.activeSelf) {
                result.gameObject.SetActive(true);
            }

            m_Current += 1;

            return result;
        }

        public void Flush(int leaveOnCount)
        {
            m_Current = 0;

            for (int i = m_Items.Count - 1; i > leaveOnCount - 1; i--) {
                m_Items[i].gameObject.SetActive(false);
            }
        }

        public void Dispose()
        {
            foreach (var item in m_Items) {
                item.gameObject.SetActive(false);
                UnityEngine.Object.Destroy(item.gameObject);
            }
        }
        #endregion

        #region - Private
        T CreateNew()
        {
            var result = UnityEngine.Object.Instantiate(
                             m_Prefab,
                             m_Prefab.transform.parent
                         );

            m_Items.Add(result);

            return result;
        }
        #endregion
    }
}