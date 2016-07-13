using System;
using SearsIL.ShopYourWay.Framework.IoC;

namespace SearsIL.ShopYourWay.Framework.Logging
{
	[RegisterAllImplementations]
	public interface ICustomLogger
	{
		bool Log(Exception ex, ILogger logger);
	}
}