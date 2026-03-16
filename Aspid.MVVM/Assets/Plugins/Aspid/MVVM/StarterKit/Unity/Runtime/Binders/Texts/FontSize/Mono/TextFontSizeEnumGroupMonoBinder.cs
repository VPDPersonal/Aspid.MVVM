#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{TMP_Text}"/> that sets the <see cref="TMP_Text.fontSize"/> property
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "EnumGroup")]
    public sealed class TextFontSizeEnumGroupMonoBinder : EnumGroupFloatMonoBinder<TMP_Text>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="TMP_Text.fontSize"/> of the element to the resolved value.
        /// </summary>
        protected override void SetValue(TMP_Text element, float value) =>
            element.fontSize = value;
    }
}
#endif