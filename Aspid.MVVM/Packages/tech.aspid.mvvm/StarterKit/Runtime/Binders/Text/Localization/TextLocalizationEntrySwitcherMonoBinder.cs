#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches the Unity Localization entry written to
    /// <see cref="TMP_Text.text"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Localization Entry Switcher")]
    public class TextLocalizationEntrySwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, string>
    {
        [Tooltip("Localized string whose table entry is bound.")]
        [SerializeField] private LocalizedString _stringReference = new();

        [Tooltip("Format arguments passed to the localized string.")]
        [SerializeField] private List<Object> _formatArguments = new();

        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            _stringReference.TableEntryReference = value;

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();
            _stringReference?.RefreshString();
        }

        /// <summary>
        /// Subscribes to <see cref="LocalizedString.StringChanged"/>.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnEnable()</c>.
        /// </remarks>
        protected virtual void OnEnable() =>
            _stringReference.Subscribe(_formatArguments, UpdateString);

        /// <summary>
        /// Unsubscribes from <see cref="LocalizedString.StringChanged"/>.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnDisable()</c>.
        /// </remarks>
        protected virtual void OnDisable() =>
            _stringReference.Unsubscribe(UpdateString);

        /// <summary>
        /// Writes the localized <paramref name="value"/> to <see cref="TMP_Text.text"/>.
        /// </summary>
        /// <param name="value">The formatted localized string.</param>
        protected virtual void UpdateString(string value) =>
            CachedComponent.text = value;
    }
}
#endif
