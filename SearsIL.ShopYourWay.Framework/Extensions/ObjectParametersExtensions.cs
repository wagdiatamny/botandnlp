using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SearsIL.ShopYourWay.Framework.Extensions
{
	public static class ObjectParametersExtensions
	{
		public static ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> ParametersModelCache = new ConcurrentDictionary<Type, IEnumerable<PropertyInfo>>();

		public static IDictionary<string, object> GetParameters(this object parametersModel)
		{
			if (parametersModel == null)
				return new Dictionary<string, object>();

			var type = parametersModel.GetType();

			if (!ParametersModelCache.ContainsKey(type))
				ParametersModelCache.TryAdd(type, type.GetProperties(BindingFlags.Instance | BindingFlags.Public));

			var modelPropertiesInfo = ParametersModelCache[type];

			return modelPropertiesInfo
				.DistinctBy(x => x.Name)
				.ToDictionary(x => x.Name, x => x.GetValue(parametersModel, null));
		}
	}
}
