using Auxiliary;
using GUI.List;
using UnityEngine;
using UnityEngine.UI;
using Yle;

namespace GUI.Search
{
    public class SearchResultView : ListItem<ProgramData>
    {
        #region - LifeCycle
        void Start()
        {
            m_DetailsButton.onClick.AddListener(() => {
                var listener = GetComponentInParent<IListItemClickListener>();
                listener.OnListItemClick(m_ProgramID);
            });
        }
        #endregion

        #region - State
        [SerializeField] Button m_DetailsButton;
        [SerializeField] Text m_TitleText;

        string m_ProgramID;
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
