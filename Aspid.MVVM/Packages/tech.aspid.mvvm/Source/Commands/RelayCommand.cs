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
    /// <remarks>Exceptions from delegates and event handlers propagate to the caller.</remarks>
    public sealed class RelayCommand : IRelayCommand
    {
        private static RelayCommand? _empty;
        private static RelayCommand? _emptyExecution;

        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        /// <inheritdoc/>
        public event Action<IRelayCommand>? CanExecuteChanged;

        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The execution predicate; <see langword="null"/> to always allow execution.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(
            Action execute,
            Func<bool>? canExecute = null)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        private RelayCommand(bool canExecute)
        {
            _execute = static () => { };
            _canExecute = canExecute
                ? static () => true
                : static () => false;
        }

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="false"/> from <see cref="CanExecute()"/>.
        /// </summary>
        public static RelayCommand Empty => _empty ??= new RelayCommand(canExecute: false);

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="true"/> from <see cref="CanExecute()"/>.
        /// </summary>
        public static RelayCommand EmptyExecution => _emptyExecution ??= new RelayCommand(canExecute: true);

        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <returns>The predicate result; <see langword="true"/> when no predicate was supplied.</returns>
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
    /// <remarks>Exceptions from delegates and event handlers propagate to the caller.</remarks>
    /// <typeparam name="T">The command parameter type.</typeparam>
    public class RelayCommand<T> : IRelayCommand<T>
    {
        private static RelayCommand<T>? _empty;
        private static RelayCommand<T>? _emptyExecution;

        private readonly Action<T?> _execute;
        private readonly Func<T?, bool>? _canExecute;

        /// <inheritdoc/>
        public event Action<IRelayCommand<T>>? CanExecuteChanged;

        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The execution predicate; <see langword="null"/> to always allow execution.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(
            Action<T?> execute,
            Func<T?, bool>? canExecute = null)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        private RelayCommand(bool canExecute)
        {
            _execute = static _ => { };
            _canExecute = canExecute
                ? static _ => true
                : static _ => false;
        }

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="false"/> from <see cref="CanExecute(T)"/>.
        /// </summary>
        public static RelayCommand<T> Empty => _empty ??= new RelayCommand<T>(canExecute: false);

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="true"/> from <see cref="CanExecute(T)"/>.
        /// </summary>
        public static RelayCommand<T> EmptyExecution => _emptyExecution ??= new RelayCommand<T>(canExecute: true);

        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <param name="param">The command parameter.</param>
        /// <returns>The predicate result; <see langword="true"/> when no predicate was supplied.</returns>
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
    /// <remarks>Exceptions from delegates and event handlers propagate to the caller.</remarks>
    /// <typeparam name="T1">The first command parameter type.</typeparam>
    /// <typeparam name="T2">The second command parameter type.</typeparam>
    public class RelayCommand<T1, T2> : IRelayCommand<T1, T2>
    {
        private static RelayCommand<T1, T2>? _empty;
        private static RelayCommand<T1, T2>? _emptyExecution;

        private readonly Action<T1?, T2?> _execute;
        private readonly Func<T1?, T2?, bool>? _canExecute;

        /// <inheritdoc/>
        public event Action<IRelayCommand<T1, T2>>? CanExecuteChanged;

        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The execution predicate; <see langword="null"/> to always allow execution.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(
            Action<T1?, T2?> execute,
            Func<T1?, T2?, bool>? canExecute = null)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        private RelayCommand(bool canExecute)
        {
            _execute = static (_, _) => { };
            _canExecute = canExecute
                ? static (_, _) => true
                : static (_, _) => false;
        }

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="false"/> from <see cref="CanExecute(T1, T2)"/>.
        /// </summary>
        public static RelayCommand<T1, T2> Empty => _empty ??= new RelayCommand<T1, T2>(canExecute: false);

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="true"/> from <see cref="CanExecute(T1, T2)"/>.
        /// </summary>
        public static RelayCommand<T1, T2> EmptyExecution => _emptyExecution ??= new RelayCommand<T1, T2>(canExecute: true);

        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <returns>The predicate result; <see langword="true"/> when no predicate was supplied.</returns>
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
    /// <remarks>Exceptions from delegates and event handlers propagate to the caller.</remarks>
    /// <typeparam name="T1">The first command parameter type.</typeparam>
    /// <typeparam name="T2">The second command parameter type.</typeparam>
    /// <typeparam name="T3">The third command parameter type.</typeparam>
    public class RelayCommand<T1, T2, T3> : IRelayCommand<T1, T2, T3>
    {
        private static RelayCommand<T1, T2, T3>? _empty;
        private static RelayCommand<T1, T2, T3>? _emptyExecution;

        private readonly Action<T1?, T2?, T3?> _execute;
        private readonly Func<T1?, T2?, T3?, bool>? _canExecute;

        /// <inheritdoc/>
        public event Action<IRelayCommand<T1, T2, T3>>? CanExecuteChanged;

        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The execution predicate; <see langword="null"/> to always allow execution.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(
            Action<T1?, T2?, T3?> execute,
            Func<T1?, T2?, T3?, bool>? canExecute = null)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        private RelayCommand(bool canExecute)
        {
            _execute = static (_, _, _) => { };
            _canExecute = canExecute
                ? static (_, _, _) => true
                : static (_, _, _) => false;
        }

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="false"/> from <see cref="CanExecute(T1, T2, T3)"/>.
        /// </summary>
        public static RelayCommand<T1, T2, T3> Empty => _empty ??= new RelayCommand<T1, T2, T3>(canExecute: false);

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="true"/> from <see cref="CanExecute(T1, T2, T3)"/>.
        /// </summary>
        public static RelayCommand<T1, T2, T3> EmptyExecution => _emptyExecution ??= new RelayCommand<T1, T2, T3>(canExecute: true);

        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <returns>The predicate result; <see langword="true"/> when no predicate was supplied.</returns>
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
    /// <remarks>Exceptions from delegates and event handlers propagate to the caller.</remarks>
    /// <typeparam name="T1">The first command parameter type.</typeparam>
    /// <typeparam name="T2">The second command parameter type.</typeparam>
    /// <typeparam name="T3">The third command parameter type.</typeparam>
    /// <typeparam name="T4">The fourth command parameter type.</typeparam>
    public class RelayCommand<T1, T2, T3, T4> : IRelayCommand<T1, T2, T3, T4>
    {
        private static RelayCommand<T1, T2, T3, T4>? _empty;
        private static RelayCommand<T1, T2, T3, T4>? _emptyExecution;

        private readonly Action<T1?, T2?, T3?, T4?> _execute;
        private readonly Func<T1?, T2?, T3?, T4?, bool>? _canExecute;

        /// <inheritdoc/>
        public event Action<IRelayCommand<T1, T2, T3, T4>>? CanExecuteChanged;

        /// <summary>
        /// Creates a command gated by <paramref name="canExecute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The execution predicate; <see langword="null"/> to always allow execution.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(
            Action<T1?, T2?, T3?, T4?> execute,
            Func<T1?, T2?, T3?, T4?, bool>? canExecute = null)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        private RelayCommand(bool canExecute)
        {
            _execute = static (_, _, _, _) => { };
            _canExecute = canExecute
                ? static (_, _, _, _) => true
                : static (_, _, _, _) => false;
        }

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="false"/> from <see cref="CanExecute(T1, T2, T3, T4)"/>.
        /// </summary>
        public static RelayCommand<T1, T2, T3, T4> Empty => _empty ??= new RelayCommand<T1, T2, T3, T4>(canExecute: false);

        /// <summary>
        /// Gets a shared command that does nothing and always returns <see langword="true"/> from <see cref="CanExecute(T1, T2, T3, T4)"/>.
        /// </summary>
        public static RelayCommand<T1, T2, T3, T4> EmptyExecution => _emptyExecution ??= new RelayCommand<T1, T2, T3, T4>(canExecute: true);

        /// <summary>
        /// Determines whether the command can execute by calling its predicate.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <param name="param4">The fourth command parameter.</param>
        /// <returns>The predicate result; <see langword="true"/> when no predicate was supplied.</returns>
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
