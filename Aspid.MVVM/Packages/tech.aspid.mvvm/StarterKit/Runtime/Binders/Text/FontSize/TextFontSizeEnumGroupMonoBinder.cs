using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_Text.fontSize"/> on each element.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize EnumGroup")]
    public sealed class TextFontSizeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Text element, float value)
        {
            if (this.RequireFinite(value))
                element.fontSize = value;
        }
    }
}
