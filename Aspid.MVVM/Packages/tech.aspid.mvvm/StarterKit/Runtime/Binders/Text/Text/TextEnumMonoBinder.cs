using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_Text.text"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text Enum")]
    public sealed class TextEnumMonoBinder : EnumMonoBinder<TMP_Text, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            CachedComponent.text = value;
    }
}
