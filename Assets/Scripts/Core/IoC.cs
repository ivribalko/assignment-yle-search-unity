using System;
using System.Collections.Generic;

using UnityEngine;

namespace Core
{
	public static class IoC
	{
		#region - State
		readonly static Dictionary<Type, Type> m_Factories = new Dictionary<Type, Type>();
		readonly static Dictionary<Type, object> m_Instances = new Dictionary<Type, object>();
		#endregion

		#region - Public
		public static T Get<T>() where T : class
		{
			var interfaceType = typeof(T);

			return (T)Get(interfaceType);
		}

		public static object Get(Type interfaceType)
		{
			object result;
			if (!m_Instances.TryGetValue(interfaceType, out result)) {				
				result = Create(interfaceType);
				m_Instances.Add(interfaceType, result);
			}

			return result;
		}

		public static void Replace<T>(T instance)
		{
			var interfaceType = typeof(T);

			if (m_Instances.ContainsKey(interfaceType)) {
				m_Instances[interfaceType] = instance;
				Debug.LogWarning(interfaceType.Name + " has been replaced");
			} else {
				m_Instances.Add(interfaceType, instance);
			}
		}

		public static void Register<T, K>() where T : class where K : class
		{
			var interfaceType = typeof(T);
			var objectType = typeof(K);

			m_Factories.Add(interfaceType, objectType);
		}
		#endregion

		#region - Internal
		static object Create(Type interfaceType)
		{
			var objectType = m_Factories[interfaceType];
			var constructors = objectType.GetConstructors();
			if (constructors.Length > 1) {
				throw new ArgumentOutOfRangeException("only single constructor supported");
			}

			var constructor = constructors[0];
			var parameters = constructor.GetParameters();
			var injections = new object[parameters.Length];

			for (int i = 0; i < parameters.Length; i++) {
				var parameter = parameters[i];
				injections[i] = Get(parameter.ParameterType);
			}

			var result = Activator.CreateInstance(objectType, injections);

			return result;
		}
		#endregion
	}
}