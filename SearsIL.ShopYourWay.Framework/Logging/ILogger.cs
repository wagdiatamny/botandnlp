using System;
using System.Collections.Generic;
using NLog;
using SearsIL.ShopYourWay.Framework.IoC;

namespace SearsIL.ShopYourWay.Framework.Logging
{
	[ContextedImplementation]
	public interface ILogger
	{
		void Error(string message, Exception ex = null, IList<LoggingParameter> parameters = null);
		void Error(Exception e, IList<LoggingParameter> parameters = null);
		void Fatal(string message, Exception e);
		void Fatal(Exception e);
		void Device(string message, long deviceId, string stacktrace);
		void Info(string message, IList<LoggingParameter> parameters = null);
		void Info(Exception exception);
		void Warn(Exception e);
		void Warn(string message, Exception ex = null, IList<LoggingParameter> parameters = null);
		void Debug(Exception exception);
		void Debug(string message, Exception exception = null);
	}

	public class Logger<T> : ILogger
	{
		private readonly ILoggingParametersProvider _loggingParametersProvider;
		private readonly ISystemEventLogger _systemEventLogger;
		private readonly string _loggerName;

		public Logger(ILoggingParametersProvider loggingParametersProvider, ISystemEventLogger systemEventLogger)
		{
			_loggingParametersProvider = loggingParametersProvider;
			_systemEventLogger = systemEventLogger;
			_loggerName = typeof(T).FullName;
		}

		public void Error(string message, Exception ex = null, IList<LoggingParameter> parameters = null)
		{
			LogWithMessage(LogLevel.Error, message, ex, parameters);
		}

		public void Error(Exception e, IList<LoggingParameter> parameters = null)
		{
			Log(LogLevel.Error, e, parameters);
		}

		public void Fatal(string message, Exception ex)
		{
			LogWithMessage(LogLevel.Fatal, message, ex);
		}

		public void Fatal(Exception e)
		{
			Log(LogLevel.Fatal, e);
		}

		private void Log(LogLevel level, Exception e, IList<LoggingParameter> parameters = null)
		{
			if (e == null)
				return;

			Log(level,
				r =>
				{
					r.Message = e.Message;
					r.Exception = e;
				}, parameters: parameters);
		}

		public void Device(string message, long deviceId, string stacktrace)
		{
			Log(LogLevel.Warn,
				r =>
				{
					r.Message = $"Device log : {message}";
					r.Properties["deviceId"] = deviceId.ToString();
				}, "device");
		}

		public void Info(string message, IList<LoggingParameter> parameters = null)
		{
			if (string.IsNullOrEmpty(message))
				return;

			Log(LogLevel.Info, r => r.Message = message, parameters: parameters);
		}

		public void Info(Exception exception)
		{
			if (exception == null)
				return;

			Log(LogLevel.Info, r => r.Exception = exception);
		}

		public void Warn(Exception e)
		{
			Log(LogLevel.Warn, e);
		}


		public void Warn(string message, Exception ex = null, IList<LoggingParameter> parameters = null)
		{
			LogWithMessage(LogLevel.Warn, message, ex, parameters);
		}

		public void Debug(Exception exception)
		{
			if (exception == null)
				return;

			Log(LogLevel.Debug, r => r.Exception = exception);
		}

		public void Debug(string message, Exception exception = null)
		{
			LogWithMessage(LogLevel.Debug, message, exception);
		}

		private void LogWithMessage(LogLevel logLevel, string message, Exception exception = null, IList<LoggingParameter> parameters = null)
		{
			if (string.IsNullOrEmpty(message) && exception == null)
				return;

			Log(logLevel, r =>
			{
				r.Message = string.IsNullOrEmpty(message) ? exception.Message : message;
				r.Exception = exception;
			}, parameters: parameters);
		}

		private void Log(LogLevel level, Action<LogEventInfo> customize, string logger = null, IList<LoggingParameter> parameters = null)
		{
			if (string.IsNullOrEmpty(logger))
				logger = _loggerName;

			try
			{
				var logMessage = LogEventInfo.Create(level, logger, "");

				foreach (var p in parameters ?? _loggingParametersProvider.GetParameters())
				{
					logMessage.Properties[p.Name] = p.Value;
				}

				customize?.Invoke(logMessage);
				LogManager.GetLogger(logger).Log(logMessage);
			}
			catch (Exception ex)
			{
				_systemEventLogger.Log(ex);
			}
		}
	}
}