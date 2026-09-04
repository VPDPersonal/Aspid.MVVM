using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Scrollbar.value"/>.
    /// </summary>
    /// <remarks>
    /// {VR}
    /// </remarks>
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Value", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value Enum")]
    public sealed class ScrollbarValueEnumMonoBinder : EnumMonoBinder<Scrollbar, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.SetValueWithoutNotify(this.SafeClamp01(value));
    }
}
