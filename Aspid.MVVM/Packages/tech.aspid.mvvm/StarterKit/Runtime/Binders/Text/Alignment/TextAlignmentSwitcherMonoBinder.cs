using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="TMP_Text.alignment"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_textAlignment", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Alignment Switcher")]
    public sealed class TextAlignmentSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, TextAlignmentOptions>
    {
        /// <inheritdoc/>
        protected override void SetValue(TextAlignmentOptions value) =>
            CachedComponent.alignment = value;
    }
}
