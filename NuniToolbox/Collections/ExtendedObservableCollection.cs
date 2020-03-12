using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NuniToolbox.Collections
{
    /// <summary>
    /// An optimized version of a <see cref="ObservableCollection{T}" /> for adding many items or replacing all items with only one
    /// changed event at the end of the operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtendedObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Creates a new <see cref="ExtendedObservableCollection" />.
        /// </summary>
        public ExtendedObservableCollection() : base() { }

        /// <summary>
        /// Creates a new <see cref="ExtendedObservableCollection" />.
        /// </summary>
        public ExtendedObservableCollection(IEnumerable<T> collection) : base(collection) { }

        /// <summary>
        /// Creates a new <see cref="ExtendedObservableCollection" />.
        /// </summary>
        public ExtendedObservableCollection(List<T> list) : base(list) { }

        /// <summary>
        /// Adds all items to the collection.
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<T> items)
        {
            if (items != null && items.Any())
            {
                AddRangeInternal(items);
            }
        }

        private void AddRangeInternal(IEnumerable<T> items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    Items.Add(item);
                }
            }

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Replaces the collection with the specified items.
        /// </summary>
        /// <param name="items"></param>
        public void ReplaceWith(IEnumerable<T> items)
        {
            if (Items.Any() || (items != null && items.Any()))
            {
                Items.Clear();
                AddRangeInternal(items);
            }
        }
    }
}
