using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public static partial class BindModeExtensions
    {
        /// <summary>
        /// Returns true when the mode represents one-way styles: <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns>True if the mode is OneWay or OneTime; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this BindMode mode) =>
            mode is BindMode.OneWay or BindMode.OneTime;
        
        /// <summary>
        /// Returns true when the mode represents two-way styles: <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns>True if the mode is TwoWay or OneWayToSource; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsTwo(this BindMode mode) =>
            mode is BindMode.TwoWay or BindMode.OneWayToSource;
        
        /// <summary>
        /// Returns true when the mode is <see cref="BindMode.None"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns>True if the mode is None; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNone(this BindMode mode) =>
            mode is BindMode.None;
    }
}