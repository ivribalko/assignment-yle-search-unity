using System;

using GUI.List;
using UnityEngine;

namespace GUI.Search
{
    public class SearchListView : ListView<SearchResultView>
    {
        #region - Constants
        // if showing one of last N items
        const int ReachingEndCount = 2;
        #endregion

        #region - State
        [Header("Layout")]
        [SerializeField] ListViewVerticalAdaptableLayout.Settings m_LayoutSettings;
        #endregion

        #region - Public
        public event Action<string> onItemClick;
        public event Action onReachingEnd;
        #endregion

        #region - Private
        protected override void ShowItem(int index, SearchResultView item, object data)
        {
            base.ShowItem(index, item, data);

            item.onClick = onItemClick;

            if (index >= m_ViewModel.Count - ReachingEndCount) {
                onReachingEnd();
            }
        }

        protected override IListViewLayout CreateLayout()
        {
            return new ListViewVerticalAdaptableLayout(
                m_ViewModel,
                m_ItemPrefab,
                m_LayoutSettings
            );
        }
        #endregion
    }
}