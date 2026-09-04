using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Scrollbar.value"/> on each element.
    /// </summary>
    /// <remarks>
    /// {VR}
    /// </remarks>
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Value", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value EnumGroup")]
    public sealed class ScrollbarValueEnumGroupMonoBinder : EnumGroupMonoBinder<Scrollbar, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(Scrollbar element, float value) =>
            element.SetValueWithoutNotify(this.SafeClamp01(value));
    }
}
