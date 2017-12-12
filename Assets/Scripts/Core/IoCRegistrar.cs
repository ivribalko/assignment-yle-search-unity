using Core;
using Json;
using GUI;
using GUI.Search;
using Network;
using Yle;

public static class IoCRegistrar
{
    public static void Run()
    {
        IoC.Register<IHttpClient, HttpClient>();
        IoC.Register<IJson, Json.Json>();
        IoC.Register<IManager, Manager>();
        IoC.Register<IRequestBuilder, RequestBuilder>();
        IoC.Register<ISearchModel, SearchModel>();
    }
}