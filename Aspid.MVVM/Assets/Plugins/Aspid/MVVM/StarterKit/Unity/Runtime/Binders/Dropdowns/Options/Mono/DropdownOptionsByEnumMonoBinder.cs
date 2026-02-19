#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<System.Enum, System.Collections.Generic.IEnumerable<TMPro.TMP_Dropdown.OptionData>>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterEnumToDropdownOptionData;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_Dropdown))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options By Enum")]
    public class DropdownOptionsByEnumMonoBinder : ComponentMonoBinder<TMP_Dropdown>, IBinder<Enum>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        public void SetValue(Enum value)
        {
            CachedComponent.options ??= new List<TMP_Dropdown.OptionData>();
            CachedComponent.ClearOptions();
            
            if (_converter is null)
            {
                var values = Enum.GetValues(value.GetType());
                
                foreach (var enumObj in values)
                    CachedComponent.options.Add(new TMP_Dropdown.OptionData(text: enumObj.ToString()));
            }
            else
            {
                foreach (var option in _converter.Convert(value))
                    CachedComponent.options.Add(option);
            }
        }
    }
}
#endif