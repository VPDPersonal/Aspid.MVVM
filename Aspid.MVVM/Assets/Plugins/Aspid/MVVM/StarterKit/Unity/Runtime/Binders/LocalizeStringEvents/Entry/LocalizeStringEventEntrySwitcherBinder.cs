#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherStringBinder{LocalizeStringEvent}"/> that switches the <c>TableEntryReference</c>
    /// of the component's <c>StringReference</c> between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-LocalizeStringEvent-Entry-1.1.0.xml" path="doc//member[@name='LocalizeStringEventEntrySwitcherBinder']/*" />
    [Serializable]
    public sealed class LocalizeStringEventEntrySwitcherBinder : SwitcherStringBinder<LocalizeStringEvent>
    {
        /// <inheritdoc/>
        public LocalizeStringEventEntrySwitcherBinder(
            LocalizeStringEvent target,
            string trueValue,
            string falseValue,
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected entry key.
        /// Sets the <c>TableEntryReference</c> of the component's <c>StringReference</c>.
        /// </summary>
        protected override void SetValue(string? value) =>
            Target.StringReference.TableEntryReference = value;
    }
}
#endif