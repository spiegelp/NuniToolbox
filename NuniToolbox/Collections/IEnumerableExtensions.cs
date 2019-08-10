using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuniToolbox.Collections
{
    /// <summary>
    /// A class with extension methods for <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class IEnumerableExtensions
    {
        #region Foreach

        /// <summary>
        /// Invokes the specified action for each item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action.Invoke(item);
            }
        }

        #endregion



        #region GroupByToDictionary

        /// <summary>
        /// Groups the items by the specified key selector into a <see cref="Dictionary{TKey, TValue}" />.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static Dictionary<TKey, List<TValue>> GroupByToDictionary<TKey, TValue>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector)
        {
            return enumerable
                .GroupBy(keySelector)
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        /// <summary>
        /// Groups the items by the specified key selector into a <see cref="Dictionary{TKey, TValue}" /> using the specified <see cref="IEqualityComparer{T}" />.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static Dictionary<TKey, List<TValue>> GroupByToDictionary<TKey, TValue>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return enumerable
                .GroupBy(keySelector, comparer)
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        #endregion GroupByToDictionary



        #region ToSet

        /// <summary>
        /// Converts the enumerable into a <see cref="HashSet{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static HashSet<T> ToSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }

        /// <summary>
        /// Converts the enumerable into a <see cref="HashSet{T}" /> using the specified <see cref="IEqualityComparer{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static HashSet<T> ToSet<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer)
        {
            return new HashSet<T>(enumerable, comparer);
        }

        /// <summary>
        /// Converts the enumerable into a <see cref="HashSet{TResult}" />.
        /// </summary>
        /// <typeparam name="T">The input type of the items</typeparam>
        /// <typeparam name="TResult">The output type of the items</typeparam>
        /// <param name="enumerable"></param>
        /// <param name="elementSelector"></param>
        /// <returns></returns>
        public static HashSet<TResult> ToSet<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> elementSelector)
        {
            return new HashSet<TResult>(enumerable.Select(elementSelector));
        }

        /// <summary>
        /// Converts the enumerable into a <see cref="HashSet{TResult}" /> using the specified <see cref="IEqualityComparer{TResult}" />.
        /// </summary>
        /// <typeparam name="T">The input type of the items</typeparam>
        /// <typeparam name="TResult">The output type of the items</typeparam>
        /// <param name="enumerable"></param>
        /// <param name="elementSelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static HashSet<TResult> ToSet<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> elementSelector, IEqualityComparer<TResult> comparer)
        {
            return new HashSet<TResult>(enumerable.Select(elementSelector), comparer);
        }

        #endregion ToSet



        #region Sorted

        /// <summary>
        /// Returns a sorted instance of enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="keySelector"></param>
        /// <param name="reversed">set to true for descending sort order</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> Sorted<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, bool reversed = false)
        {
            if (reversed)
            {
                return enumerable.OrderByDescending(keySelector);
            }
            else
            {
                return enumerable.OrderBy(keySelector);
            }
        }

        /// <summary>
        /// Returns a sorted instance of the enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <param name="reversed">set to true for descending sort order</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> Sorted<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey> comparer, bool reversed = false)
        {
            if (reversed)
            {
                return enumerable.OrderByDescending(keySelector, comparer);
            }
            else
            {
                return enumerable.OrderBy(keySelector, comparer);
            }
        }

        #endregion Sorted
    }
}
