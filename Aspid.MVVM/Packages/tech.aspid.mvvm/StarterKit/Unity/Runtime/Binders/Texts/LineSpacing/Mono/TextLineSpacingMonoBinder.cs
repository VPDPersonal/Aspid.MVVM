#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
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
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.lineSpacing = value;
            }
        }
    }
}
#endif
