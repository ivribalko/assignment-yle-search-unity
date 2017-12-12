using System.Collections;

using UnityEngine;

namespace Core
{
    public interface ICoroutineRunner
    {
        void StartCoroutine(IEnumerator coroutine);
    }

    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        #region - LifeCycle
        void Awake()
        {
            IoC.Replace<ICoroutineRunner>(this);
        }
        #endregion

        #region - Public
        public new void StartCoroutine(IEnumerator coroutine)
        {
            base.StartCoroutine(coroutine);
        }
        #endregion
    }
}