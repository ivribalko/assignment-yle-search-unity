using UnityEngine;

namespace GUI.List
{
    public interface IListItem
    {
        void Set(object data);
    }

    public abstract class ListItem<T> : MonoBehaviour, IListItem
    {
        #region - Public
        public void Set(object data)
        {
            Set((T)data);
        }
        #endregion

        #region - Private
        protected abstract void Set(T data);
        #endregion
    }
}