using System;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class BindModeExtensions
    {
        private static string None => "None".GetStructMessage();
        
        private static string OneWay => "OneWay".GetStructMessage();
        
        private static string TwoWay => "TwoWay".GetStructMessage();
        
        private static string OneTime => "OneTime".GetStructMessage();
        
        private static string OneWayToSource => "OneWayToSource".GetStructMessage();
        
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode is either <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentException">Thrown when the mode is <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfOne(this BindMode mode)
        {
            if (!mode.IsOne()) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            throw new InvalidOperationException($"Mode can't be {OneWay} and {OneTime}. Mode = {modeMessage}");
        }
        
        /// <summary>
        /// Throws if the binding mode is not one-way style (<see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>).
        /// </summary>
        /// <param name="mode">The binding mode to validate.</param>
        /// <exception cref="InvalidOperationException">Thrown when the mode is not OneWay or OneTime.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNotOne(this BindMode mode)
        {
            if (mode.IsOne()) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            throw new InvalidOperationException($"Mode must be {OneWay} or {OneTime}. Mode = {modeMessage}");
        }
        
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode is either <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentException">Thrown when the mode is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfTwo(this BindMode mode)
        {
            if (!mode.IsTwo()) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            throw new InvalidOperationException($"Mode can't be {TwoWay} and {OneWayToSource}. Mode = {modeMessage}");
        }
        
        /// <summary>
        /// Throws if the binding mode is not two-way style (<see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>).
        /// </summary>
        /// <param name="mode">The binding mode to validate.</param>
        /// <exception cref="InvalidOperationException">Thrown when the mode is not TwoWay or OneWayToSource.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNotTwo(this BindMode mode)
        {
            if (mode.IsTwo()) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            throw new InvalidOperationException($"Mode must be {TwoWay} or {OneWayToSource}. Mode = {modeMessage}");
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
            if (mode == validMode) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            var validModeMessage = validMode.ToString().GetStructMessage();
            throw new ArgumentException($"Mode can be only {validModeMessage}. Mode = {modeMessage}");
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
            if (mode != invalidMode) return;
            
            var invalidModeMessage = invalidMode.ToString().GetStructMessage();
            throw new ArgumentException($"Mode is not supported {invalidModeMessage}");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the binding mode is <see cref="BindMode.None"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentException">Thrown when the mode is <see cref="BindMode.None"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNone(this BindMode mode)
        {
            if (mode is not BindMode.None) return;
            throw new ArgumentException($"Mode can't be {None}");
        }
    }
}