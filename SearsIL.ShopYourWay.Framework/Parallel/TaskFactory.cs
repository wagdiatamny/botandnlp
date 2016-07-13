using System;
using System.Threading.Tasks;
using SearsIL.ShopYourWay.Framework.Exceptions;
using SearsIL.ShopYourWay.Framework.Logging;

namespace SearsIL.ShopYourWay.Framework.Parallel
{
	public interface ITaskFactory
	{
		void RunTask(Type serviceType, Action action);
	}

	public class TaskFactory : ITaskFactory
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILoggingParametersProvider _loggingParametersProvider;

		public TaskFactory(ILoggerFactory loggerFactory, ILoggingParametersProvider loggingParametersProvider)
		{
			_loggerFactory = loggerFactory;
			_loggingParametersProvider = loggingParametersProvider;
		}

		public void RunTask(Type serviceType, Action action)
		{
			var parameters = _loggingParametersProvider.GetParameters();

			var logger = new TaskLogger(_loggerFactory.Create(serviceType), parameters);

			new Task(() =>
			{
				try
				{
					action();
				}
				catch (Exception ex)
				{
					if (ex is ILogWarnException)
						logger.Warn(ex);
					else
						logger.Error(ex);
				}
			}).Start();
		}
	}
}