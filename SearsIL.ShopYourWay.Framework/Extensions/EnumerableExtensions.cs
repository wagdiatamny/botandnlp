using System;
using System.Collections.Generic;
using System.Linq;

namespace SearsIL.ShopYourWay.Framework.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T2> MoveToLast<T1, T2>(this IEnumerable<T2> filters)
		{
			return filters.MoveToFront(x => !(x is T1));
		}

		public static IEnumerable<T2> MoveToFront<T1, T2>(this IEnumerable<T2> filters)
		{
			return filters.MoveToFront(x => x is T1);
		}

		public static IEnumerable<T> MoveToFront<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			var lastItems = new List<T>();

			foreach (var item in source)
			{
				if (predicate(item))
					yield return item;
				else
					lastItems.Add(item);
			}

			foreach (var item in lastItems)
			{
				yield return item;
			}
		}

		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> target)
		{
			return target ?? Enumerable.Empty<T>();
		}

		public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> target, params Func<T, object>[] keySelectors)
		{
			return target.Distinct(EqualityComparerForExtension.For(keySelectors));
		}
	}
}