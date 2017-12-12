using System;
using System.Collections;

using Auxiliary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GUI.List
{
    public class ListView<T> : MonoBehaviour where T : MonoBehaviour, IListItem
    {
        #region - LifeCycle
        void Start()
        {
            m_ItemPrefab.gameObject.SetActive(false);
            m_Pool = new Pool<T>(m_ItemPrefab as T);
        }

        void Update()
        {
            // An ugly workaround for ScrollRect not properly resizing the viewport
            // Am probably looking for a better solution
            var scrollBarOn = m_ScrollRect.verticalScrollbar.gameObject.activeSelf;

            if (m_ScrollBarOn != scrollBarOn) {
                m_ScrollBarOn = scrollBarOn;

                var viewportText = m_ScrollRect.viewport.GetComponent<Text>();
                viewportText.enabled = false;
                viewportText.enabled = true;
            }
        }
        #endregion

        #region - State
        [SerializeField] ScrollRect m_ScrollRect;
        [SerializeField] LayoutGroup m_LayoutGroup;
        [SerializeField] MonoBehaviour m_ItemPrefab;

        IViewModel m_ViewModel;
        Pool<T> m_Pool;

        bool m_ScrollBarOn;
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
                    var newItem = m_Pool.Get();
                    ShowItem(newItem, data);
                }
            }
        }

        void Clear()
        {
            m_Pool.Flush();
        }

        protected virtual void ShowItem(T item, object data)
        {
            item.Set(data);
        }
        #endregion
    }
}