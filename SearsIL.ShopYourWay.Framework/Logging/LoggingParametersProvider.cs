using System;
using System.Collections.Generic;
using System.Linq;

namespace SearsIL.ShopYourWay.Framework.Logging
{
	public interface ILoggingParametersProvider
	{
		IList<LoggingParameter> GetParameters();
	}

	public class LoggingParametersProvider : ILoggingParametersProvider
	{
		private readonly IEnumerable<ILoggingContextParameter> _parameters;
		private readonly ISystemEventLogger _systemEventLogger;

		public LoggingParametersProvider(IEnumerable<ILoggingContextParameter> parameters, ISystemEventLogger systemEventLogger)
		{
			_parameters = parameters;
			_systemEventLogger = systemEventLogger;
		}

		public IList<LoggingParameter> GetParameters()
		{
			try
			{
				return _parameters
					.Select(GetParameter)
					.Where(x => x != null)
					.ToArray();
			}
			catch (Exception ex)
			{
				_systemEventLogger.Log(ex);
				return new LoggingParameter[0];
			}
		}

		private LoggingParameter GetParameter(ILoggingContextParameter parameter)
		{
			try
			{
				return parameter.GetValue();
			}
			catch (Exception ex)
			{
				_systemEventLogger.Log(ex);
				return null;
			}
		}
	}
}