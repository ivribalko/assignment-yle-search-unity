using System;


namespace Yle
{
	public class Answer
	{
		public Meta meta;
		public ProgramData[] data;
	}

	/// Information about a progam on the Yle API.
	[Serializable]
	public struct ProgramData
	{
		public Languages title;
		public object[] description;
		public string[] creator;
		public string[] countryOfOrigin;
		public string collection;
		public string duration;
	}

	/// Generalized representation of available translations.
	[Serializable]
	public struct Languages
	{
		public string fi;
	}

	[Serializable]
	public struct Meta
	{
		public int offset;
		public int limit;
		public int count;
		public int program;
		public int clip;
	}
}