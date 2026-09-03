using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_Text.font"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontAsset", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Font Enum")]
    public sealed class TextFontEnumMonoBinder : EnumMonoBinder<TMP_Text, TMP_FontAsset>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_FontAsset value) =>
            CachedComponent.font = value;
    }
}
