using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{Scrollbar}"/> that sets <see cref="Scrollbar.value"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value Enum")]
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Value", SubPath = "Enum")]
    public sealed class ScrollbarValueEnumMonoBinder : EnumFloatMonoBinder<Scrollbar>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="Scrollbar.value"/> to the resolved float, clamped to the 0..1 range.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.value = BinderMath.SafeClamp01(value);
    }
}
