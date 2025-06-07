using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Implementation of the <see cref="IRelayCommand"/> interface for commands without parameters.
    /// Allows defining whether the command can be executed and executing the command.
    /// </summary>
    public sealed class RelayCommand : IRelayCommand
    {
        /// <summary>
        /// Event that is raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand>? CanExecuteChanged;
        
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class, taking an action to execute the command.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <c>null</c>.</exception>
        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class, taking an action to execute the command 
        /// and a function to check whether it can be executed.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <param name="canExecute">A function that returns <c>true</c> if the command can be executed; otherwise, <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> or <paramref name="canExecute"/> is <c>null</c>.</exception>
        public RelayCommand(Action execute, Func<bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise, <c>false</c>.</returns>
        public bool CanExecute() =>
            _canExecute?.Invoke() ?? true;

        /// <summary>
        /// Executes the command if it can be executed.
        /// </summary>
        public void Execute()
        {
            if (CanExecute()) 
                _execute.Invoke();
        }
        
        /// <summary>
        /// Notifies that the ability to execute the command has changed.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    /// <summary>
    /// Implementation of the <see cref="IRelayCommand{T}"/> interface for commands with one parameter.
    /// Allows defining whether the command can be executed and executing the command with a specified parameter.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    public sealed class RelayCommand<T> : IRelayCommand<T>
    {
        /// <summary>
        /// Event that is raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand<T>>? CanExecuteChanged;
        
        private readonly Action<T?> _execute;
        private readonly Func<T?, bool>? _canExecute;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class, taking an action to execute the command.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <c>null</c>.</exception>
        public RelayCommand(Action<T?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class, taking an action to execute the command 
        /// and a function to check whether it can be executed.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <param name="canExecute">A function that returns <c>true</c> if the command can be executed; otherwise, <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <c>null</c>.</exception>
        public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Determines whether the command can be executed with the specified parameter.
        /// </summary>
        /// <param name="param">The parameter passed for checking the ability to execute the command.</param>
        /// <returns><c>true</c> if the command can be executed; otherwise, <c>false</c>.</returns>
        public bool CanExecute(T? param) =>
            _canExecute?.Invoke(param) ?? true;
        
        /// <summary>
        /// Executes the command with the specified parameter if it can be executed.
        /// </summary>
        /// <param name="param">The parameter passed to the command for execution.</param>
        public void Execute(T? param)
        {
            if (CanExecute(param)) 
                _execute.Invoke(param);
        }
        
        /// <summary>
        /// Notifies that the ability to execute the command has changed.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    /// <summary>
    /// Implementation of the <see cref="IRelayCommand{T1, T2}"/> interface for commands with two parameters.
    /// Allows defining whether the command can be executed and executing the command with specified parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second command parameter.</typeparam>
    public sealed class RelayCommand<T1, T2> : IRelayCommand<T1, T2>
    {
        /// <summary>
        /// Event that is raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand<T1, T2>>? CanExecuteChanged;
        
        private readonly Action<T1?, T2?> _execute;
        private readonly Func<T1?, T2?, bool>? _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T1, T2}"/> class, taking an action to execute the command.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T1, T2}"/> class, taking an action to execute the command 
        /// and a function to check whether it can be executed.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <param name="canExecute">A function that returns <c>true</c> if the command can be executed; otherwise, <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?> execute, Func<T1?, T2?, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Determines whether the command can be executed with the specified parameters.
        /// </summary>
        /// <param name="param1">The first parameter passed for checking the ability to execute the command.</param>
        /// <param name="param2">The second parameter passed for checking the ability to execute the command.</param>
        /// <returns><c>true</c> if the command can be executed; otherwise, <c>false</c>.</returns>
        public bool CanExecute(T1? param1, T2? param2) =>
            _canExecute?.Invoke(param1, param2) ?? true;
        
        /// <summary>
        /// Executes the command with the specified parameters if it can be executed.
        /// </summary>
        /// <param name="param1">The first parameter passed to the command for execution.</param>
        /// <param name="param2">The second parameter passed to the command for execution.</param>
        public void Execute(T1? param1, T2? param2)
        {
            if (CanExecute(param1, param2)) 
                _execute.Invoke(param1, param2);
        }
        
        /// <summary>
        /// Notifies that the ability to execute the command has changed.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    /// <summary>
    /// Implementation of the <see cref="IRelayCommand{T1, T2, T3}"/> interface for commands with three parameters.
    /// Allows determining whether the command can execute and executing the command with the specified parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second command parameter.</typeparam>
    /// <typeparam name="T3">The type of the third command parameter.</typeparam>
    public sealed class RelayCommand<T1, T2, T3> : IRelayCommand<T1, T2, T3>
    {
        /// <summary>
        /// Event that is raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3>>? CanExecuteChanged;
        
        private readonly Action<T1?, T2?, T3?> _execute;
        private readonly Func<T1?, T2?, T3?, bool>? _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T1, T2, T3}"/> class, taking an action to execute the command.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T1, T2, T3}"/> class, taking an action to execute the command and a function to check if it can execute.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <param name="canExecute">A function that returns <c>true</c> if the command can execute; otherwise, <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?> execute, Func<T1?, T2?, T3?, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Determines whether the command can be executed with the specified parameters.
        /// </summary>
        /// <param name="param1">The first parameter passed to check if the command can execute.</param>
        /// <param name="param2">The second parameter passed to check if the command can execute.</param>
        /// <param name="param3">The third parameter passed to check if the command can execute.</param>
        /// <returns><c>true</c> if the command can be executed; otherwise, <c>false</c>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3) =>
            _canExecute?.Invoke(param1, param2, param3) ?? true;
        
        /// <summary>
        /// Executes the command with the specified parameters if it can be executed.
        /// </summary>
        /// <param name="param1">The first parameter passed to the command for execution.</param>
        /// <param name="param2">The second parameter passed to the command for execution.</param>
        /// <param name="param3">The third parameter passed to the command for execution.</param>
        public void Execute(T1? param1, T2? param2, T3? param3)
        {
            if (CanExecute(param1, param2, param3)) 
                _execute.Invoke(param1, param2, param3);
        }
        
        /// <summary>
        /// Notifies that the ability to execute the command has changed.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    /// <summary>
    /// Implementation of the <see cref="IRelayCommand{T1, T2, T3, T4}"/> interface for commands with four parameters.
    /// Allows determining whether the command can execute and executing the command with the specified parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second command parameter.</typeparam>
    /// <typeparam name="T3">The type of the third command parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth command parameter.</typeparam>
    public sealed class RelayCommand<T1, T2, T3, T4> : IRelayCommand<T1, T2, T3, T4>
    {
        /// <summary>
        /// Event that is raised when the ability to execute the command changes.
        /// </summary>
        public event Action<IRelayCommand<T1, T2, T3, T4>>? CanExecuteChanged;
        
        private readonly Action<T1?, T2?, T3?, T4?> _execute;
        private readonly Func<T1?, T2?, T3?, T4?, bool>? _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T1, T2, T3, T4}"/> class, taking an action to execute the command.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?, T4?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T1, T2, T3, T4}"/> class, taking an action to execute the command and a function to check if it can execute.
        /// </summary>
        /// <param name="execute">The action that will be executed by the command.</param>
        /// <param name="canExecute">A function that returns <c>true</c> if the command can execute; otherwise, <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <c>null</c>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?, T4?> execute, Func<T1?, T2?, T3?, T4?, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        /// <summary>
        /// Determines whether the command can be executed with the specified parameters.
        /// </summary>
        /// <param name="param1">The first parameter passed to check if the command can execute.</param>
        /// <param name="param2">The second parameter passed to check if the command can execute.</param>
        /// <param name="param3">The third parameter passed to check if the command can execute.</param>
        /// <param name="param4">The fourth parameter passed to check if the command can execute.</param>
        /// <returns><c>true</c> if the command can be executed; otherwise, <c>false</c>.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4) =>
            _canExecute?.Invoke(param1, param2, param3, param4) ?? true;
        
        /// <summary>
        /// Executes the command with the specified parameters if it can be executed.
        /// </summary>
        /// <param name="param1">The first parameter passed to the command for execution.</param>
        /// <param name="param2">The second parameter passed to the command for execution.</param>
        /// <param name="param3">The third parameter passed to the command for execution.</param>
        /// <param name="param4">The fourth parameter passed to the command for execution.</param>
        public void Execute(T1? param1, T2? param2, T3? param3, T4? param4)
        {
            if (CanExecute(param1, param2, param3, param4)) 
                _execute.Invoke(param1, param2, param3, param4);
        }
        
        /// <summary>
        /// Notifies that the ability to execute the command has changed.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
}