#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetStringBinder{LocalizeStringEvent}"/> that sets the TableEntryReference
    /// of the component's StringReference when the bound ViewModel string value changes.
    /// </summary>
    /// <include file="XmlExampleDoc-LocalizeStringEvent-Entry-1.1.0.xml" path="doc//member[@name='LocalizeStringEventEntryBinder']/*" />
    [Serializable]
    public class LocalizeStringEventEntryBinder : TargetStringBinder<LocalizeStringEvent>
    {
        /// <inheritdoc/>
        protected sealed override string? Property
        {
            get => Target.StringReference.TableEntryReference;
            set => Target.StringReference.TableEntryReference = value;
        }
        
        /// <inheritdoc/>
        public LocalizeStringEventEntryBinder(LocalizeStringEvent target, IConverter<string?, string?>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif