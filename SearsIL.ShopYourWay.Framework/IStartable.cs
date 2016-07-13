using SearsIL.ShopYourWay.Framework.IoC;

namespace SearsIL.ShopYourWay.Framework
{
	[RegisterAllImplementations]
	public interface IStartable
	{
		void Start();
		void Stop();
	}
}