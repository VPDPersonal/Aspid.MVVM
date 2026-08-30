using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;Scrollbar, float&gt;</see> that sets <see cref="Scrollbar.value"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value Enum")]
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Value", SubPath = "Enum")]
    public sealed class ScrollbarValueEnumMonoBinder : EnumMonoBinder<Scrollbar, float>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="Scrollbar.value"/> to the resolved float, clamped to the 0..1 range.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.SetValueWithoutNotify(this.SafeClamp01(value));
    }
}
