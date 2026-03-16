#if (UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION) && ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumStringMonoBinder{TMP_Text}"/> that sets the <see cref="TMP_Text.text"/> property
    /// by resolving a localization table entry based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Localization Entry Enum")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text", SubPath = "Enum")]
    public class TextLocalizationEntryEnumMonoBinder : EnumStringMonoBinder<TMP_Text>
    {
        [Tooltip("The localized string reference that provides the localized text.")]
        [SerializeField] private LocalizedString _stringReference = new();

        [Tooltip("Format arguments passed to the localized string.")]
        [SerializeField] private List<Object> _formatArguments = new();
        
        protected override void OnValidate()
        {
            base.OnValidate();
            _stringReference?.RefreshString();
        }
        
        /// <summary>
        /// Called by Unity when the <see cref="MonoBehaviour"/> becomes enabled and active.
        /// Subscribes to localization string changes.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call base.OnEnable() to preserve
        /// the localization string subscription behavior.
        /// </remarks>
        protected virtual void OnEnable() =>
            Subscribe();

        /// <summary>
        /// Called by Unity when the <see cref="MonoBehaviour"/> becomes disabled.
        /// Unsubscribes from localization string changes.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call base.OnDisable() to preserve
        /// the localization string unsubscription behavior.
        /// </remarks>
        protected virtual void OnDisable() =>
            Unsubscribe();

        private void Subscribe() =>
            _stringReference.Subscribe(_formatArguments, UpdateString);

        private void Unsubscribe() =>
            _stringReference.Unsubscribe(UpdateString);

        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets the localized string table entry reference.
        /// </summary>
        protected sealed override void SetValue(string value) =>
            _stringReference.TableEntryReference = value;
        
        /// <summary>
        /// Called when the localized string changes. Updates <see cref="TMP_Text.text"/> with the localized value.
        /// </summary>
        protected virtual void UpdateString(string value) =>
            CachedComponent.text = value;
    }
}
#endif