using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="TMP_Text.font"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontAsset", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Font Switcher")]
    public sealed class TextFontSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, TMP_FontAsset>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_FontAsset value) =>
            CachedComponent.font = value;
    }
}
