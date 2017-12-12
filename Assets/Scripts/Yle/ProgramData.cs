using System;

namespace Yle
{
    public class Answer
    {
        public Meta meta;
        public ProgramData[] data;
        public bool isDone = false;
    }

    /// Information about a progam on the Yle API.
    [Serializable]
    public struct ProgramData
    {
        public Languages title;
        public string id;
        public Languages description;
        public Creator[] creator;
        public string[] countryOfOrigin;
        public string type;
        public string indexDataModified;
    }

    /// Generalized representation of available translations.
    [Serializable]
    public struct Languages
    {
        public string fi;
        public string sv;
        public string se;
    }

    [Serializable]
    public struct Creator
    {
        public string name;
        public string type;
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