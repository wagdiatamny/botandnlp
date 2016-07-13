using System;
using System.Diagnostics;

namespace SearsIL.ShopYourWay.Framework.Logging
{
	public interface ISystemEventLogger
	{
		void Log(Exception exception);
		void Log(string message);
	}

	public class SystemEventLogger : ISystemEventLogger
	{
		public void Log(Exception exception)
		{
			var message = $"Exception:\n{exception.Message}\n\nStackTrace:\n{exception.StackTrace}";
			EventLog.WriteEntry("Alfie", message, EventLogEntryType.Error, 4321);
		}

		public void Log(string message)
		{
			EventLog.WriteEntry("Alfie", message, EventLogEntryType.Error, 4321);
		}
	}
}