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
    /// <see cref="SwitcherBinder{TTarget, T}"/> that switches the Unity Localization entry written to
    /// <see cref="TMP_Text.text"/>.
    /// </summary>
    [Serializable]
    public class TextLocalizationEntrySwitcherBinder : SwitcherBinder<TMP_Text, string>
    {
        [Tooltip("Localized string whose table entry is bound.")]
        [SerializeField] private LocalizedString _stringReference = new();

        [Tooltip("Format arguments passed to the localized string.")]
        [SerializeField] private List<Object> _formatArguments = new();

        /// <param name="target">The text to bind.</param>
        /// <param name="trueValue">The entry key applied when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The entry key applied when the bound value is <see langword="false"/>.</param>
        /// <param name="entry">The initial table entry key, or <see langword="null"/> to leave it unset.</param>
        /// <param name="formatArguments">
        /// Format arguments passed to the localized string, or <see langword="null"/> for none.
        /// </param>
        /// <param name="converter">
        /// The converter applied to the chosen key, or <see langword="null"/> to use it as-is.
        /// </param>
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
            _stringReference.TableEntryReference = entry;
            _formatArguments = formatArguments ?? _formatArguments;
        }

        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            _stringReference.TableEntryReference = value;

        /// <summary>
        /// Subscribes to <see cref="LocalizedString.StringChanged"/> before the first value arrives.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnBinding()</c>.
        /// </remarks>
        protected override void OnBinding() =>
            _stringReference.Subscribe(_formatArguments, UpdateString);

        /// <summary>
        /// Unsubscribes from <see cref="LocalizedString.StringChanged"/>.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnUnbound()</c>.
        /// </remarks>
        protected override void OnUnbound() =>
            _stringReference.Unsubscribe(UpdateString);

        /// <summary>
        /// Writes the localized <paramref name="value"/> to <see cref="TMP_Text.text"/>.
        /// </summary>
        /// <param name="value">The formatted localized string.</param>
        protected virtual void UpdateString(string value) =>
            Target.text = value;
    }
}
#endif
