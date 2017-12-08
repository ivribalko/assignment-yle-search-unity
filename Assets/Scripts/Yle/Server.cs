using System;
using Network;
using Json;

namespace Yle
{
	/// Provides ways to exchange information via Yle API.
	public interface IServer
	{
		void FindProgramsByTitle(string title, Action<Answer> onDone);
	}

	public class Server : IServer
	{
		#region - LifeCycle
		public Server(IHttpClient m_HttpClient, IJson m_Json, IRequestBuilder m_RequestBuilder)
		{
			this.m_HttpClient = m_HttpClient;
			this.m_Json = m_Json;
			this.m_RequestBuilder = m_RequestBuilder;
		}
		#endregion

		#region - Dependencies
		readonly IHttpClient m_HttpClient;
		readonly IJson m_Json;
		readonly IRequestBuilder m_RequestBuilder;
		#endregion

		#region - Public
		public void FindProgramsByTitle(string title, Action<Answer> onDone)
		{
			var requestString = m_RequestBuilder.SearchByTitle(title);

			m_HttpClient.GetText(requestString, answerText => ProcessAnswer(answerText, onDone));
		}
		#endregion

		#region - Private
		void ProcessAnswer(string answerText, Action<Answer> onDone)
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