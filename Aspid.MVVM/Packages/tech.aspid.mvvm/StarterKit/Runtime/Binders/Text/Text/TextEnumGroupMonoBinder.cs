using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_Text.text"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text EnumGroup")]
    public sealed class TextEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Text element, string value) =>
            element.text = value;
    }
}
