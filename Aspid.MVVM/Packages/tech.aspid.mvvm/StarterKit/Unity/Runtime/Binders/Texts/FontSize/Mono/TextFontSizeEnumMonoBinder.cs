#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{TMP_Text}"/> that sets the <see cref="TMP_Text.fontSize"/> property
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize Enum")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "Enum")]
    public sealed class TextFontSizeEnumMonoBinder : EnumFloatMonoBinder<TMP_Text>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="TMP_Text.fontSize"/> to the resolved value.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.fontSize = value;
    }
}
#endif