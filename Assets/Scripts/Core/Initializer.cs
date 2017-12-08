using UnityEngine;
using Yle;
using Auxiliary;

namespace Core
{
	//TODO diable logs

	public class Initializer : MonoBehaviour
	{
		void Start()
		{
			IoCRegistrar.Run();

			var server = IoC.Get<IServer>();
			var requestBuilder = IoC.Get<IRequestBuilder>();

			requestBuilder.LimitResults = 2;

			server.FindProgramsByTitle("qwe", log);
		}

		void log(Answer answer)
		{
			if (answer == null) {
				return;
			}

			if (answer.data != null) {
				UnityEngine.Debug.LogError(answer.data[0].title.Localised());
			}

			UnityEngine.Debug.LogError(answer.meta.count);
		}
	}
}