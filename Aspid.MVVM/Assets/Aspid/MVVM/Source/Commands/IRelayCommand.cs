using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// An interface for a command that can be executed without parameters.
    /// </summary>
    public interface IRelayCommand
    {
        /// <summary>
        /// Raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand> CanExecuteChanged;
        
        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <returns><see langword="true"/> if the command can be executed; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute();

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute();
        
        /// <summary>
        /// Notifies that the execution state of the command has changed, raising the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// An interface for a command that can be executed with a parameter.
    /// </summary>
    /// <typeparam name="T">The type of the parameter passed to the command.</typeparam>
    public interface IRelayCommand<in T>
    {
        /// <summary>
        /// Raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand<T>> CanExecuteChanged;
        
        /// <summary>
        /// Executes the command with the given parameter.
        /// </summary>
        /// <param name="param">The parameter used to execute the command.</param>
        public bool CanExecute(T? param);

        /// <summary>
        /// Executes the command with the given parameter.
        /// </summary>
        /// <param name="param">The parameter used to execute the command.</param>
        public void Execute(T? param);
        
        /// <summary>
        /// Notifies that the execution state of the command has changed, raising the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// An interface for a command that can be executed with two parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter passed to the command.</typeparam>
    /// <typeparam name="T2">The type of the second parameter passed to the command.</typeparam>
    public interface IRelayCommand<in T1, in T2>
    {
        /// <summary>
        /// Raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand<T1, T2>> CanExecuteChanged;
        
        /// <summary>
        /// Determines whether the command can be executed with the given parameters.
        /// </summary>
        /// <param name="param1">The first parameter used to execute the command.</param>
        /// <param name="param2">The second parameter used to execute the command.</param>
        /// <returns><see langword="true"/> if the command can be executed; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute(T1? param1, T2? param2);

        /// <summary>
        /// Executes the command with the given parameters.
        /// </summary>
        /// <param name="param1">The first parameter used to execute the command.</param>
        /// <param name="param2">The second parameter used to execute the command.</param>
        public void Execute(T1? param1, T2? param2);
        
        /// <summary>
        /// Notifies that the execution state of the command has changed, raising the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// An interface for a command that can be executed with three parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter passed to the command.</typeparam>
    /// <typeparam name="T2">The type of the second parameter passed to the command.</typeparam>
    /// <typeparam name="T3">The type of the third parameter passed to the command.</typeparam>
    public interface IRelayCommand<in T1, in T2, in T3>
    {
        /// <summary>
        /// Raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3>> CanExecuteChanged;
        
        /// <summary>
        /// Determines whether the command can be executed with the given parameters.
        /// </summary>
        /// <param name="param1">The first parameter used to execute the command.</param>
        /// <param name="param2">The second parameter used to execute the command.</param>
        /// <param name="param3">The third parameter used to execute the command.</param>
        /// <returns><see langword="true"/> if the command can be executed; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3);

        /// <summary>
        /// Executes the command with the given parameters.
        /// </summary>
        /// <param name="param1">The first parameter used to execute the command.</param>
        /// <param name="param2">The second parameter used to execute the command.</param>
        /// <param name="param3">The third parameter used to execute the command.</param>
        public void Execute(T1? param1, T2? param2, T3? param3);
        
        /// <summary>
        /// Notifies that the execution state of the command has changed, raising the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
    
    /// <summary>
    /// An interface for a command that can be executed with four parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter passed to the command.</typeparam>
    /// <typeparam name="T2">The type of the second parameter passed to the command.</typeparam>
    /// <typeparam name="T3">The type of the third parameter passed to the command.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter passed to the command.</typeparam>
    public interface IRelayCommand<in T1, in T2, in T3, in T4>
    {
        /// <summary>
        /// Raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3, T4>> CanExecuteChanged;
        
        /// <summary>
        /// Determines whether the command can be executed with the given parameters.
        /// </summary>
        /// <param name="param1">The first parameter used to execute the command.</param>
        /// <param name="param2">The second parameter used to execute the command.</param>
        /// <param name="param3">The third parameter used to execute the command.</param>
        /// <param name="param4">The fourth parameter used to execute the command.</param>
        /// <returns><see langword="true"/> if the command can be executed; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4);

        /// <summary>
        /// Executes the command with the given parameters.
        /// </summary>
        /// <param name="param1">The first parameter used to execute the command.</param>
        /// <param name="param2">The second parameter used to execute the command.</param>
        /// <param name="param3">The third parameter used to execute the command.</param>
        /// <param name="param4">The fourth parameter used to execute the command.</param>
        public void Execute(T1? param1, T2? param2, T3? param3, T4? param4);
        
        /// <summary>
        /// Notifies that the execution state of the command has changed, raising the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        void NotifyCanExecuteChanged();
    }
}
