using GUI.List;
using System;

namespace GUI.Search
{
    public class SearchListView : ListView<SearchResultView>
    {
        #region - Public
        public event Action<string> onItemClick;
        #endregion

        #region - Private
        protected override void ShowItem(SearchResultView item, object data)
        {
            base.ShowItem(item, data);

            item.onClick = onItemClick;
        }
        #endregion
    }
}