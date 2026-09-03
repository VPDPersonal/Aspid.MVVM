using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="TMP_Text.fontSize"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize Switcher")]
    public sealed class TextFontSizeSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value)
        {
            if (this.RequireFinite(value))
                CachedComponent.fontSize = value;
        }
    }
}
