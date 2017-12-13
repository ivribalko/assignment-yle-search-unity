using System;

using Auxiliary;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.List
{
    public abstract class ListView<T> : MonoBehaviour where T : MonoBehaviour, IListItem
    {
        #region - LifeCycle
        void Start()
        {
            m_ItemPrefab.gameObject.SetActive(false);
            m_Pool = new Pool<T>(m_ItemPrefab as T);

            m_ScrollRect.onValueChanged.AddListener(OnScrollChangeHandler);
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

            var scrollRectTransform = m_ScrollRect.transform as RectTransform;
            var scrollRectRect = scrollRectTransform.rect;

            if (m_ScrollRectRect != scrollRectRect) { 
                m_ScrollRectRect = scrollRectRect;

                UpdateLayout(updateViewport: true);
                UpdateItems(force: true);
            }
        }
        #endregion

        #region - State
        [SerializeField] ScrollRect m_ScrollRect;
        [SerializeField] RectTransform m_Content;
        [SerializeField] protected MonoBehaviour m_ItemPrefab;

        protected IViewModel m_ViewModel;
        IListViewLayout m_ListViewLayout;
        Pool<T> m_Pool;

        int m_LastStartVisibleIndex = -1;
        int m_LastEndVisibleIndex = -1;

        Rect m_ScrollRectRect;
        bool m_ScrollBarOn;
        #endregion

        #region - Public
        public IViewModel viewModel {
            set {
                if (m_ViewModel != null) {
                    throw new NotSupportedException();
                }

                m_ViewModel = value;
                m_ViewModel.onChanged += OnDataChanged;

                m_ListViewLayout = CreateLayout();
            }
        }
        #endregion

        #region - Private
        protected abstract IListViewLayout CreateLayout();

        protected virtual void ShowItem(int index, T item, object data)
        {
            item.Set(data);
        }

        void UpdateLayout(bool updateViewport)
        {
            if (updateViewport) {
                var scrollRectTransform = m_ScrollRect.transform as RectTransform;
                m_ListViewLayout.ViewportSize = scrollRectTransform.rect.size;
            }

            m_ListViewLayout.CalculateParameters();

            var height = m_ListViewLayout.CanvasSize.y;

            m_Content.sizeDelta = new Vector2(m_Content.sizeDelta.x, height);
        }

        void UpdateItems(bool force)
        {
            int startVisibleIndex, endVisibleIndex;

            m_ListViewLayout.GetVisibleRange(
                m_Content.offsetMax.y,
                out startVisibleIndex,
                out endVisibleIndex
            );

            if (!force &&
                startVisibleIndex == m_LastStartVisibleIndex &&
                endVisibleIndex == m_LastEndVisibleIndex) {
                return;
            }

            var leaveOnCount = Mathf.Max(0, endVisibleIndex - startVisibleIndex);

            m_Pool.Flush(leaveOnCount);

            Populate(startVisibleIndex, endVisibleIndex);

            m_LastStartVisibleIndex = startVisibleIndex;
            m_LastEndVisibleIndex = endVisibleIndex;
        }

        void Populate(int startVisibleIndex, int endVisibleIndex)
        {
            for (var i = startVisibleIndex; i <= endVisibleIndex; i += 1) {
                var itemData = m_ViewModel.Get(i);
                var item = m_Pool.Get();

                var itemTransform = item.transform as RectTransform;
                var itemPosition = m_ListViewLayout.GetItemPosition(i);
                itemTransform.anchoredPosition = itemPosition;

                ShowItem(i, item, itemData);
            }
        }

        void OnDataChanged()
        {
            UpdateLayout(updateViewport: false);
            UpdateItems(force: true);
        }

        void OnScrollChangeHandler(Vector2 scrollPosition)
        {
            UpdateItems(force: false);
        }
        #endregion
    }
}