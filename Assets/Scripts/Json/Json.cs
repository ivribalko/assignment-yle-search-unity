using UnityEngine;

namespace Json
{
	public interface IJson
	{
		T Deserialize<T>(string text) where T : class;

		string Serialize(object target);
	}

	//TODO tell i know about other
	public class Json : IJson
	{
		public T Deserialize<T>(string text) where T : class
		{
			return JsonUtility.FromJson<T>(text);
		}

		public string Serialize(object target)
		{
			return JsonUtility.ToJson(target);
		}
	}
}