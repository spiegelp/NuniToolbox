using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NuniToolbox.Ui.Commands
{
    /// <summary>
    /// A simple, ready to use command implementation without a command parameter.
    /// </summary>
    public class DelegateCommand : BaseDelegateCommand
    {
        private readonly Action m_executeMethod;
        private readonly Func<bool> m_canExecuteMethod;

        /// <summary>
        /// Creates a new <see cref="DelegateCommand" />.
        /// </summary>
        /// <param name="executeMethod">The method for executing the command</param>
        public DelegateCommand(Action executeMethod) : this (executeMethod, () => true) { }

        /// <summary>
        /// Creates a new <see cref="DelegateCommand" />.
        /// </summary>
        /// <param name="executeMethod">The method for executing the command</param>
        /// <param name="canExecuteMethod">The method to check if the command can be executed</param>
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base()
        {
            m_executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod), $"The argument {nameof(executeMethod)} must not be null");
            m_canExecuteMethod = canExecuteMethod ?? throw new ArgumentNullException(nameof(canExecuteMethod), $"The argument {nameof(canExecuteMethod)} must not be null");
        }

        /// <summary>
        /// Checks if the command can be executed.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return m_canExecuteMethod();
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                m_executeMethod();
            }
        }
    }

    /// <summary>
    /// A read to use command implementation with a command parameter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateCommand<T> : BaseDelegateCommand
    {
        private readonly Action<T> m_executeMethod;
        private readonly Predicate<T> m_canExecuteMethod;

        /// <summary>
        /// Creates a new <see cref="DelegateCommand" />.
        /// </summary>
        /// <param name="executeMethod">The method for executing the command</param>
        public DelegateCommand(Action<T> executeMethod) : this(executeMethod, parameter => true) { }

        /// <summary>
        /// Creates a new <see cref="DelegateCommand" />.
        /// </summary>
        /// <param name="executeMethod">The method for executing the command</param>
        /// <param name="canExecuteMethod">The method to check if the command can be executed</param>
        public DelegateCommand(Action<T> executeMethod, Predicate<T> canExecuteMethod)
            : base()
        {
            /*if (executeMethod == null)
            {
                throw new ArgumentNullException(nameof(executeMethod), $"The argument {nameof(executeMethod)} must not be null");
            }

            if (canExecuteMethod == null)
            {
                throw new ArgumentNullException(nameof(canExecuteMethod), $"The argument {nameof(canExecuteMethod)} must not be null");
            }

            Type genericType = typeof(T);
            TypeInfo genericTypeInfo = genericType.GetTypeInfo();

            if (genericTypeInfo.IsValueType
                && (!genericTypeInfo.IsGenericType || !typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition().GetTypeInfo())))
            {
                throw new InvalidCastException($"Cannot cast {genericType.FullName} as command parameter");
            }

            m_executeMethod = executeMethod;
            m_canExecuteMethod = canExecuteMethod;*/

            m_executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod), $"The argument {nameof(executeMethod)} must not be null");
            m_canExecuteMethod = canExecuteMethod ?? throw new ArgumentNullException(nameof(canExecuteMethod), $"The argument {nameof(canExecuteMethod)} must not be null");
        }

        /// <summary>
        /// Checks if the command can be executed.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return m_canExecuteMethod((T)parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The command parameter</param>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                m_executeMethod((T)parameter);
            }
        }
    }
}
