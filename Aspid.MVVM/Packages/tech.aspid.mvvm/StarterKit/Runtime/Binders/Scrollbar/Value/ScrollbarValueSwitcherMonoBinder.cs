using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="Scrollbar.value"/>.
    /// </summary>
    /// <remarks>
    /// {VR}
    /// </remarks>
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Value", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value Switcher")]
    public sealed class ScrollbarValueSwitcherMonoBinder : SwitcherMonoBinder<Scrollbar, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.SetValueWithoutNotify(this.SafeClamp01(value));
    }
}
