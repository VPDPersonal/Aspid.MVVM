#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IRelayCommand"/> that runs a delegate, optionally gated by a predicate.
    /// </summary>
    public sealed class RelayCommand : IRelayCommand
    {
        /// <inheritdoc/>
        public event Action<IRelayCommand>? CanExecuteChanged;
        
        private static RelayCommand? _empty;
        private static RelayCommand? _emptyExecution;
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute()"/> returns
        /// <see langword="false"/>.
        /// </summary>
        public static RelayCommand Empty => _empty ??= new RelayCommand();

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute()"/> returns
        /// <see langword="true"/>.
        /// </summary>
        public static RelayCommand EmptyExecution => _emptyExecution ??= new RelayCommand(true);
        
        /// <summary>
        /// Creates a command that can always execute.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action execute, Func<bool>? canExecute)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private RelayCommand(bool value = false)
        {
            if (value) _execute = () => { };
            _canExecute = () => value;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <returns>The result of the predicate; <see langword="true"/> when there is none.</returns>
        public bool CanExecute()
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                return _canExecute?.Invoke() ?? true;
            }
        }

        /// <summary>
        /// Runs the action when <see cref="CanExecute()"/> returns <see langword="true"/>.
        /// </summary>
        public void Execute()
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                if (CanExecute()) 
                    _execute.Invoke();
            }
        }
        
        /// <inheritdoc/>
        public void NotifyCanExecuteChanged()
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                CanExecuteChanged?.Invoke(this);
            }
        }
    }
    
    /// <summary>
    /// <see cref="IRelayCommand{T}"/> that runs a delegate, optionally gated by a predicate.
    /// </summary>
    /// <typeparam name="T">The command parameter type.</typeparam>
    public sealed class RelayCommand<T> : IRelayCommand<T>
    {
        /// <inheritdoc/>
        public event Action<IRelayCommand<T>>? CanExecuteChanged;
        
        private static RelayCommand<T>? _empty;
        private static RelayCommand<T>? _emptyExecution;
        private readonly Action<T?> _execute;
        private readonly Func<T?, bool>? _canExecute;

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute(T)"/> returns
        /// <see langword="false"/>.
        /// </summary>
        public static RelayCommand<T> Empty => _empty ??= new RelayCommand<T>();

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute(T)"/> returns
        /// <see langword="true"/>.
        /// </summary>
        public static RelayCommand<T> EmptyExecution => _emptyExecution ??= new RelayCommand<T>(true);
        
        /// <summary>
        /// Creates a command that can always execute.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action<T?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private RelayCommand(bool value = false)
        {
            if (value) _execute = (_) => { };
            _canExecute = (_) => value;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <param name="param">The command parameter.</param>
        /// <returns>The result of the predicate; <see langword="true"/> when there is none.</returns>
        public bool CanExecute(T? param)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                return _canExecute?.Invoke(param) ?? true;
            }
        }

        /// <summary>
        /// Runs the action when <see cref="CanExecute(T)"/> returns <see langword="true"/>.
        /// </summary>
        /// <param name="param">The command parameter.</param>
        public void Execute(T? param)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                if (CanExecute(param)) 
                    _execute.Invoke(param);
            }
        }
        
        /// <inheritdoc/>
        public void NotifyCanExecuteChanged()
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                CanExecuteChanged?.Invoke(this);
            }
        }
    }
    
    /// <summary>
    /// <see cref="IRelayCommand{T1, T2}"/> that runs a delegate, optionally gated by a predicate.
    /// </summary>
    /// <typeparam name="T1">The first command parameter type.</typeparam>
    /// <typeparam name="T2">The second command parameter type.</typeparam>
    public sealed class RelayCommand<T1, T2> : IRelayCommand<T1, T2>
    {
        /// <inheritdoc/>
        public event Action<IRelayCommand<T1, T2>>? CanExecuteChanged;
        
        private static RelayCommand<T1, T2>? _empty;
        private static RelayCommand<T1, T2>? _emptyExecution;
        private readonly Action<T1?, T2?> _execute;
        private readonly Func<T1?, T2?, bool>? _canExecute;

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute(T1, T2)"/> returns
        /// <see langword="false"/>.
        /// </summary>
        public static RelayCommand<T1, T2> Empty => _empty ??= new RelayCommand<T1, T2>();

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute(T1, T2)"/> returns
        /// <see langword="true"/>.
        /// </summary>
        public static RelayCommand<T1, T2> EmptyExecution => _emptyExecution ??= new RelayCommand<T1, T2>(true);
        
        /// <summary>
        /// Creates a command that can always execute.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action<T1?, T2?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action<T1?, T2?> execute, Func<T1?, T2?, bool>? canExecute)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private RelayCommand(bool value = false)
        {
            if (value) _execute = (_, _) => { };
            _canExecute = (_, _) => value;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <returns>The result of the predicate; <see langword="true"/> when there is none.</returns>
        public bool CanExecute(T1? param1, T2? param2)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                return _canExecute?.Invoke(param1, param2) ?? true;
            }
        }

        /// <summary>
        /// Runs the action when <see cref="CanExecute(T1, T2)"/> returns <see langword="true"/>.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        public void Execute(T1? param1, T2? param2)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                if (CanExecute(param1, param2)) 
                    _execute.Invoke(param1, param2);
            }
        }
        
        /// <inheritdoc/>
        public void NotifyCanExecuteChanged()
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                CanExecuteChanged?.Invoke(this);
            }
        }
    }
    
    /// <summary>
    /// <see cref="IRelayCommand{T1, T2, T3}"/> that runs a delegate, optionally gated by a predicate.
    /// </summary>
    /// <typeparam name="T1">The first command parameter type.</typeparam>
    /// <typeparam name="T2">The second command parameter type.</typeparam>
    /// <typeparam name="T3">The third command parameter type.</typeparam>
    public sealed class RelayCommand<T1, T2, T3> : IRelayCommand<T1, T2, T3>
    {
        /// <inheritdoc/>
        public event Action<IRelayCommand<T1, T2, T3>>? CanExecuteChanged;
        
        private static RelayCommand<T1, T2, T3>? _empty;
        private static RelayCommand<T1, T2, T3>? _emptyExecution;
        private readonly Action<T1?, T2?, T3?> _execute;
        private readonly Func<T1?, T2?, T3?, bool>? _canExecute;

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute(T1, T2, T3)"/> returns
        /// <see langword="false"/>.
        /// </summary>
        public static RelayCommand<T1, T2, T3> Empty => _empty ??= new RelayCommand<T1, T2, T3>();

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute(T1, T2, T3)"/> returns
        /// <see langword="true"/>.
        /// </summary>
        public static RelayCommand<T1, T2, T3> EmptyExecution => _emptyExecution ??= new RelayCommand<T1, T2, T3>(true);
        
        /// <summary>
        /// Creates a command that can always execute.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?> execute, Func<T1?, T2?, T3?, bool>? canExecute)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private RelayCommand(bool value = false)
        {
            if (value) _execute = (_, _, _) => { };
            _canExecute = (_, _, _) => value;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <returns>The result of the predicate; <see langword="true"/> when there is none.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                return _canExecute?.Invoke(param1, param2, param3) ?? true;
            }
        }

        /// <summary>
        /// Runs the action when <see cref="CanExecute(T1, T2, T3)"/> returns <see langword="true"/>.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        public void Execute(T1? param1, T2? param2, T3? param3)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                if (CanExecute(param1, param2, param3)) 
                    _execute.Invoke(param1, param2, param3);
            }
        }
        
        /// <inheritdoc/>
        public void NotifyCanExecuteChanged()
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                CanExecuteChanged?.Invoke(this);
            }
        }
    }
    
    /// <summary>
    /// <see cref="IRelayCommand{T1, T2, T3, T4}"/> that runs a delegate, optionally gated by a predicate.
    /// </summary>
    /// <typeparam name="T1">The first command parameter type.</typeparam>
    /// <typeparam name="T2">The second command parameter type.</typeparam>
    /// <typeparam name="T3">The third command parameter type.</typeparam>
    /// <typeparam name="T4">The fourth command parameter type.</typeparam>
    public sealed class RelayCommand<T1, T2, T3, T4> : IRelayCommand<T1, T2, T3, T4>
    {
        /// <inheritdoc/>
        public event Action<IRelayCommand<T1, T2, T3, T4>>? CanExecuteChanged;
        
        private static RelayCommand<T1, T2, T3, T4>? _empty;
        private static RelayCommand<T1, T2, T3, T4>? _emptyExecution;
        private readonly Action<T1?, T2?, T3?, T4?> _execute;
        private readonly Func<T1?, T2?, T3?, T4?, bool>? _canExecute;

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute(T1, T2, T3, T4)"/> returns
        /// <see langword="false"/>.
        /// </summary>
        public static RelayCommand<T1, T2, T3, T4> Empty => _empty ??= new RelayCommand<T1, T2, T3, T4>();

        /// <summary>
        /// Gets a shared command that does nothing and whose <see cref="CanExecute(T1, T2, T3, T4)"/> returns
        /// <see langword="true"/>.
        /// </summary>
        public static RelayCommand<T1, T2, T3, T4> EmptyExecution => _emptyExecution ??= new RelayCommand<T1, T2, T3, T4>(true);
        
        /// <summary>
        /// Creates a command that can always execute.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?, T4?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action<T1?, T2?, T3?, T4?> execute, Func<T1?, T2?, T3?, T4?, bool>? canExecute)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private RelayCommand(bool value = false)
        {
            if (value) _execute = (_, _, _, _) => { };
            _canExecute = (_, _, _, _) => value;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <param name="param4">The fourth command parameter.</param>
        /// <returns>The result of the predicate; <see langword="true"/> when there is none.</returns>
        public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                return _canExecute?.Invoke(param1, param2, param3, param4) ?? true;
            }
        }

        /// <summary>
        /// Runs the action when <see cref="CanExecute(T1, T2, T3, T4)"/> returns <see langword="true"/>.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <param name="param4">The fourth command parameter.</param>
        public void Execute(T1? param1, T2? param2, T3? param3, T4? param4)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                if (CanExecute(param1, param2, param3, param4)) 
                    _execute.Invoke(param1, param2, param3, param4);
            }
        }
        
        /// <inheritdoc/>
        public void NotifyCanExecuteChanged()
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                CanExecuteChanged?.Invoke(this);
            }
        }
    }
}
