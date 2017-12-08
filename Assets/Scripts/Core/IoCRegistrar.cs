using Core;
using Json;
using Network;
using Yle;

public static class IoCRegistrar
{
	public static void Run()
	{
		IoC.Register<IHttpClient, HttpClient>();
		IoC.Register<IJson, Json.Json>();
		IoC.Register<IRequestBuilder, RequestBuilder>();
		IoC.Register<IServer, Server>();
	}
}