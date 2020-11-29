using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NuniToolbox.Ui.ViewModel
{
    /// <summary>
    /// View model class for an item that can be selected.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SelectableViewModel<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// The <see cref="PropertyChanged" /> event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private bool m_isSelected;
        private T m_item;

        /// <summary>
        /// Boolean indicating if this item is selected or not.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return m_isSelected;
            }

            set
            {
                m_isSelected = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The item to be selected or not.
        /// </summary>
        public T Item
        {
            get
            {
                return m_item;
            }

            set
            {
                m_item = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Creates a new <see cref="SelectableViewModel" />.
        /// </summary>
        /// <param name="item">The item to be selected or not</param>
        /// <param name="isSelected">Boolean indicating if this item is selected or not</param>
        public SelectableViewModel(T item, bool isSelected)
        {
            m_item = item;
            m_isSelected = isSelected;
        }

        /// <summary>
        /// A helper method for raising the <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged is not null && !string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
