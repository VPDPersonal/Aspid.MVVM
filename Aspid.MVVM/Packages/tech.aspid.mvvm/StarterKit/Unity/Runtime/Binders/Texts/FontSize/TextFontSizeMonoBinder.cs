using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TMP_Text}"/> that sets the <see cref="TMP_Text.fontSize"/> property.
    /// </summary>
    /// <remarks>
    /// Non-finite values are ignored — TMP would otherwise rebuild the mesh from <see cref="float.NaN"/>
    /// and the text disappears entirely.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize")]
    public class TextFontSizeMonoBinder : ComponentFloatMonoBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.fontSize;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.fontSize = value;
            }
        }
    }
}