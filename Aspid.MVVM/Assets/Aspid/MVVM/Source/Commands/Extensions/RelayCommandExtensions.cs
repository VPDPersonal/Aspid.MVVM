using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Extension methods for <see cref="IRelayCommand"/> and its generic variants.
    /// Provides helpers for null-safe fallback to empty commands and for creating commands from delegates.
    /// </summary>
    public static partial class RelayCommandExtensions
    {
        #region Get Empty RelayCommand Methods
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand GetSelfOrEmpty(this IRelayCommand? command) =>
            command ?? RelayCommand.Empty;
        
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty execution command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty execution command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand GetSelfOrEmptyExecution(this IRelayCommand? command) =>
            command ?? RelayCommand.EmptyExecution;
        #endregion

        #region Get Empty RelayCommand<T> Methods
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T?> GetSelfOrEmpty<T>(this IRelayCommand<T?>? command) =>
            command ?? RelayCommand<T?>.Empty;
        
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty execution command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty execution command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T?> GetSelfOrEmptyExecution<T>(this IRelayCommand<T?>? command) =>
            command ?? RelayCommand<T?>.EmptyExecution;
        #endregion

        #region Get Empty RelayCommand<T1, T2> Methods
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T1?, T2?> GetSelfOrEmpty<T1, T2>(this IRelayCommand<T1?, T2?>? command) =>
            command ?? RelayCommand<T1?, T2?>.Empty;
        
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty execution command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty execution command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T1?, T2?> GetSelfOrEmptyExecution<T1, T2>(this IRelayCommand<T1?, T2?>? command) =>
            command ?? RelayCommand<T1?, T2?>.EmptyExecution;
        #endregion

        #region Get Empty RelayCommand<T1, T2, T3> Methods
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T1?, T2?, T3?> GetSelfOrEmpty<T1, T2, T3>(
            this IRelayCommand<T1?, T2?, T3?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?>.Empty;

        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty execution command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty execution command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T1?, T2?, T3?> GetSelfOrEmptyExecution<T1, T2, T3>(
            this IRelayCommand<T1?, T2?, T3?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?>.EmptyExecution;
        #endregion
        
        #region Get Empty RelayCommand<T1, T2, T3, T4> Methods
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T1?, T2?, T3?, T4?> GetSelfOrEmpty<T1, T2, T3, T4>(
            this IRelayCommand<T1?, T2?, T3?, T4?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?, T4?>.Empty;
        
        /// <summary>
        /// Returns the command if it's not null; otherwise, returns an empty execution command.
        /// </summary>
        /// <param name="command">The command to check.</param>
        /// <returns>The original command if not null; otherwise, an empty execution command.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRelayCommand<T1?, T2?, T3?, T4?> GetSelfOrEmptyExecution<T1, T2, T3, T4>(
            this IRelayCommand<T1?, T2?, T3?, T4?>? command) =>
            command ?? RelayCommand<T1?, T2?, T3?, T4?>.EmptyExecution;
        #endregion
    }
}
