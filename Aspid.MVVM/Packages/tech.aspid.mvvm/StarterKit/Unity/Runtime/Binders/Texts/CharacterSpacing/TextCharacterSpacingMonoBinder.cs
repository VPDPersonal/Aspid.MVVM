#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TMP_Text}"/> that binds <see cref="TMP_Text.characterSpacing"/>.
    /// </summary>
    /// <remarks>
    /// Non-finite values are ignored — TMP would otherwise rebuild the mesh from <see cref="float.NaN"/>
    /// and the text disappears entirely.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_characterSpacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – CharacterSpacing")]
    public class TextCharacterSpacingMonoBinder : ComponentFloatMonoBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.characterSpacing;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.characterSpacing = value;
            }
        }
    }
}
#endif
