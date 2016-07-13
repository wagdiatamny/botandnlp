using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;
using SearsIL.ShopYourWay.Framework.DataAccess;

namespace SearsIL.ShopYourWay.Framework.Logging
{
	public class NLogInstaller : ILogInstaller
	{
		private readonly IEnumerable<ILoggingContextParameter> _contextParameters;
		private readonly IConnectionStringProvider _connectionStringProvider;

		public NLogInstaller(IEnumerable<ILoggingContextParameter> contextParameters,
			IConnectionStringProvider connectionStringProvider)
		{
			_contextParameters = contextParameters;
			_connectionStringProvider = connectionStringProvider;
		}

		public void Configure()
		{
			var config = new LoggingConfiguration();

			var target = new DatabaseTarget();

			ConfigureDatabaseParameters(_connectionStringProvider.Get(), target, config);

			ConfigureLoggingLevel(target, config);

			LogManager.Configuration = config;

		}

		private void ConfigureDatabaseParameters(string connectionString, DatabaseTarget target, LoggingConfiguration config)
		{
			target.DBProvider = "MySql.Data.MySqlClient";
			target.ConnectionString = connectionString;

			var contextedParameterColumnNames = BuildContextedParameterColumnNames();
			var contextedParameterNames = BuildContextedParameterNames();

			target.CommandText = "INSERT INTO log (date,level,logger,message,exception" + contextedParameterColumnNames +") VALUES (?date, ?level, ?logger, ?message, ?exception" + contextedParameterNames + ")";
			target.Parameters.Add(new DatabaseParameterInfo("?date", "${date:universalTime=true:format=yyyy-MM-dd HH\\:mm\\:ss}"));
			target.Parameters.Add(new DatabaseParameterInfo("?level", "${level}"));
			target.Parameters.Add(new DatabaseParameterInfo("?logger", "${logger}"));
			target.Parameters.Add(new DatabaseParameterInfo("?message", "${message}"));
			target.Parameters.Add(new DatabaseParameterInfo("?exception", "${exception:format=ToString,StackTrace}"));

			foreach (var info in _contextParameters.Select(p => p.GetInfo()))
				target.Parameters.Add(new DatabaseParameterInfo(info.Name, info.Layout));
			
			config.AddTarget("database", new AsyncTargetWrapper(target));
		}

		private string BuildContextedParameterNames()
		{
			if (!_contextParameters.Any())
				return string.Empty;

			return ", ?" + string.Join(", ?", _contextParameters.Select(x => x.GetInfo().Name));
		}

		private string BuildContextedParameterColumnNames()
		{
			if (!_contextParameters.Any())
				return string.Empty;

			return ", " + string.Join(", ", _contextParameters.Select(x => x.GetInfo().Name));
		}

		private static LogLevel SetLogLevel()
		{
			var configValue = ConfigurationManager.AppSettings["logging:min-level"];
			LogLevel logLevel = null;
			if (!string.IsNullOrEmpty(configValue))
				logLevel = LogLevel.FromString(configValue);

			return logLevel ?? LogLevel.Info;
		}

		private static void ConfigureLoggingLevel(DatabaseTarget target, LoggingConfiguration config)
		{
			var logLevel = SetLogLevel();

			var loggingRule = new LoggingRule("*", logLevel, target);

			config.LoggingRules.Add(loggingRule);
		}
	}
}