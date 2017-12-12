using UnityEngine;

namespace Auxiliary
{
    public static class Scene
    {
        #region - State
        static Canvas s_Canvas;
        #endregion

        #region - Public
        public static Canvas canvas {
            get {
                if (s_Canvas == null) {
                    s_Canvas = Object.FindObjectOfType<Canvas>();
                }

                return s_Canvas;
            }
        }
        #endregion
    }
}