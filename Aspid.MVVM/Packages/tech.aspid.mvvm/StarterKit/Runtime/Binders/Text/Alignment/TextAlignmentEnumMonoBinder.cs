using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_Text.alignment"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_textAlignment", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Alignment Enum")]
    public sealed class TextAlignmentEnumMonoBinder : EnumMonoBinder<TMP_Text, TextAlignmentOptions>
    {
        /// <inheritdoc/>
        protected override void SetValue(TextAlignmentOptions value) =>
            CachedComponent.alignment = value;
    }
}
