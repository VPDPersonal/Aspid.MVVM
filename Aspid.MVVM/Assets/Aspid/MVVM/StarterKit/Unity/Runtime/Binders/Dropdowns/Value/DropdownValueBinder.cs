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
    /// <include file="XmlExampleDoc-Dropdown-Value-1.1.0.xml" path="doc//member[@name='DropdownValueBinder']/*" />
    [Serializable]
    public class DropdownValueBinder : TargetIntBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.value;
            set => Target.value = value;
        }

        /// <inheritdoc/>
        public DropdownValueBinder(TMP_Dropdown target, IConverter<int, int>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif