using System;
using System.Text;

namespace Yle
{
	/// Creates request strings for Yle API.
	public interface IRequestBuilder
	{
		int? LimitResults { set; }

		string SearchByTitle(string title);
	}

	public class RequestBuilder : IRequestBuilder
	{
		#region - Constant
		const string APP_ID = "035f0f50";
		const string APP_KEY =	"5a7b3fe14324c8d68606023e9b2f3c73";
		const string CREDENTIALS_FORMAT = "?app_id={0}&app_key={1}";
		const string FILTER_FORMAT = "&q={0}";
		const string LIMIT_FORMAT = "&limit={0}";
		const string ITEMS_BASE = "https://external.api.yle.fi/v1/programs/items.json";
		#endregion

		#region - State
		readonly string m_Credentials = string.Format(CREDENTIALS_FORMAT, APP_ID, APP_KEY);
		string m_Limit;
		#endregion

		#region - Public
		public int? LimitResults {
			set {
				m_Limit = value > 0
					? string.Format(LIMIT_FORMAT, value)
					: null;
			}
		}

		public string SearchByTitle(string title)
		{
			return CreateRequestString(ITEMS_BASE, title);
		}
		#endregion

		#region - Private
		string CreateRequestString(string root, string filter = null)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append(root);
			stringBuilder.Append(m_Credentials);
			stringBuilder.Append(m_Limit);

			if (!string.IsNullOrEmpty(filter)) {
				var filterArgument = string.Format(FILTER_FORMAT, filter);
				stringBuilder.Append(filterArgument);
			}

			return stringBuilder.ToString();
		}
		#endregion
	}
}