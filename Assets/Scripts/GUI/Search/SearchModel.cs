using System;

using Json;
using Network;
using Yle;

namespace GUI.Search
{
    public interface ISearchModel
    {
        void FindProgramsByTitle(string title, int offset, Action<Answer> onDone);
    }

    public class SearchModel : ServerModel, ISearchModel
    {
        #region - LifeCycle
        public SearchModel(IJson m_Json, IHttpClient m_HttpClient, IRequestBuilder m_RequestBuilder)
            : base(m_Json, m_HttpClient, m_RequestBuilder)
        {
        }
        #endregion

        #region - Public
        public void FindProgramsByTitle(string title, int offset, Action<Answer> onDone)
        {
            if (Settings.log) UnityEngine.Debug.Assert(!string.IsNullOrEmpty(title));

            var requestString = m_RequestBuilder.SearchByTitle(title, offset);

            m_HttpClient.GetText(requestString, answerText => ProcessAnswer(answerText, onDone));
        }
        #endregion
    }
}
