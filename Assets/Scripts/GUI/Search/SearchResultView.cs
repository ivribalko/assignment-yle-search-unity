using Auxiliary;
using GUI.List;
using UnityEngine;
using UnityEngine.UI;
using Yle;
using System;

namespace GUI.Search
{
    public class SearchResultView : ListItem<ProgramData>
    {
        #region - LifeCycle
        void Start()
        {
            m_DetailsButton.onClick.AddListener(() => onClick(m_ProgramID));
        }
        #endregion

        #region - State
        [SerializeField] Button m_DetailsButton;
        [SerializeField] Text m_TitleText;

        string m_ProgramID;
        #endregion

        #region - Public
        public Action<string> onClick;
        #endregion

        #region - Private
        protected override void Set(ProgramData data)
        {
            m_ProgramID = data.id;

            m_TitleText.text = data.title
                .Localised()
                .ProveNotEmpty();
        }
        #endregion
    }
}
