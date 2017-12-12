using UnityEngine;

namespace GUI
{
    public interface IView
    {
        void Show();

        void Hide();

        void Close();
    }

    public class View : MonoBehaviour, IView
    {
        #region - Public
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Close()
        {
            Hide();

            Destroy(gameObject);
        }
        #endregion
    }
}