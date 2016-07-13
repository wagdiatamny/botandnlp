using System;
using System.Collections.Generic;
using System.Linq;

namespace SearsIL.ShopYourWay.Framework.Extensions
{
	public static class EqualityComparerForExtension
	{
		public static IEqualityComparer<T> For<T>(params Func<T, object>[] keySelectors)
		{
			return new CompositeKeyEqualityComparer<T>(keySelectors);
		}
	}

	public class CompositeKeyEqualityComparer<T> : IEqualityComparer<T>
	{
		private readonly IList<Func<T, object>> _keySelectors;

		public CompositeKeyEqualityComparer(IList<Func<T, object>> keySelectors)
		{
			_keySelectors = keySelectors;
		}

		public bool Equals(T x, T y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (x == null || y == null) return false;

			return _keySelectors.All(ks => Equals(ks(x), ks(y)));
		}

		public int GetHashCode(T obj)
		{
			return _keySelectors.Aggregate(0, (h, ks) => (h * 397) ^ ks(obj).GetHashCode());
		}
	}

	
}