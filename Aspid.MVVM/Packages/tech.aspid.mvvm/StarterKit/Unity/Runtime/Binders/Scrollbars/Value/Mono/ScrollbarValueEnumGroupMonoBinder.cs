using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{Scrollbar}"/> that sets <see cref="Scrollbar.value"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value EnumGroup")]
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Value", SubPath = "EnumGroup")]
    public sealed class ScrollbarValueEnumGroupMonoBinder : EnumGroupFloatMonoBinder<Scrollbar>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="Scrollbar.value"/> of the element to the resolved float, clamped to the 0..1 range.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(Scrollbar element, float value) =>
            element.value = BinderMath.SafeClamp01(value);
    }
}
