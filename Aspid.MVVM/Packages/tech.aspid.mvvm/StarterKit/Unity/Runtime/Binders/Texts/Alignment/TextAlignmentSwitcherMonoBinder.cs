using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TMP_Text, TextAlignmentOptions}"/> that switches the <see cref="TMP_Text.alignment"/>
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Alignment Switcher")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_textAlignment", SubPath = "Switcher")]
    public sealed class TextAlignmentSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, TextAlignmentOptions>
    {
        /// <inheritdoc/>
        protected override void SetValue(TextAlignmentOptions value) =>
            CachedComponent.alignment = value;
    }
}