namespace SearsIL.ShopYourWay.Framework.Logging
{
	public class LoggingParameter
	{
		public string Name { get; }
		public object Value { get; }

		public LoggingParameter(string name, object value)
		{
			Name = name;
			Value = value;
		}
	}
}