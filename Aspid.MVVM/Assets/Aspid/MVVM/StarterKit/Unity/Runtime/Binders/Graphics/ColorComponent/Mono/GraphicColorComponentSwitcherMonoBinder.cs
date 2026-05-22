using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{Graphic}"/> that switches a single <see cref="ColorComponent"/> channel
    /// of the <see cref="Graphic.color"/> property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component Switcher")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Switcher")]
    public sealed class GraphicColorComponentSwitcherMonoBinder : SwitcherFloatMonoBinder<Graphic>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.SetColorComponent(_colorComponent, value);
    }
}