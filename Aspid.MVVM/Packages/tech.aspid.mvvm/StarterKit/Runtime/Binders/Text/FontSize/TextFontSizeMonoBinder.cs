using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="TMP_Text.fontSize"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize")]
    public class TextFontSizeMonoBinder : ComponentFloatMonoBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.fontSize;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.fontSize = value;
            }
        }
    }
}
