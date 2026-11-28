// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for creating relay commands, binding arguments and supplying empty commands.
    /// </summary>
    /// <remarks>
    /// Parameter-bound adapters forward notifications while listeners are attached and delegate execution to the source command.
    /// </remarks>
    public static partial class RelayCommandExtensions
    {
        /// <summary>
        /// Substitutes <see cref="RelayCommand.Empty"/> for a <see langword="null"/> command.
        /// </summary>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand.Empty"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand GetSelfOrEmpty(this IRelayCommand? command) =>
            command ?? RelayCommand.Empty;

        /// <summary>
        /// Substitutes <see cref="RelayCommand.EmptyExecution"/> for a <see langword="null"/> command.
        /// </summary>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand.EmptyExecution"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand GetSelfOrEmptyExecution(this IRelayCommand? command) =>
            command ?? RelayCommand.EmptyExecution;

        /// <summary>
        /// Substitutes <see cref="RelayCommand{T}.Empty"/> for a <see langword="null"/> command.
        /// </summary>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand{T}.Empty"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand<T?> GetSelfOrEmpty<T>(this IRelayCommand<T?>? command) =>
            command ?? RelayCommand<T?>.Empty;

        /// <summary>
        /// Substitutes <see cref="RelayCommand{T}.EmptyExecution"/> for a <see langword="null"/> command.
        /// </summary>
        /// <typeparam name="T">The command parameter type.</typeparam>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand{T}.EmptyExecution"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand<T?> GetSelfOrEmptyExecution<T>(this IRelayCommand<T?>? command) =>
            command ?? RelayCommand<T?>.EmptyExecution;

        /// <summary>
        /// Substitutes <see cref="RelayCommand{T1, T2}.Empty"/> for a <see langword="null"/> command.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand{T1, T2}.Empty"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand<T1?, T2?> GetSelfOrEmpty<T1, T2>(this IRelayCommand<T1?, T2?>? command) =>
            command ?? RelayCommand<T1?, T2?>.Empty;

        /// <summary>
        /// Substitutes <see cref="RelayCommand{T1, T2}.EmptyExecution"/> for a <see langword="null"/> command.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand{T1, T2}.EmptyExecution"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand<T1?, T2?> GetSelfOrEmptyExecution<T1, T2>(this IRelayCommand<T1?, T2?>? command) =>
            command ?? RelayCommand<T1?, T2?>.EmptyExecution;

        /// <summary>
        /// Substitutes <see cref="RelayCommand{T1, T2, T3}.Empty"/> for a <see langword="null"/> command.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand{T1, T2, T3}.Empty"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand<T1?, T2?, T3?> GetSelfOrEmpty<T1, T2, T3>(
            this IRelayCommand<T1?, T2?, T3?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?>.Empty;

        /// <summary>
        /// Substitutes <see cref="RelayCommand{T1, T2, T3}.EmptyExecution"/> for a <see langword="null"/> command.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand{T1, T2, T3}.EmptyExecution"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand<T1?, T2?, T3?> GetSelfOrEmptyExecution<T1, T2, T3>(
            this IRelayCommand<T1?, T2?, T3?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?>.EmptyExecution;

        /// <summary>
        /// Substitutes <see cref="RelayCommand{T1, T2, T3, T4}.Empty"/> for a <see langword="null"/> command.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand{T1, T2, T3, T4}.Empty"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand<T1?, T2?, T3?, T4?> GetSelfOrEmpty<T1, T2, T3, T4>(
            this IRelayCommand<T1?, T2?, T3?, T4?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?, T4?>.Empty;

        /// <summary>
        /// Substitutes <see cref="RelayCommand{T1, T2, T3, T4}.EmptyExecution"/> for a <see langword="null"/> command.
        /// </summary>
        /// <typeparam name="T1">The first command parameter type.</typeparam>
        /// <typeparam name="T2">The second command parameter type.</typeparam>
        /// <typeparam name="T3">The third command parameter type.</typeparam>
        /// <typeparam name="T4">The fourth command parameter type.</typeparam>
        /// <param name="command">The command, or <see langword="null"/>.</param>
        /// <returns><paramref name="command"/>, or <see cref="RelayCommand{T1, T2, T3, T4}.EmptyExecution"/> if it is <see langword="null"/>.</returns>
        public static IRelayCommand<T1?, T2?, T3?, T4?> GetSelfOrEmptyExecution<T1, T2, T3, T4>(
            this IRelayCommand<T1?, T2?, T3?, T4?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?, T4?>.EmptyExecution;
    }
}
