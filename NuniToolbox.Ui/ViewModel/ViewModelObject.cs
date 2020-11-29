using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace NuniToolbox.Ui.ViewModel
{
    /// <summary>
    /// A base class for view model objects implementing <see cref="IDisposable" /> and <see cref="INotifyPropertyChanged" />.
    /// </summary>
    public abstract class ViewModelObject : IDisposable, INotifyPropertyChanged
    {
        private readonly object m_lockObject = new object();

        /// <summary>
        /// The <see cref="PropertyChanged" /> event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private bool m_isBusy;

        /// <summary>
        /// Boolean indicating if the an operation is running.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                lock (m_lockObject)
                {
                    return m_isBusy;
                }
            }

            set
            {
                lock (m_lockObject)
                {
                    m_isBusy = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Creates a new <see cref="ViewModelObject" />.
        /// </summary>
        public ViewModelObject()
        {
            m_isBusy = false;
        }

        /// <summary>
        /// Disposes the view model object.
        /// </summary>
        public virtual void Dispose() { }

        /// <summary>
        /// Runs the specified action on the thread of the user interface.
        /// </summary>
        /// <param name="action">The action to run</param>
        protected void RunWithinUiThread(Action action)
        {
            RunWithinUiThread(action, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Runs the specified action on the thread of the user interface.
        /// </summary>
        /// <param name="action">The action to run</param>
        /// <param name="priority">The priority for the dispatcher</param>
        protected void RunWithinUiThread(Action action, DispatcherPriority priority)
        {
            Application.Current.Dispatcher.Invoke(action, priority);
        }

        /// <summary>
        /// Handles the property changed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void PropertyChangedHandler(object sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged(args.PropertyName);
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
