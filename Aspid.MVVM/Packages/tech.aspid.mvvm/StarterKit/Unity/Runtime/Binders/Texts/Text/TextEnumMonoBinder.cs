#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;TMP_Text, string&gt;</see> that sets the <see cref="TMP_Text.text"/> property
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text Enum")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text", SubPath = "Enum")]
    public sealed class TextEnumMonoBinder : EnumMonoBinder<TMP_Text, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            CachedComponent.text = value;
    }
}
#endif