using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;Scrollbar, float&gt;</see> that switches <see cref="Scrollbar.value"/>
    /// between two float values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value Switcher")]
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Value", SubPath = "Switcher")]
    public sealed class ScrollbarValueSwitcherMonoBinder : SwitcherMonoBinder<Scrollbar, float>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="Scrollbar.value"/>.
        /// The value is clamped to the 0..1 range a scrollbar accepts.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.SetValueWithoutNotify(this.SafeClamp01(value));
    }
}
