#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
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
    /// <see cref="SwitcherBinder{TTarget,T}">SwitcherBinder&lt;TMP_Text, string&gt;</see> that switches the localization table entry reference
    /// between two values and sets <see cref="TMP_Text.text"/> via a <see cref="LocalizedString"/>.
    /// </summary>
    [Serializable]
    public class TextLocalizationEntrySwitcherBinder : SwitcherBinder<TMP_Text, string>
    {
        [Tooltip("The localized string reference that provides the localized text.")]
        [SerializeField] private LocalizedString _stringReference = new();

        [Tooltip("Format arguments passed to the localized string.")]
        [SerializeField] private List<Object> _formatArguments = new();
        
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
        /// Subscribing here rather than in <c>OnBound</c> is required: the ViewModel's first push sets
        /// the table entry reference, which raises <see cref="LocalizedString.StringChanged"/> — a later
        /// subscription would miss it. Overrides must call base.OnBinding() to preserve the subscription.
        /// </remarks>
        protected override void OnBinding() =>
            Subscribe();

        /// <summary>
        /// Called after unbinding. Unsubscribes from localization string changes.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call base.OnUnbound() to preserve
        /// the localization string unsubscription behavior.
        /// </remarks>
        protected override void OnUnbound() =>
            Unsubscribe();

        private void Subscribe() =>
            _stringReference.Subscribe(_formatArguments, UpdateString);

        private void Unsubscribe() =>
            _stringReference.Unsubscribe(UpdateString);
        
        /// <summary>
        /// Sets the localized string table entry reference to the selected value.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(string value) =>
            _stringReference.TableEntryReference = value;

        /// <summary>
        /// Called when the localized string changes. Sets <see cref="TMP_Text.text"/> to the localized value.
        /// </summary>
        /// <param name="value">The value formatted into the localized string.</param>
        protected virtual void UpdateString(string value) =>
            Target.text = value;
    }
}
#endif