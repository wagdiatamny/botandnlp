using System;
using System.Collections.Generic;
using SearsIL.ShopYourWay.Framework.IoC;
using SearsIL.ShopYourWay.Framework.Logging;

namespace SearsIL.ShopYourWay.Framework.Parallel
{
	[NotAService]
	public interface ITaskLogger
	{
		void Error(string message, Exception ex = null);
		void Error(Exception ex);
		void Info(string message);
		void Warn(string message, Exception ex = null);
		void Warn(Exception ex);
	}

	public class TaskLogger : ITaskLogger
	{
		private readonly ILogger _logger;
		private readonly IList<LoggingParameter> _parameters;

		public TaskLogger(ILogger logger, IList<LoggingParameter> parameters)
		{
			_logger = logger;
			_parameters = parameters;
		}

		public void Error(string message, Exception ex = null)
		{
			_logger.Error(message, ex, _parameters);
		}

		public void Error(Exception ex)
		{
			_logger.Error(ex, _parameters);
		}

		public void Info(string message)
		{
			_logger.Info(message, _parameters);
		}

		public void Warn(string message, Exception ex = null)
		{
			_logger.Warn(message, ex, _parameters);
		}

		public void Warn(Exception ex)
		{
			_logger.Warn(ex);
		}
	}
}