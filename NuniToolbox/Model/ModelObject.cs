using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NuniToolbox.Model
{
    /// <summary>
    /// A base class for model objects implementing <see cref="INotifyPropertyChanged" />.
    /// </summary>
    public abstract class ModelObject : INotifyPropertyChanged
    {
        /// <summary>
        /// The <see cref="PropertyChanged" /> event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public ModelObject() { }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null && !string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
