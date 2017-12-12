using System.Collections.Generic;

using UnityEngine;

namespace GUI
{
    public interface IManager
    {
        T OpenWindow<T>() where T : class, IView;

        void OpenPrevious();
    }

    public class Manager : IManager
    {
        #region - State
        readonly Stack<IView> m_WindowStack = new Stack<IView>();
        #endregion

        #region - Public
        public T OpenWindow<T>() where T : class, IView
        {
            var window = LoadWindow<T>();

            if (m_WindowStack.Count > 0) {
                var current = m_WindowStack.Peek();
                current.Hide();
            }

            m_WindowStack.Push(window);

            window.Show();

            return window;
        }

        public void OpenPrevious()
        {
            if (!OpenPreviousWindow()) {
                if (Settings.log) Debug.LogWarning("no way back");
            }
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

        bool OpenPreviousWindow()
        {
            if (m_WindowStack.Count < 2) {
                return false;
            }

            var current = m_WindowStack.Pop();
            var previous = m_WindowStack.Peek();

            current.Close();
            previous.Show();

            return true;
        }
        #endregion
    }
}