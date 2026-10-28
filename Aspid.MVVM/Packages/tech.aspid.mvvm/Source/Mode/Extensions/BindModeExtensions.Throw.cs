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
        /// Throws when <paramref name="mode"/> is <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="InvalidOperationException"><paramref name="mode"/> is <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [System.Obsolete("Nothing in the package throws for the one-way modes: a binder that supports only the reverse direction uses ThrowExceptionIfNotTwo, and one that supports only the forward direction has nothing to reject. Will be removed in the next major version.")]
        public static void ThrowExceptionIfOne(this BindMode mode)
        {
            if (!mode.IsOne()) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            throw new InvalidOperationException($"Mode can't be {OneWay} and {OneTime}. Mode = {modeMessage}");
        }
        
        /// <summary>
        /// Throws unless <paramref name="mode"/> is <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="InvalidOperationException"><paramref name="mode"/> is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNotOne(this BindMode mode)
        {
            if (mode.IsOne()) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            throw new InvalidOperationException($"Mode must be {OneWay} or {OneTime}. Mode = {modeMessage}");
        }
        
        /// <summary>
        /// Throws when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="InvalidOperationException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfTwo(this BindMode mode)
        {
            if (!mode.IsTwo()) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            throw new InvalidOperationException($"Mode can't be {TwoWay} and {OneWayToSource}. Mode = {modeMessage}");
        }
        
        /// <summary>
        /// Throws unless <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="InvalidOperationException"><paramref name="mode"/> is neither <see cref="BindMode.TwoWay"/> nor <see cref="BindMode.OneWayToSource"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNotTwo(this BindMode mode)
        {
            if (mode.IsTwo()) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            throw new InvalidOperationException($"Mode must be {TwoWay} or {OneWayToSource}. Mode = {modeMessage}");
        }
        
        /// <summary>
        /// Throws unless <paramref name="mode"/> equals <paramref name="validMode"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <param name="validMode">The only allowed mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> differs from <paramref name="validMode"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNotMatches(this BindMode mode, BindMode validMode)
        {
            if (mode == validMode) return;
            
            var modeMessage = mode.ToString().GetStructMessage();
            var validModeMessage = validMode.ToString().GetStructMessage();
            throw new ArgumentException($"Mode can be only {validModeMessage}. Mode = {modeMessage}");
        }

        /// <summary>
        /// Throws when <paramref name="mode"/> equals <paramref name="invalidMode"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <param name="invalidMode">The disallowed mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> equals <paramref name="invalidMode"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfMatches(this BindMode mode, BindMode invalidMode)
        {
            if (mode != invalidMode) return;
            
            var invalidModeMessage = invalidMode.ToString().GetStructMessage();
            throw new ArgumentException($"Mode is not supported {invalidModeMessage}");
        }

        /// <summary>
        /// Throws when <paramref name="mode"/> is <see cref="BindMode.None"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionIfNone(this BindMode mode)
        {
            if (mode is not BindMode.None) return;
            throw new ArgumentException($"Mode can't be {None}");
        }
    }
}
