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
    /// <see cref="TargetBinder{TMP_Text, string}"/> that sets the <see cref="TMP_Text.text"/> property
    /// using a Unity Localization entry, resolved via a <see cref="LocalizedString"/>.
    /// </summary>
    [Serializable]
    public class TextLocalizationEntryBinder : TargetBinder<TMP_Text, string>
    {
        [Tooltip("The localized string reference that provides the localized text.")]
        [SerializeField] private LocalizedString _stringReference = new();

        [Tooltip("Format arguments passed to the localized string.")]
        [SerializeField] private List<Object> _formatArguments = new();

        /// <param name="target">The <see cref="TMP_Text"/> to bind.</param>
        /// <param name="entry">The initial localization table entry reference, or <see langword="null"/> to leave unset.</param>
        /// <param name="formatArguments">Format arguments passed to the localized string, or <see langword="null"/> to use none.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="string"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TextLocalizationEntryBinder(
            TMP_Text target, 
            string? entry = null,
            List<Object>? formatArguments = null,
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            
            _formatArguments = formatArguments ?? _formatArguments;
            _stringReference.TableEntryReference = entry;
        }

        /// <inheritdoc/>
        protected sealed override string? Property
        {
            get => _stringReference.TableEntryReference.ToKeyName(this, Target);
            set => _stringReference.TableEntryReference = value;
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
        /// Called when the localized string changes. Sets <see cref="TMP_Text.text"/> to the localized value.
        /// </summary>
        /// <param name="value">The value formatted into the localized string.</param>
        protected virtual void UpdateString(string value) =>
            Target.text = value;
    }
}
#endif