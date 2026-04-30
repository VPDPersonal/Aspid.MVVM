#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TMP_Text}"/> that sets the <see cref="TMP_Text.fontSize"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current font size
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize")]
    public class TextFontSizeMonoBinder : ComponentFloatMonoBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.fontSize;
            set => CachedComponent.fontSize = value;
        }
    }
}
#endif