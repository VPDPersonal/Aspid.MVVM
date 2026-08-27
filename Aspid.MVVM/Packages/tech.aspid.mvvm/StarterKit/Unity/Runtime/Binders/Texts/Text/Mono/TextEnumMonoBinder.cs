#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumStringMonoBinder{TMP_Text}"/> that sets the <see cref="TMP_Text.text"/> property
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text Enum")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text", SubPath = "Enum")]
    public sealed class TextEnumMonoBinder : EnumStringMonoBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            CachedComponent.text = value;
    }
}
#endif