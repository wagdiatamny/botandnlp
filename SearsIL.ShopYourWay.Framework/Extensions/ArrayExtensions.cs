namespace SearsIL.ShopYourWay.Framework.Extensions
{
	public static class ArrayExtensions
	{
		public static T[] AsArray<T>(this T target)
		{
			return new[] { target };
		}
	}
}