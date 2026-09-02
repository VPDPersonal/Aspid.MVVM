#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;TMP_Text, float&gt;</see> that sets the <see cref="TMP_Text.fontSize"/> property
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize Enum")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "Enum")]
    public sealed class TextFontSizeEnumMonoBinder : EnumMonoBinder<TMP_Text, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.fontSize = value;
    }
}
#endif