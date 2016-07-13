using System.Configuration;

namespace SearsIL.ShopYourWay.Framework.DataAccess
{
	public interface IConnectionStringProvider
	{
		string Get();
	}

	public class ConnectionStringProvider : IConnectionStringProvider
	{
		public string Get()
		{
			return ConfigurationManager.ConnectionStrings["MySqlDbConnectionString"].ConnectionString;
		}
	}
}