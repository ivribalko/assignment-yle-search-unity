using UnityEngine;

namespace Auxiliary
{
    public static class Extensions
    {
        public static string Localised(this Yle.Languages languages)
        {
            if (!string.IsNullOrEmpty(languages.fi)) {
                return languages.fi;
            } else if (!string.IsNullOrEmpty(languages.sv)) {
                return "(sv) " + languages.sv;
            } else if (!string.IsNullOrEmpty(languages.se)) {
                return "(se) " + languages.se;
            }

            return null;
        }

        public static void DestroyChildren(this Transform transform)
        {
            foreach (Transform item in transform) {
                item.gameObject.SetActive(false);
                Object.Destroy(item.gameObject);
            }
        }

        public static string ProveNotEmpty(this string target)
        {
            return string.IsNullOrEmpty(target) ? Constants.NoData : target;
        }
    }
}