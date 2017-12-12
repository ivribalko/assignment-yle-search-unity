using System.Text;

namespace Yle
{
    /// Creates request strings for Yle API.
    public interface IRequestBuilder
    {
        int limitResults { get; set; }

        string SearchByTitle(string title, int offset = 0);
    }

    public class RequestBuilder : IRequestBuilder
    {
        #region - Constant
        const string APP_ID = "035f0f50";
        const string APP_KEY = "5a7b3fe14324c8d68606023e9b2f3c73";
        //        const string DECRYPT_MEDIA_KEY = "487fc3eb6ac0e5c8";
        const string CREDENTIALS_FORMAT = "?app_id={0}&app_key={1}";
        const string FILTER_FORMAT = "&q={0}";
        const string LIMIT_FORMAT = "&limit={0}";
        const string OFFSET_FORMAT = "&offset={0}";
        const string ITEMS_BASE = "https://external.api.yle.fi/v1/programs/items.json";
        #endregion

        #region - State
        readonly string m_Credentials = string.Format(CREDENTIALS_FORMAT, APP_ID, APP_KEY);
        string m_Limit;
        int m_LimitResults;
        #endregion

        #region - Public
        public int limitResults {
            get { return m_LimitResults; }
            set {
                m_LimitResults = value;
                m_Limit = value > 0
					? string.Format(LIMIT_FORMAT, value)
					: null;
            }
        }

        public string SearchByTitle(string title, int offset = 0)
        {
            return CreateRequestString(ITEMS_BASE, offset, title);
        }
        #endregion

        #region - Private
        string CreateRequestString(string root, int offset = 0, string filter = null)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(root);
            stringBuilder.Append(m_Credentials);
            stringBuilder.Append(m_Limit);

            if (!string.IsNullOrEmpty(filter)) {
                stringBuilder.AppendFormat(FILTER_FORMAT, filter);
            }

            if (offset != 0) {
                stringBuilder.AppendFormat(OFFSET_FORMAT, offset);
            }

            return stringBuilder.ToString();
        }
        #endregion
    }
}