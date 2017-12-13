using UnityEngine;

namespace GUI
{
    public interface IManager
    {
        T OpenWindow<T>() where T : class, IView;
    }

    public class Manager : IManager
    {
        #region - Public
        public T OpenWindow<T>() where T : class, IView
        {
            var window = LoadWindow<T>();

            window.Show();

            return window;
        }
        #endregion

        #region - Private
        T LoadWindow<T>() where T : class, IView
        {
            var windowName = typeof(T).Name;
            var prefab = Resources.Load("GUI/" + windowName);
            var window = Object.Instantiate(prefab, Auxiliary.Scene.canvas.transform);
            var gameObject = window as GameObject;

            var result = gameObject.GetComponent<T>();

            return result;
        }
        #endregion
    }
}