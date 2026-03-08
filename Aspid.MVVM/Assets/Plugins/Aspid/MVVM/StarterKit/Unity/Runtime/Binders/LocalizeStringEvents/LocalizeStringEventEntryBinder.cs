#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine.Localization.Components;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <c>TableEntryReference</c> of a <see cref="LocalizeStringEvent"/> when the bound
    /// ViewModel string value changes. Supports an optional string-to-string converter.
    /// <see cref="BindMode.TwoWay"/> is not supported.
    /// </summary>
    [Serializable]
    public class LocalizeStringEventEntryBinder : TargetBinder<LocalizeStringEvent, string, Converter>
    {
        protected sealed override string? Property
        {
            get => Target.StringReference.TableEntryReference;
            set => Target.StringReference.TableEntryReference = value;
        }
        
        public LocalizeStringEventEntryBinder(LocalizeStringEvent target, BindMode mode)
            :this(target, converter: null, mode) { }
        
        public LocalizeStringEventEntryBinder(LocalizeStringEvent target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            :base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif