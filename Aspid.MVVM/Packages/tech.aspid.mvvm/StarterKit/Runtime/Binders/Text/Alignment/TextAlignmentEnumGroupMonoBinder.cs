using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_Text.alignment"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_textAlignment", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Alignment EnumGroup")]
    public sealed class TextAlignmentEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, TextAlignmentOptions>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Text element, TextAlignmentOptions value) =>
            element.alignment = value;
    }
}
