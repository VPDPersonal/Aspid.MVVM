#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class DropdownValueBinder : TargetIntBinder<TMP_Dropdown>
    {
        protected sealed override int Property
        {
            get => Target.value;
            set => Target.value = value;
        }

        public DropdownValueBinder(TMP_Dropdown target, BindMode mode)
            : this(target, converter: null, mode) { }

        public DropdownValueBinder(TMP_Dropdown target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif