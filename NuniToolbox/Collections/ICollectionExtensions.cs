using System;
using System.Collections.Generic;
using System.Text;

namespace NuniToolbox.Collections
{
    /// <summary>
    /// A class with extension methods for <see cref="ICollection{T}" />.
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Adds all of the specified items to the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void AddAll<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection is ExtendedObservableCollection<T> observableCollection)
            {
                observableCollection.AddRange(items);
            }
            else
            {
                items.Foreach(item => collection.Add(item));
            }
        }
    }
}
