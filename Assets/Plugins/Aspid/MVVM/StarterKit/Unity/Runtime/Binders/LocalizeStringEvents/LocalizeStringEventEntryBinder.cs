#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using UnityEngine.Localization.Components;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class LocalizeStringEventEntryBinder : TargetBinder<LocalizeStringEvent>, IBinder<string>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public LocalizeStringEventEntryBinder(LocalizeStringEvent target, BindMode mode)
            :this(target, null, mode) { }
        
        public LocalizeStringEventEntryBinder(LocalizeStringEvent target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            :base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter;
        }
        
        public void SetValue(string? value) =>
            Target.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif