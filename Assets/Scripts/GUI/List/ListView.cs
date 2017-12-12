using System;
using System.Collections;

using Auxiliary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GUI.List
{
    public class ListView : MonoBehaviour
    {
        #region - LifeCycle
        void Awake()
        {
            m_ItemPrefab.gameObject.SetActive(false);
            /// Avoid destroying
            m_ItemPrefab.transform.SetParent(transform);
        }
        #endregion

        #region - State
        [SerializeField] ScrollRect m_ScrollRect;
        [SerializeField] LayoutGroup m_LayoutGroup;
        [SerializeField] MonoBehaviour m_ItemPrefab;

        IViewModel m_ViewModel;
        #endregion

        #region - Public
        public event UnityAction<Vector2> OnSrollEvent {
            add { m_ScrollRect.onValueChanged.AddListener(value); }
            remove { m_ScrollRect.onValueChanged.RemoveListener(value); }
        }

        public IViewModel viewModel {
            set {
                if (m_ViewModel != null) {
                    m_ViewModel.onAppended -= Append;
                    m_ViewModel.onCleared -= Clear;
                }

                m_ViewModel = value;

                m_ViewModel.onAppended += Append;
                m_ViewModel.onCleared += Clear;
            }
        }
        #endregion

        #region - Private
        void Append(IEnumerable collection)
        {
            if (collection != null) {
                foreach (var data in collection) {
                    ShowItem(data);
                }
            }
        }

        void Clear()
        {
            m_LayoutGroup.transform.DestroyChildren();
        }

        void ShowItem(object data)
        {
            var newItem = Instantiate(m_ItemPrefab);
            newItem.GetComponent<IListItem>().Set(data);
            newItem.transform.SetParent(m_LayoutGroup.transform);
            newItem.transform.localScale = m_ItemPrefab.transform.localScale;

            newItem.gameObject.SetActive(true);
        }
        #endregion
    }
}