using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class RelayCommandExtensions
    {
        #region CreateCommandWithoutParameters Methods
        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="command">The command to wrap.</param>
        /// <param name="param">The parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParameters<T>(this IRelayCommand<T> command, T param) =>
            new RelayCommand(() => command.Execute(param), () => command.CanExecute(param));

        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param1"/> and <paramref name="param2"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T1, T2}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="command">The command to wrap.</param>
        /// <param name="param1">The first parameter passed to <paramref name="command"/>.</param>
        /// <param name="param2">The second parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParameters<T1, T2>(this IRelayCommand<T1, T2> command, T1 param1, T2 param2) =>
            new RelayCommand(() => command.Execute(param1, param2), () => command.CanExecute(param1, param2));

        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param1"/>, <paramref name="param2"/> and <paramref name="param3"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T1, T2, T3}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="command">The command to wrap.</param>
        /// <param name="param1">The first parameter passed to <paramref name="command"/>.</param>
        /// <param name="param2">The second parameter passed to <paramref name="command"/>.</param>
        /// <param name="param3">The third parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3>(this IRelayCommand<T1, T2, T3> command, T1 param1, T2 param2, T3 param3) =>
            new RelayCommand(() => command.Execute(param1, param2, param3), () => command.CanExecute(param1, param2, param3));

        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param1"/>, <paramref name="param2"/>, <paramref name="param3"/> and <paramref name="param4"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T1, T2, T3, T4}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="command">The command to wrap.</param>
        /// <param name="param1">The first parameter passed to <paramref name="command"/>.</param>
        /// <param name="param2">The second parameter passed to <paramref name="command"/>.</param>
        /// <param name="param3">The third parameter passed to <paramref name="command"/>.</param>
        /// <param name="param4">The fourth parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4> command, T1 param1, T2 param2, T3 param3, T4 param4) =>
            new RelayCommand(() => command.Execute(param1, param2, param3, param4), () => command.CanExecute(param1, param2, param3, param4));
        #endregion
        
        #region CreateCommandWithoutParameters Or Get Empty RelayCommand<T> Methods
        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param"/>, or returns
        /// <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="command">The command to wrap, or <see langword="null"/>.</param>
        /// <param name="param">The parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command; <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T>(this IRelayCommand<T>? command, T param) => command is not null
            ? new RelayCommand(() => command.Execute(param), () => command.CanExecute(param))
            : RelayCommand.Empty;

        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param"/>, or returns
        /// <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="command">The command to wrap, or <see langword="null"/>.</param>
        /// <param name="param">The parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command; <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T>(this IRelayCommand<T>? command, T param) => command is not null
            ? new RelayCommand(() => command.Execute(param), () => command.CanExecute(param))
            : RelayCommand.EmptyExecution;
        #endregion

        #region CreateCommandWithoutParameters Or Get Empty RelayCommand<T1, T2> Methods
        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param1"/> and <paramref name="param2"/>, or returns
        /// <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T1, T2}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="command">The command to wrap, or <see langword="null"/>.</param>
        /// <param name="param1">The first parameter passed to <paramref name="command"/>.</param>
        /// <param name="param2">The second parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command; <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2>(this IRelayCommand<T1, T2>? command, T1 param1, T2 param2) => command is not null
            ? new RelayCommand(() => command.Execute(param1, param2), () => command.CanExecute(param1, param2))
            : RelayCommand.Empty;

        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param1"/> and <paramref name="param2"/>, or returns
        /// <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T1, T2}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="command">The command to wrap, or <see langword="null"/>.</param>
        /// <param name="param1">The first parameter passed to <paramref name="command"/>.</param>
        /// <param name="param2">The second parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command; <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2>(this IRelayCommand<T1, T2>? command, T1 param1, T2 param2) => command is not null
            ? new RelayCommand(() => command.Execute(param1, param2), () => command.CanExecute(param1, param2))
            : RelayCommand.EmptyExecution;
        #endregion

        #region CreateCommandWithoutParameters Or Get Empty RelayCommand<T1, T2, T3> Methods
        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param1"/>, <paramref name="param2"/> and <paramref name="param3"/>, or returns
        /// <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T1, T2, T3}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="command">The command to wrap, or <see langword="null"/>.</param>
        /// <param name="param1">The first parameter passed to <paramref name="command"/>.</param>
        /// <param name="param2">The second parameter passed to <paramref name="command"/>.</param>
        /// <param name="param3">The third parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command; <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3>(this IRelayCommand<T1, T2, T3>? command, T1 param1, T2 param2, T3 param3) => command is not null
            ? new RelayCommand(() => command.Execute(param1, param2, param3), () => command.CanExecute(param1, param2, param3))
            : RelayCommand.Empty;

        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param1"/>, <paramref name="param2"/> and <paramref name="param3"/>, or returns
        /// <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T1, T2, T3}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="command">The command to wrap, or <see langword="null"/>.</param>
        /// <param name="param1">The first parameter passed to <paramref name="command"/>.</param>
        /// <param name="param2">The second parameter passed to <paramref name="command"/>.</param>
        /// <param name="param3">The third parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command; <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2, T3>(this IRelayCommand<T1, T2, T3>? command, T1 param1, T2 param2, T3 param3) => command is not null
            ? new RelayCommand(() => command.Execute(param1, param2, param3), () => command.CanExecute(param1, param2, param3))
            : RelayCommand.EmptyExecution;
        #endregion
        
        #region CreateCommandWithoutParameters Or Get Empty RelayCommand<T1, T2, T3, T4> Methods
        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param1"/>, <paramref name="param2"/>, <paramref name="param3"/> and <paramref name="param4"/>, or returns
        /// <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T1, T2, T3, T4}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="command">The command to wrap, or <see langword="null"/>.</param>
        /// <param name="param1">The first parameter passed to <paramref name="command"/>.</param>
        /// <param name="param2">The second parameter passed to <paramref name="command"/>.</param>
        /// <param name="param3">The third parameter passed to <paramref name="command"/>.</param>
        /// <param name="param4">The fourth parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command; <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4>? command, T1 param1, T2 param2, T3 param3, T4 param4) => command is not null
            ? new RelayCommand(() => command.Execute(param1, param2, param3, param4), () => command.CanExecute(param1, param2, param3, param4))
            : RelayCommand.Empty;

        /// <summary>
        /// Creates an <see cref="IRelayCommand"/> that runs <paramref name="command"/> with <paramref name="param1"/>, <paramref name="param2"/>, <paramref name="param3"/> and <paramref name="param4"/>, or returns
        /// <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.
        /// </summary>
        /// <remarks>
        /// The new command does not forward <see cref="IRelayCommand{T1, T2, T3, T4}.CanExecuteChanged"/> of <paramref name="command"/>.
        /// </remarks>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="command">The command to wrap, or <see langword="null"/>.</param>
        /// <param name="param1">The first parameter passed to <paramref name="command"/>.</param>
        /// <param name="param2">The second parameter passed to <paramref name="command"/>.</param>
        /// <param name="param3">The third parameter passed to <paramref name="command"/>.</param>
        /// <param name="param4">The fourth parameter passed to <paramref name="command"/>.</param>
        /// <returns>The parameterless command; <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4>? command, T1 param1, T2 param2, T3 param3, T4 param4) => command is not null
            ? new RelayCommand(() => command.Execute(param1, param2, param3, param4), () => command.CanExecute(param1, param2, param3, param4))
            : RelayCommand.EmptyExecution;
        #endregion
    }
}
