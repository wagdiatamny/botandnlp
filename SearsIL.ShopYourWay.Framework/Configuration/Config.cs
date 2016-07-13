using System;
using System.Configuration;

namespace SearsIL.ShopYourWay.Framework.Configuration
{
	public static class Config
	{
		public static long GetLong(string key)
		{
			var val = GetString(key);

			long result;
			if (!long.TryParse(val, out result))
				throw new ConfigurationErrorsException(string.Format("Value of key '{0}' was expected to be long but wasn't: {1}", key, val));

			return result;
		}

		public static int GetInt(string key)
		{
			var val = GetString(key);

			int result;
			if (!int.TryParse(val, out result))
				throw new ConfigurationErrorsException(string.Format("Value of key '{0}' was expected to be integer but wasn't: {1}", key, val));

			return result;
		}

		public static decimal GetDecimal(string key)
		{
			var val = GetString(key);

			decimal result;
			if (!decimal.TryParse(val, out result))
				throw new ConfigurationErrorsException($"Value of key '{key}' was expected to be decimal but wasn't: {val}");

			return result;
		}

		public static string GetString(string key)
		{
			var val = GetStringOrDefault(key);

			if (string.IsNullOrEmpty(val))
				throw new ConfigurationErrorsException($"Key '{key}' is missing from appSettings in the configuration. Add it to your config file");

			return val;
		}
		public static string GetStringOrDefault(string key)
		{
			var val = ConfigurationManager.AppSettings[key];
			return val ?? "";
		}

		public static Uri GetUri(string key)
		{
			var val = GetString(key);

			Uri result;

			if (!Uri.TryCreate(val, UriKind.Absolute, out result))
				throw new ConfigurationErrorsException($"Value of key '{key}' is not a valid URI: '{val}'");

			return result;
		}

		public static bool GetBoolean(string key)
		{
			var val = ConfigurationManager.AppSettings[key];

			if (string.IsNullOrEmpty(val))
				return false;

			bool result;
			if (!bool.TryParse(val, out result))
				throw new ConfigurationErrorsException($"Value of key '{key}' was expected to be Boolean but wasn't: {val}");

			return result;
		}
	}
}
