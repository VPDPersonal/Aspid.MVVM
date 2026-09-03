using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TMP_Text}"/> that binds <see cref="TMP_Text.lineSpacing"/>.
    /// </summary>
    /// <remarks>
    /// Non-finite values are ignored — TMP would otherwise rebuild the mesh from <see cref="float.NaN"/>
    /// and the text disappears entirely.
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
                if (!this.RequireFinite(value)) return;
                CachedComponent.lineSpacing = value;
            }
        }
    }
}
