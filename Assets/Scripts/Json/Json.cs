using UnityEngine;

namespace Json
{
    public interface IJson
    {
        T Deserialize<T>(string text) where T : class;
    }

    public class Json : IJson
    {
        #region - Public
        public T Deserialize<T>(string text) where T : class
        {
            return JsonUtility.FromJson<T>(text);
        }
        #endregion
    }
}