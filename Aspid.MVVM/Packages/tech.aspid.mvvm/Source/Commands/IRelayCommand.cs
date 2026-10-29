using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines a command without parameters.
    /// </summary>
    public interface IRelayCommand
    {
        /// <summary>
        /// Occurs when the result of <see cref="CanExecute()"/> may have changed.
        /// </summary>
        public event Action<IRelayCommand> CanExecuteChanged;
        
        /// <summary>
        /// Determines whether the command can execute.
        /// </summary>
        /// <returns><see langword="true"/> if the command can execute; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute();

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute();
        
        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// Defines a command with one parameter.
    /// </summary>
    /// <typeparam name="T">The command parameter type.</typeparam>
    public interface IRelayCommand<in T>
    {
        /// <summary>
        /// Occurs when the result of <see cref="CanExecute(T)"/> may have changed.
        /// </summary>
        public event Action<IRelayCommand<T>> CanExecuteChanged;
        
        /// <summary>
        /// Determines whether the command can execute with the given parameter.
        /// </summary>
        /// <param name="param">The command parameter.</param>
        /// <returns><see langword="true"/> if the command can execute; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute(T? param);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="param">The command parameter.</param>
        public void Execute(T? param);
        
        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// Defines a command with two parameters.
    /// </summary>
    /// <typeparam name="T1">The first command parameter type.</typeparam>
    /// <typeparam name="T2">The second command parameter type.</typeparam>
    public interface IRelayCommand<in T1, in T2>
    {
        /// <summary>
        /// Occurs when the result of <see cref="CanExecute(T1, T2)"/> may have changed.
        /// </summary>
        public event Action<IRelayCommand<T1, T2>> CanExecuteChanged;
        
        /// <summary>
        /// Determines whether the command can execute with the given parameters.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <returns><see langword="true"/> if the command can execute; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute(T1? param1, T2? param2);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        public void Execute(T1? param1, T2? param2);
        
        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// Defines a command with three parameters.
    /// </summary>
    /// <typeparam name="T1">The first command parameter type.</typeparam>
    /// <typeparam name="T2">The second command parameter type.</typeparam>
    /// <typeparam name="T3">The third command parameter type.</typeparam>
    public interface IRelayCommand<in T1, in T2, in T3>
    {
        /// <summary>
        /// Occurs when the result of <see cref="CanExecute(T1, T2, T3)"/> may have changed.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3>> CanExecuteChanged;
        
        /// <summary>
        /// Determines whether the command can execute with the given parameters.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <returns><see langword="true"/> if the command can execute; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        public void Execute(T1? param1, T2? param2, T3? param3);
        
        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// Defines a command with four parameters.
    /// </summary>
    /// <typeparam name="T1">The first command parameter type.</typeparam>
    /// <typeparam name="T2">The second command parameter type.</typeparam>
    /// <typeparam name="T3">The third command parameter type.</typeparam>
    /// <typeparam name="T4">The fourth command parameter type.</typeparam>
    public interface IRelayCommand<in T1, in T2, in T3, in T4>
    {
        /// <summary>
        /// Occurs when the result of <see cref="CanExecute(T1, T2, T3, T4)"/> may have changed.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3, T4>> CanExecuteChanged;
        
        /// <summary>
        /// Determines whether the command can execute with the given parameters.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <param name="param4">The fourth command parameter.</param>
        /// <returns><see langword="true"/> if the command can execute; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <param name="param4">The fourth command parameter.</param>
        public void Execute(T1? param1, T2? param2, T3? param3, T4? param4);
        
        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
}
