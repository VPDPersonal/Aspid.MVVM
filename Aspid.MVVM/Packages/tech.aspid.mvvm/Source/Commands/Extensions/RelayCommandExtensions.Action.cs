using System;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class RelayCommandExtensions
    {
        #region CreateCommand Methods
        /// <summary>
        /// Creates a <see cref="RelayCommand"/> that runs <paramref name="execute"/>.
        /// </summary>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand CreateCommand(
            this Action execute,
            Func<bool>? canExecute = null) =>
            new(execute, canExecute);

        /// <summary>
        /// Creates a <see cref="RelayCommand{T}"/> that runs <paramref name="execute"/>.
        /// </summary>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T?> CreateCommand<T>(
            this Action<T?> execute,
            Func<T?, bool>? canExecute = null) =>
            new(execute, canExecute);

        /// <summary>
        /// Creates a <see cref="RelayCommand{T1, T2}"/> that runs <paramref name="execute"/>.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T1?, T2?> CreateCommand<T1, T2>(
            this Action<T1?, T2?> execute,
            Func<T1?, T2?, bool>? canExecute = null) =>
            new(execute, canExecute);

        /// <summary>
        /// Creates a <see cref="RelayCommand{T1, T2, T3}"/> that runs <paramref name="execute"/>.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T1?, T2?, T3?> CreateCommand<T1, T2, T3>(
            this Action<T1?, T2?, T3?> execute,
            Func<T1?, T2?, T3?, bool>? canExecute = null) =>
            new(execute, canExecute);

        /// <summary>
        /// Creates a <see cref="RelayCommand{T1, T2, T3, T4}"/> that runs <paramref name="execute"/>.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="execute">The action to run.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommand<T1, T2, T3, T4>(
            this Action<T1?, T2?, T3?, T4?> execute,
            Func<T1?, T2?, T3?, T4?, bool>? canExecute = null) =>
            new(execute, canExecute);
        #endregion

        #region CreateCommand Or Get Empty RelayCommand Methods
        /// <summary>
        /// Creates a <see cref="RelayCommand"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand.Empty"/> when it is <see langword="null"/>.
        /// </summary>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand.Empty"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand CreateCommandOrEmpty(
            this Action? execute, 
            Func<bool>? canExecute = null) =>
            execute is not null ? new RelayCommand(execute, canExecute) : RelayCommand.Empty;
        
        /// <summary>
        /// Creates a <see cref="RelayCommand"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand.EmptyExecution"/> when it is <see langword="null"/>.
        /// </summary>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand.EmptyExecution"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand CreateCommandOrEmptyExecution(
            this Action? execute, 
            Func<bool>? canExecute = null) =>
            execute is not null ? new RelayCommand(execute, canExecute) : RelayCommand.EmptyExecution;
        #endregion

        #region CreateCommand Or Get Empty RelayCommand<T> Methods
        /// <summary>
        /// Creates a <see cref="RelayCommand{T}"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand{T}.Empty"/> when it is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand{T}.Empty"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T?> CreateCommandOrEmpty<T>(
            this Action<T?>? execute,
            Func<T?, bool>? canExecute = null) =>
            execute is not null ? new RelayCommand<T?>(execute, canExecute) : RelayCommand<T?>.Empty;

        /// <summary>
        /// Creates a <see cref="RelayCommand{T}"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand{T}.EmptyExecution"/> when it is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand{T}.EmptyExecution"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T?> CreateCommandOrEmptyExecution<T>(
            this Action<T?>? execute,
            Func<T?, bool>? canExecute = null) =>
            execute is not null ? new RelayCommand<T?>(execute, canExecute) : RelayCommand<T?>.EmptyExecution;
        #endregion

        #region CreateCommand Or Get Empty RelayCommand<T1, T2> Methods
        /// <summary>
        /// Creates a <see cref="RelayCommand{T1, T2}"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand{T1, T2}.Empty"/> when it is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand{T1, T2}.Empty"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T1?, T2?> CreateCommandOrEmpty<T1, T2>(
            this Action<T1?, T2?>? execute,
            Func<T1?, T2?, bool>? canExecute = null) =>
            execute is not null ? new RelayCommand<T1?, T2?>(execute, canExecute) : RelayCommand<T1?, T2?>.Empty;

        /// <summary>
        /// Creates a <see cref="RelayCommand{T1, T2}"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand{T1, T2}.EmptyExecution"/> when it is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand{T1, T2}.EmptyExecution"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T1?, T2?> CreateCommandOrEmptyExecution<T1, T2>(
            this Action<T1?, T2?>? execute,
            Func<T1?, T2?, bool>? canExecute = null) =>
            execute is not null ? new RelayCommand<T1?, T2?>(execute, canExecute) : RelayCommand<T1?, T2?>.EmptyExecution;
        #endregion

        #region CreateCommand Or Get Empty RelayCommand<T1, T2, T3> Methods
        /// <summary>
        /// Creates a <see cref="RelayCommand{T1, T2, T3}"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand{T1, T2, T3}.Empty"/> when it is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand{T1, T2, T3}.Empty"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T1?, T2?, T3?> CreateCommandOrEmpty<T1, T2, T3>(
            this Action<T1?, T2?, T3?>? execute,
            Func<T1?, T2?, T3?, bool>? canExecute = null) =>
            execute is not null ? new RelayCommand<T1?, T2?, T3?>(execute, canExecute) : RelayCommand<T1?, T2?, T3?>.Empty;

        /// <summary>
        /// Creates a <see cref="RelayCommand{T1, T2, T3}"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand{T1, T2, T3}.EmptyExecution"/> when it is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand{T1, T2, T3}.EmptyExecution"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T1?, T2?, T3?> CreateCommandOrEmptyExecution<T1, T2, T3>(
            this Action<T1?, T2?, T3?>? execute,
            Func<T1?, T2?, T3?, bool>? canExecute = null) =>
            execute is not null ? new RelayCommand<T1?, T2?, T3?>(execute, canExecute) : RelayCommand<T1?, T2?, T3?>.EmptyExecution;
        #endregion

        #region CreateCommand Or Get Empty RelayCommand<T1, T2, T3, T4> Methods
        /// <summary>
        /// Creates a <see cref="RelayCommand{T1, T2, T3, T4}"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand{T1, T2, T3, T4}.Empty"/> when it is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand{T1, T2, T3, T4}.Empty"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommandOrEmpty<T1, T2, T3, T4>(
            this Action<T1?, T2?, T3?, T4?>? execute,
            Func<T1?, T2?, T3?, T4?, bool>? canExecute = null) =>
            execute is not null ? new RelayCommand<T1?, T2?, T3?, T4?>(execute, canExecute) : RelayCommand<T1?, T2?, T3?, T4?>.Empty;

        /// <summary>
        /// Creates a <see cref="RelayCommand{T1, T2, T3, T4}"/> that runs <paramref name="execute"/>, or returns
        /// <see cref="RelayCommand{T1, T2, T3, T4}.EmptyExecution"/> when it is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="execute">The action to run, or <see langword="null"/>.</param>
        /// <param name="canExecute">The predicate that allows execution; <see langword="null"/> to always allow it.</param>
        /// <returns>The new command; <see cref="RelayCommand{T1, T2, T3, T4}.EmptyExecution"/> when <paramref name="execute"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommandOrEmptyExecution<T1, T2, T3, T4>(
            this Action<T1?, T2?, T3?, T4?>? execute,
            Func<T1?, T2?, T3?, T4?, bool>? canExecute = null) =>
            execute is not null ? new RelayCommand<T1?, T2?, T3?, T4?>(execute, canExecute) : RelayCommand<T1?, T2?, T3?, T4?>.EmptyExecution;
        #endregion
    }
}
