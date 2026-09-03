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
    /// <see cref="TargetBinder{TTarget, TProperty}"/> that binds <see cref="TMP_Text.text"/> to a Unity
    /// Localization entry.
    /// </summary>
    /// <remarks>
    /// The bound string is the table entry key; the localized text is written whenever the entry resolves.
    /// </remarks>
    [Serializable]
    public class TextLocalizationEntryBinder : TargetBinder<TMP_Text, string>
    {
        [Tooltip("Localized string whose table entry is bound.")]
        [SerializeField] private LocalizedString _stringReference = new();

        [Tooltip("Format arguments passed to the localized string.")]
        [SerializeField] private List<Object> _formatArguments = new();

        /// <param name="target">The text to bind.</param>
        /// <param name="entry">The initial table entry key, or <see langword="null"/> to leave it unset.</param>
        /// <param name="formatArguments">
        /// Format arguments passed to the localized string, or <see langword="null"/> for none.
        /// </param>
        /// <param name="converter">
        /// The converter applied to the bound key, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TextLocalizationEntryBinder(
            TMP_Text target,
            string? entry = null,
            List<Object>? formatArguments = null,
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);

            _stringReference.TableEntryReference = entry;
            _formatArguments = formatArguments ?? _formatArguments;
        }

        /// <inheritdoc/>
        protected sealed override string? Property
        {
            get => _stringReference.TableEntryReference.ToKeyName(this, Target);
            set => _stringReference.TableEntryReference = value;
        }

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
