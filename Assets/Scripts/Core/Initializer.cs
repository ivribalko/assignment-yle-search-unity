using GUI;
using GUI.Search;
using UnityEngine;
using Yle;

namespace Core
{
    public class Initializer : MonoBehaviour
    {
        void Start()
        {
            IoCRegistrar.Run();

            var guiManager = IoC.Get<IManager>();
            var requestBuilder = IoC.Get<IRequestBuilder>();

            requestBuilder.limitResults = Settings.instance.LimitRequestResults;

            var view = guiManager.OpenWindow<SearchWindow>();
            new SearchPresenter(guiManager, requestBuilder, view, IoC.Get<ISearchModel>());
        }
    }
}