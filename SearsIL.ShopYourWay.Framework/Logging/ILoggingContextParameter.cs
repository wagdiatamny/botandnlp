using SearsIL.ShopYourWay.Framework.IoC;

namespace SearsIL.ShopYourWay.Framework.Logging
{
	[RegisterAllImplementations]
	public interface ILoggingContextParameter
	{
		LoggingParameterInfo GetInfo();
		LoggingParameter GetValue();
	}

	public class LoggingParameterInfo
	{
		public string Name { get; }
		public string Layout { get; }

		public LoggingParameterInfo(string name, string layout)
		{
			Name = name;
			Layout = layout;
		}
	}
}