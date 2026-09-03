using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="TMP_Text.lineSpacing"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_lineSpacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – LineSpacing")]
    public class TextLineSpacingMonoBinder : ComponentFloatMonoBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.lineSpacing;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.lineSpacing = value;
            }
        }
    }
}
