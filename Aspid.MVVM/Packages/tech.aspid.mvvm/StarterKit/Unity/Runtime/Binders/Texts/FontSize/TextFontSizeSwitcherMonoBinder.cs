using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;TMP_Text, float&gt;</see> that switches the <see cref="TMP_Text.fontSize"/>
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize Switcher")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "Switcher")]
    public sealed class TextFontSizeSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.fontSize = value;
    }
}