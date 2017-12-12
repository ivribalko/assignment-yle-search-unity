using System;

using Json;
using Yle;

namespace Network
{
    public abstract class ServerModel
    {
        #region - LifeCycle
        protected ServerModel(IJson m_Json, IHttpClient m_HttpClient, IRequestBuilder m_RequestBuilder)
        {
            this.m_Json = m_Json;
            this.m_HttpClient = m_HttpClient;
            this.m_RequestBuilder = m_RequestBuilder;
        }
        #endregion

        #region - Dependencies
        readonly IJson m_Json;
        readonly protected IHttpClient m_HttpClient;
        readonly protected IRequestBuilder m_RequestBuilder;
        #endregion

        #region - Private
        protected void ProcessAnswer(string answerText, Action<Answer> onDone)
        {
            if (string.IsNullOrEmpty(answerText)) {
                onDone(null);
            } else {
                var parsed = m_Json.Deserialize<Answer>(answerText);
                onDone(parsed);
            }
        }
        #endregion
    }
}