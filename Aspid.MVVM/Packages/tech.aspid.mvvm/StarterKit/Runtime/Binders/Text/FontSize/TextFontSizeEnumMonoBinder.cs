using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_Text.fontSize"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize Enum")]
    public sealed class TextFontSizeEnumMonoBinder : EnumMonoBinder<TMP_Text, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value)
        {
            if (this.RequireFinite(value))
                CachedComponent.fontSize = value;
        }
    }
}
