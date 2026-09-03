using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_Text.font"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontAsset", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Font EnumGroup")]
    public sealed class TextFontEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, TMP_FontAsset>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Text element, TMP_FontAsset value) =>
            element.font = value;
    }
}
