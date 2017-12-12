using UnityEngine;

public class Settings : ScriptableObject
{
    #region - State
    static Settings s_Instance;
    #endregion

    #region - Public
    public bool Logging = true;
    public int LimitRequestResults = 10;
    public int NetworkDelaySeconds;

    public static bool log { get { return instance.Logging; } }

    public static Settings instance {
        get {
            if (s_Instance == null) {
                s_Instance = Resources.Load<Settings>("Settings");
            }

            return s_Instance;
        }
    }
    #endregion
}
