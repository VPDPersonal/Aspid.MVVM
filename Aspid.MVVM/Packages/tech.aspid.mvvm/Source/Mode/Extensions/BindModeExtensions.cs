using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="BindMode"/>.
    /// </summary>
    public static partial class BindModeExtensions
    {
        /// <summary>
        /// Determines whether the mode can be used for binding.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns><see langword="true"/> for a declared mode other than <see cref="BindMode.None"/>; otherwise, <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValid(this BindMode mode) =>
            mode is BindMode.OneWay or BindMode.TwoWay or BindMode.OneTime or BindMode.OneWayToSource;

        /// <summary>
        /// Determines whether the mode sends values only from the ViewModel to the View.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns><see langword="true"/> for <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>; otherwise, <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsForwardOnly(this BindMode mode) =>
            mode is BindMode.OneWay or BindMode.OneTime;

        /// <summary>
        /// Determines whether the mode sends values from the ViewModel to the View.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns><see langword="true"/> for <see cref="BindMode.OneWay"/>, <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneTime"/>; otherwise, <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IncludesForward(this BindMode mode) =>
            mode is BindMode.OneWay or BindMode.TwoWay or BindMode.OneTime;

        /// <summary>
        /// Determines whether the mode sends values from the View to the ViewModel.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns><see langword="true"/> for <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>; otherwise, <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IncludesReverse(this BindMode mode) =>
            mode is BindMode.TwoWay or BindMode.OneWayToSource;
    }
}
