#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;TMP_Text, FontStyles&gt;</see> that binds
    /// <see cref="TMP_Text.fontStyle"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontStyle")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontStyle")]
    public class TextFontStyleMonoBinder : ComponentMonoBinder<TMP_Text, FontStyles>
    {
        /// <inheritdoc/>
        protected sealed override FontStyles Property
        {
            get => CachedComponent.fontStyle;
            set => CachedComponent.fontStyle = value;
        }
    }
}
#endif
