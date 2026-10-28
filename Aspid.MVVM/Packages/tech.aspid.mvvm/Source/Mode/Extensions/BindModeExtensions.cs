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
        /// Determines whether <paramref name="mode"/> pushes values only from the ViewModel to the View.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns>
        /// <see langword="true"/> for <see cref="BindMode.OneWay"/> and <see cref="BindMode.OneTime"/>; otherwise, <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this BindMode mode) =>
            mode is BindMode.OneWay or BindMode.OneTime;
        
        /// <summary>
        /// Determines whether <paramref name="mode"/> pushes values from the View to the ViewModel.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns>
        /// <see langword="true"/> for <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsTwo(this BindMode mode) =>
            mode is BindMode.TwoWay or BindMode.OneWayToSource;
        
        /// <summary>
        /// Determines whether <paramref name="mode"/> is <see cref="BindMode.None"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <returns><see langword="true"/> for <see cref="BindMode.None"/>; otherwise, <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [System.Obsolete("Compare with BindMode.None directly. Nothing in the package calls this, and the name reads as a question about validity rather than about one enum member. Will be removed in the next major version.")]
        public static bool IsNone(this BindMode mode) =>
            mode is BindMode.None;
    }
}
