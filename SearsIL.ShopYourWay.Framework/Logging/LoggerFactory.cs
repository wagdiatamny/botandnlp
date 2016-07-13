using System;

namespace SearsIL.ShopYourWay.Framework.Logging
{
	public interface ILoggerFactory
	{
		ILogger Create(Type service);
	}

	public class LoggerFactory : ILoggerFactory
	{
		private readonly ILoggingParametersProvider _loggingParametersProvider;
		private readonly ISystemEventLogger _systemEventLogger;

		public LoggerFactory(ILoggingParametersProvider loggingParametersProvider, ISystemEventLogger systemEventLogger)
		{
			_loggingParametersProvider = loggingParametersProvider;
			_systemEventLogger = systemEventLogger;
		}

		public ILogger Create(Type service)
		{
			var loggerType = typeof(Logger<>).MakeGenericType(service);

			return (ILogger)Activator.CreateInstance(loggerType, _loggingParametersProvider, _systemEventLogger);
		}
	}
}