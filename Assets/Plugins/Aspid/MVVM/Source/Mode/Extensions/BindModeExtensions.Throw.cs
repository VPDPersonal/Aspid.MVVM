using System;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides throwing helpers for validating <see cref="BindMode"/> values.
    /// </summary>
    public static partial class BindModeExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode is either <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentException">Thrown when the mode is <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfOne(this BindMode mode)
        {
            if (mode.IsOne())
                throw new InvalidOperationException($"BindMode can't be BindMode.OneWay and BindMode.OneTime. Mode = {mode}");
        }
        
        /// <summary>
        /// Throws if the binding mode is not one-way style (<see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>).
        /// </summary>
        /// <param name="mode">The binding mode to validate.</param>
        /// <exception cref="InvalidOperationException">Thrown when the mode is not OneWay or OneTime.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNotOne(this BindMode mode)
        {
            if (!mode.IsOne())
                throw new InvalidOperationException($"Mode must be OneWay or OneTime. Mode = {mode}");
        }
        
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode is either <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentException">Thrown when the mode is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfTwo(this BindMode mode)
        {
            if (mode.IsTwo())
                throw new InvalidOperationException($"BindMode can't be BindMode.TwoWay and BindMode.OneWayToSource. Mode = {mode}");
        }
        
        /// <summary>
        /// Throws if the binding mode is not two-way style (<see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>).
        /// </summary>
        /// <param name="mode">The binding mode to validate.</param>
        /// <exception cref="InvalidOperationException">Thrown when the mode is not TwoWay or OneWayToSource.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNotTwo(this BindMode mode)
        {
            if (!mode.IsTwo())
                throw new InvalidOperationException($"Mode must be TwoWay or OneWayToSource. Mode = {mode}");
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