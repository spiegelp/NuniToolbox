using System.Windows.Input;

namespace NuniToolbox.Mvvm.Commands;

/// <summary>
/// Base class for commands providing common functionality.
/// </summary>
public abstract class BaseDelegateCommand : ICommand
{
    /// <summary>
    /// The <see cref="CanExecuteChanged" /> event.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    private readonly SynchronizationContext m_synchronizationContext;

    /// <summary>
    /// Creates a new <see cref="BaseDelegateCommand" />.
    /// </summary>
    public BaseDelegateCommand()
    {
        m_synchronizationContext = SynchronizationContext.Current;
    }

    /// <summary>
    /// Checks if the command can be executed.
    /// </summary>
    /// <param name="parameter">The command parameter</param>
    /// <returns></returns>
    public abstract bool CanExecute(object parameter);

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="parameter">The command parameter</param>
    public abstract void Execute(object parameter);

    /// <summary>
    /// Raises the <see cref="CanExecuteChanged" /> event.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        OnCanExecuteChanged();
    }

    /// <summary>
    /// A helper method for raising the <see cref="CanExecuteChanged" /> event.
    /// </summary>
    protected virtual void OnCanExecuteChanged()
    {
        EventHandler canExecuteChangedHandler = CanExecuteChanged;

        if (canExecuteChangedHandler is not null)
        {
            if (m_synchronizationContext is not null && m_synchronizationContext != SynchronizationContext.Current)
            {
                m_synchronizationContext.Post(x => canExecuteChangedHandler(this, EventArgs.Empty), null);
            }
            else
            {
                canExecuteChangedHandler(this, EventArgs.Empty);
            }
        }
    }
}
