using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement,TValue}">EnumGroupMonoBinder&lt;TMP_Text, float&gt;</see> that sets the <see cref="TMP_Text.fontSize"/> property
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "EnumGroup")]
    public sealed class TextFontSizeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Text element, float value) =>
            element.fontSize = value;
    }
}