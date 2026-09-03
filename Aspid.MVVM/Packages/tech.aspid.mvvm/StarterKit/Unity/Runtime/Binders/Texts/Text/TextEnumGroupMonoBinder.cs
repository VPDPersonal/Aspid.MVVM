using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement,TValue}">EnumGroupMonoBinder&lt;TMP_Text, string&gt;</see> that sets the <see cref="TMP_Text.text"/> property
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text", SubPath = "EnumGroup")]
    public sealed class TextEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Text element, string value) =>
            element.text = value;
    }
}