using System;

using GUI;
using GUI.List;
using GUI.Other;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Search
{
    public interface ISearchView : IView
    {
        event Action OnSearch;
        /// Sends program ID
        event Action<string> OnDetails;
        /// The more -> the closer to start
        event Action<float> onVerticalScrollChanged;

        string searchText { get; }

        IViewModel searchViewModel { set; }

        void ToggleActivityIndicator(bool show);
    }

    public class SearchWindow : View, ISearchView
    {
        #region - LifeCycle
        void Start()
        {
            m_ClearSearchButton.onClick.AddListener(() => m_SearchInputField.text = null);

            m_SearchList.onItemClick += OnDetails;

            m_SearchInputField.onEndEdit.AddListener(_ => OnSearch());
        }
        #endregion

        #region - State
        [SerializeField] Button m_ClearSearchButton;
        [SerializeField] InputField m_SearchInputField;
        [SerializeField] SearchListView m_SearchList;
        [SerializeField] ActivityIndicator m_ActivityIndicator;
        #endregion

        #region - Public
        public event Action OnSearch;
        public event Action<string> OnDetails;

        public event Action<float> onVerticalScrollChanged {
            add { m_SearchList.OnSrollEvent += position => value(position.y); }
            remove { throw new NotSupportedException(); }
        }

        public string searchText {
            get { return m_SearchInputField.text; }
        }

        public IViewModel searchViewModel {
            set { m_SearchList.viewModel = value; }
        }

        public void ToggleActivityIndicator(bool show)
        {
            m_ActivityIndicator.gameObject.SetActive(show);
        }
        #endregion
    }
}
