#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.UI.ColorBlock, UnityEngine.UI.ColorBlock>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColorBlock;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class DropdownColorBlockBinder : TargetBinder<TMP_Dropdown, ColorBlock, Converter>
    {
        protected sealed override ColorBlock Property
        {
            get => Target.colors;
            set => Target.colors = value;
        }
        
        public DropdownColorBlockBinder(TMP_Dropdown target, BindMode mode) 
            : this(target, converter: null, mode) { }

        public DropdownColorBlockBinder(TMP_Dropdown target, Converter converter = null, BindMode mode = BindMode.OneWay) 
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif