using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class BindModeExtensions
    {
        /// <summary>
        /// Ensures the mode is declared and is not <see cref="BindMode.None"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="ArgumentOutOfRangeException">The mode is <see cref="BindMode.None"/> or undefined.</exception>
        public static void EnsureValid(this BindMode mode)
        {
            if (mode.IsValid()) return;
            throw new ArgumentOutOfRangeException(nameof(mode), mode, CreateModeMessage(mode, BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime, BindMode.OneWayToSource));
        }

        /// <summary>
        /// Ensures the mode sends values only from the ViewModel to the View.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="InvalidOperationException">The mode is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
        public static void EnsureForwardOnly(this BindMode mode)
        {
            if (mode.IsForwardOnly()) return;
            throw new InvalidOperationException(CreateModeMessage(mode, BindMode.OneWay, BindMode.OneTime));
        }

        /// <summary>
        /// Ensures the mode sends values from the ViewModel to the View.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="InvalidOperationException">The mode is not <see cref="BindMode.OneWay"/>, <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneTime"/>.</exception>
        public static void EnsureForward(this BindMode mode)
        {
            if (mode.IncludesForward()) return;
            throw new InvalidOperationException(CreateModeMessage(mode, BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime));
        }

        /// <summary>
        /// Ensures the mode sends values from the View to the ViewModel.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <exception cref="InvalidOperationException">The mode is neither <see cref="BindMode.TwoWay"/> nor <see cref="BindMode.OneWayToSource"/>.</exception>
        public static void EnsureReverse(this BindMode mode)
        {
            if (mode.IncludesReverse()) return;
            throw new InvalidOperationException(CreateModeMessage(mode, BindMode.TwoWay, BindMode.OneWayToSource));
        }

        /// <summary>
        /// Ensures the mode is valid and differs from <paramref name="excludedMode"/>.
        /// </summary>
        /// <param name="mode">The binding mode to check.</param>
        /// <param name="excludedMode">The mode to exclude.</param>
        /// <exception cref="ArgumentOutOfRangeException">Either argument is <see cref="BindMode.None"/> or undefined.</exception>
        /// <exception cref="ArgumentException"><paramref name="mode"/> equals <paramref name="excludedMode"/>.</exception>
        public static void EnsureNot(this BindMode mode, BindMode excludedMode)
        {
            mode.EnsureValid();
            if (!excludedMode.IsValid())
                throw new ArgumentOutOfRangeException(nameof(excludedMode), excludedMode, "The excluded mode must be a valid binding mode.");

            if (mode != excludedMode) return;

            var index = 0;
            var allowed = new BindMode[3];
            
            foreach (var candidate in new[] { BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime, BindMode.OneWayToSource })
            {
                if (candidate == excludedMode) continue;
                allowed[index++] = candidate;
            }

            throw new ArgumentException(CreateModeMessage(mode, allowed), nameof(mode));
        }

        internal static void ValidateModes(BindMode[] modes)
        {
            if (modes is null)
                throw new ArgumentNullException(nameof(modes));

            foreach (var mode in modes)
            {
                if (mode.IsValid()) continue;

                throw new ArgumentOutOfRangeException(nameof(modes), mode,
                    CreateModeMessage(mode, BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime, BindMode.OneWayToSource));
            }
        }

        private static string CreateModeMessage(BindMode mode, params BindMode[] allowed)
        {
            var names = new string[allowed.Length];

            for (var i = 0; i < names.Length; i++)
                names[i] = allowed[i].ToString().GetStructMessage();

            return $"Unsupported binding mode: {mode.ToString().GetStructMessage()}.\nAllowed: {string.Join(", ", names)}.";
        }
    }
}
