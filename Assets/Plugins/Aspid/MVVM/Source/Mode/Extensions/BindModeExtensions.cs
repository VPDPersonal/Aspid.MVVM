using System;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static class BindModeExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode is either <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentException">Thrown when the mode is <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfOne(this BindMode mode)
        {
            if (mode is BindMode.OneWay or BindMode.OneTime)
                throw new ArgumentException($"BindMode can't be {nameof(BindMode.OneWay)} and {nameof(BindMode.OneTime)}. Mode = {mode}");
        }
        
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode is either <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentException">Thrown when the mode is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfTwo(this BindMode mode)
        {
            if (mode is BindMode.TwoWay or BindMode.OneWayToSource)
                throw new ArgumentException($"BindMode can't be {nameof(BindMode.TwoWay)} and {nameof(BindMode.OneWayToSource)}. Mode = {mode}");
        }
        
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode does not match the specified valid mode.
        /// </summary>
        /// <param name="mode">The current binding mode to check.</param>
        /// <param name="validMode">The valid binding mode to compare against.</param>
        /// <exception cref="ArgumentException">Thrown when the mode is not equal to the valid mode.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNotMatches(this BindMode mode, BindMode validMode)
        {
            if (mode != validMode)
                throw new ArgumentException($"BindMode can be only {validMode}. Mode = {mode}");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode matches the specified invalid mode.
        /// </summary>
        /// <param name="mode">The current binding mode to check.</param>
        /// <param name="invalidMode">The invalid binding mode to compare against.</param>
        /// <exception cref="ArgumentException">Thrown when the mode matches the invalid mode.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfMatches(this BindMode mode, BindMode invalidMode)
        {
            if (mode == invalidMode)
                throw new ArgumentException($"BindMode is not supported {invalidMode}");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode is <see cref="BindMode.None"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentException">Thrown when the mode is <see cref="BindMode.None"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNone(this BindMode mode)
        {
            if (mode is BindMode.None) 
                throw new ArgumentException("Mode can't be none");
        }
    }
}