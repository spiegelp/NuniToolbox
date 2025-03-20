using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NuniToolbox.Mvvm.ViewModel;

/// <summary>
/// A base class for view model objects implementing <see cref="IDisposable" /> and <see cref="INotifyPropertyChanged" />.
/// </summary>
public abstract class ViewModelObject : IDisposable, INotifyPropertyChanged
{
    private readonly object m_lockObject = new();

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
