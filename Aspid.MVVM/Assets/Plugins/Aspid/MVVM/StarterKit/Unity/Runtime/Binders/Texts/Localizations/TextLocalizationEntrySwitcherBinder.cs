#if (UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION) && ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using UnityEngine.Localization;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherStringBinder{TMP_Text}"/> that switches the localization table entry reference
    /// between two values and sets <see cref="TMP_Text.text"/> via a <see cref="LocalizedString"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-Text-Localization-1.1.0.xml" path="doc//member[@name='TextLocalizationEntrySwitcherBinder']/*" />
    [Serializable]
    public class TextLocalizationEntrySwitcherBinder : SwitcherStringBinder<TMP_Text>
    {
        [Tooltip("The localized string reference that provides the localized text.")]
        [SerializeField] private LocalizedString _stringReference = new();

        [Tooltip("Format arguments passed to the localized string.")]
        [SerializeField] private List<Object> _formatArguments = new();
        
        /// <summary>
        /// Initializes a new instance of <see cref="TextLocalizationEntrySwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_Text"/> to bind.</param>
        /// <param name="trueValue">The entry reference applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The entry reference applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="entry">The initial localization table entry reference, or <see langword="null"/> to leave unset.</param>
        /// <param name="formatArguments">Format arguments passed to the localized string, or <see langword="null"/> to use none.</param>
        /// <param name="converter">The converter used to transform the selected string value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode.</param>
        public TextLocalizationEntrySwitcherBinder(
            TMP_Text target, 
            string trueValue, 
            string falseValue,
            string? entry = null,
            List<Object>? formatArguments = null,
            IConverter<string, string>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _formatArguments = formatArguments ?? _formatArguments;
            _stringReference.TableEntryReference = entry;
        }
        
        /// <summary>
        /// Called before binding is established. Subscribes to localization string changes.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnBinding()</c> to preserve
        /// the localization string subscription behavior.
        /// </remarks>
        protected override void OnBinding() =>
            Subscribe();

        /// <summary>
        /// Called after unbinding. Unsubscribes from localization string changes.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnUnbound()</c> to preserve
        /// the localization string unsubscription behavior.
        /// </remarks>
        protected override void OnUnbound() =>
            Unsubscribe();

        private void Subscribe() =>
            _stringReference.Subscribe(_formatArguments, UpdateString);

        private void Unsubscribe() =>
            _stringReference.Unsubscribe(UpdateString);
        
        /// <summary>
        /// Called when applying the selected entry key.
        /// Sets the localized string table entry reference.
        /// </summary>
        protected override void SetValue(string value) =>
            _stringReference.TableEntryReference = value;

        /// <summary>
        /// Called when the localized string changes. Sets <see cref="TMP_Text.text"/> to the localized value.
        /// </summary>
        protected virtual void UpdateString(string value) =>
            Target.text = value;
    }
}
#endif