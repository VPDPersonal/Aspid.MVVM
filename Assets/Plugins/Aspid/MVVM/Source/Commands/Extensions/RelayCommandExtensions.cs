using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class RelayCommandExtensions
    {
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty no-op command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand GetSelfOrEmpty(this IRelayCommand? command) =>
            command ?? RelayCommand.Empty;

        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty no-op command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T?> GetSelfOrEmpty<T>(this IRelayCommand<T?>? command) =>
            command ?? RelayCommand<T?>.Empty;

        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty no-op command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T1?, T2?> GetSelfOrEmpty<T1, T2>(this IRelayCommand<T1?, T2?>? command) =>
            command ?? RelayCommand<T1?, T2?>.Empty;

        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty no-op command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T1?, T2?, T3?> GetSelfOrEmpty<T1, T2, T3>(
            this IRelayCommand<T1?, T2?, T3?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?>.Empty;

        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty no-op command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T1?, T2?, T3?, T4?> GetSelfOrEmpty<T1, T2, T3, T4>(
            this IRelayCommand<T1?, T2?, T3?, T4?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?, T4?>.Empty;
    }
}