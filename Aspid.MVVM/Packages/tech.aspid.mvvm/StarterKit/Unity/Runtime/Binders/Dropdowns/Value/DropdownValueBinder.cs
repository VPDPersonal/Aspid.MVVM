#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{TMP_Dropdown}"/> that sets the <see cref="TMP_Dropdown.value"/> property.
    /// </summary>
    /// <remarks>
    /// Writes go through <see cref="TMP_Dropdown.SetValueWithoutNotify"/> rather than assigning
    /// <see cref="TMP_Dropdown.value"/> directly, which would raise <see cref="TMP_Dropdown.onValueChanged"/> as
    /// if the user had clicked, echoing the write back to every binder on the dropdown.
    /// </remarks>
    [Serializable]
    public class DropdownValueBinder : TargetIntBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public DropdownValueBinder(TMP_Dropdown target, IConverter<int, int>? converter = null, BindMode mode = BindMode.OneWay)
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
#endif