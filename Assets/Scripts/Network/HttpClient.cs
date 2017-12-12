using System;
using System.Collections;

using Core;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public interface IHttpClient
    {
        void GetText(string url, Action<string> onDone);
    }

    public class HttpClient : IHttpClient
    {
        #region - LifeCycle
        public HttpClient(ICoroutineRunner m_CoroutineRunner)
        {
            this.m_CoroutineRunner = m_CoroutineRunner;
        }
        #endregion

        #region - Dependencies
        readonly ICoroutineRunner m_CoroutineRunner;
        #endregion

        #region - Public
        public void GetText(string url, Action<string> onDone)
        {
            m_CoroutineRunner.StartCoroutine(GetTextCoroutine(url, onDone));
        }
        #endregion

        #region - Private
        IEnumerator GetTextCoroutine(string url, Action<string> onDone)
        {
            var delay = Settings.instance.NetworkDelaySeconds; //TODO move under #if
            if (delay > 0) {
                yield return new WaitForSeconds(delay);
            }

            string result = null;

            using (var webRequest = UnityWebRequest.Get(url)) {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError) {
                    if (Settings.log) Debug.LogError(webRequest.error);
                } else {
                    result = webRequest.downloadHandler.text;
                    if (Settings.log) Debug.Log(string.Format("Request:\n{0}\n\nAnswer:\n{1}", url, result));
                }
            }

            onDone(result);
        }
        #endregion
    }
}

