using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;TMP_Text, string&gt;</see> that switches the <see cref="TMP_Text.text"/>
    /// between two string values based on the bound boolean ViewModel value.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text Switcher")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text", SubPath = "Switcher")]
    public sealed class TextSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            CachedComponent.text = value;
    }
}