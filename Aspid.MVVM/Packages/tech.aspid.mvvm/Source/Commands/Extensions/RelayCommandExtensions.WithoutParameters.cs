using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class RelayCommandExtensions
    {
        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="command">The command to bind.</param>
        /// <param name="param">The bound argument.</param>
        /// <returns>The parameterless adapter.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        public static IRelayCommand CreateCommandWithoutParameters<T>(
            this IRelayCommand<T> command, 
            T? param) =>
            new BoundRelayCommand<T>(command, param);

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="command">The command to bind; <see langword="null"/> for the fallback.</param>
        /// <param name="param">The bound argument.</param>
        /// <returns>The adapter; <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T>(
            this IRelayCommand<T>? command,
            T? param) => command is not null
            ? command.CreateCommandWithoutParameters(param)
            : RelayCommand.Empty;

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="command">The command to bind; <see langword="null"/> for the fallback.</param>
        /// <param name="param">The bound argument.</param>
        /// <returns>The adapter; <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T>(
            this IRelayCommand<T>? command,
            T? param) => command is not null
            ? command.CreateCommandWithoutParameters(param)
            : RelayCommand.EmptyExecution;

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="command">The command to bind.</param>
        /// <param name="param1">The first bound argument.</param>
        /// <param name="param2">The second bound argument.</param>
        /// <returns>The parameterless adapter.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        public static IRelayCommand CreateCommandWithoutParameters<T1, T2>(
            this IRelayCommand<T1, T2> command,
            T1? param1,
            T2? param2) =>
            new BoundRelayCommand<T1, T2>(command, param1, param2);

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="command">The command to bind; <see langword="null"/> for the fallback.</param>
        /// <param name="param1">The first bound argument.</param>
        /// <param name="param2">The second bound argument.</param>
        /// <returns>The adapter; <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2>(
            this IRelayCommand<T1, T2>? command,
            T1? param1,
            T2? param2) => command is not null
            ? command.CreateCommandWithoutParameters(param1, param2)
            : RelayCommand.Empty;

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="command">The command to bind; <see langword="null"/> for the fallback.</param>
        /// <param name="param1">The first bound argument.</param>
        /// <param name="param2">The second bound argument.</param>
        /// <returns>The adapter; <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2>(
            this IRelayCommand<T1, T2>? command,
            T1? param1,
            T2? param2) => command is not null
            ? command.CreateCommandWithoutParameters(param1, param2)
            : RelayCommand.EmptyExecution;

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="command">The command to bind.</param>
        /// <param name="param1">The first bound argument.</param>
        /// <param name="param2">The second bound argument.</param>
        /// <param name="param3">The third bound argument.</param>
        /// <returns>The parameterless adapter.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3>(
            this IRelayCommand<T1, T2, T3> command,
            T1? param1,
            T2? param2,
            T3? param3) =>
            new BoundRelayCommand<T1, T2, T3>(command, param1, param2, param3);

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="command">The command to bind; <see langword="null"/> for the fallback.</param>
        /// <param name="param1">The first bound argument.</param>
        /// <param name="param2">The second bound argument.</param>
        /// <param name="param3">The third bound argument.</param>
        /// <returns>The adapter; <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3>(
            this IRelayCommand<T1, T2, T3>? command,
            T1? param1,
            T2? param2,
            T3? param3) => command is not null
            ? command.CreateCommandWithoutParameters(param1, param2, param3)
            : RelayCommand.Empty;

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="command">The command to bind; <see langword="null"/> for the fallback.</param>
        /// <param name="param1">The first bound argument.</param>
        /// <param name="param2">The second bound argument.</param>
        /// <param name="param3">The third bound argument.</param>
        /// <returns>The adapter; <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2, T3>(
            this IRelayCommand<T1, T2, T3>? command,
            T1? param1,
            T2? param2,
            T3? param3) => command is not null
            ? command.CreateCommandWithoutParameters(param1, param2, param3)
            : RelayCommand.EmptyExecution;

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="command">The command to bind.</param>
        /// <param name="param1">The first bound argument.</param>
        /// <param name="param2">The second bound argument.</param>
        /// <param name="param3">The third bound argument.</param>
        /// <param name="param4">The fourth bound argument.</param>
        /// <returns>The parameterless adapter.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3, T4>(
            this IRelayCommand<T1, T2, T3, T4> command,
            T1? param1,
            T2? param2,
            T3? param3,
            T4? param4) =>
            new BoundRelayCommand<T1, T2, T3, T4>(command, param1, param2, param3, param4);

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="command">The command to bind; <see langword="null"/> for the fallback.</param>
        /// <param name="param1">The first bound argument.</param>
        /// <param name="param2">The second bound argument.</param>
        /// <param name="param3">The third bound argument.</param>
        /// <param name="param4">The fourth bound argument.</param>
        /// <returns>The adapter; <see cref="RelayCommand.Empty"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3, T4>(
            this IRelayCommand<T1, T2, T3, T4>? command,
            T1? param1,
            T2? param2,
            T3? param3,
            T4? param4) => command is not null
            ? command.CreateCommandWithoutParameters(param1, param2, param3, param4)
            : RelayCommand.Empty;

        /// <summary>
        /// Creates a parameterless command bound to the supplied arguments.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="command">The command to bind; <see langword="null"/> for the fallback.</param>
        /// <param name="param1">The first bound argument.</param>
        /// <param name="param2">The second bound argument.</param>
        /// <param name="param3">The third bound argument.</param>
        /// <param name="param4">The fourth bound argument.</param>
        /// <returns>The adapter; <see cref="RelayCommand.EmptyExecution"/> when <paramref name="command"/> is <see langword="null"/>.</returns>
        public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2, T3, T4>(
            this IRelayCommand<T1, T2, T3, T4>? command,
            T1? param1,
            T2? param2,
            T3? param3,
            T4? param4) => command is not null
            ? command.CreateCommandWithoutParameters(param1, param2, param3, param4)
            : RelayCommand.EmptyExecution;
    }
}
