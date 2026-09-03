#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{TTarget}"/> that binds <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    /// <remarks>
    /// Writes use <see cref="TMP_Dropdown.SetValueWithoutNotify"/> so the write is not echoed through
    /// <see cref="TMP_Dropdown.onValueChanged"/>.
    /// </remarks>
    [Serializable]
    public class DropdownValueBinder : TargetIntBinder<TMP_Dropdown>
    {
        /// <param name="target">The dropdown to bind.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public DropdownValueBinder(
            TMP_Dropdown target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.value;
            set => Target.SetValueWithoutNotify(value);
        }
    }
}
