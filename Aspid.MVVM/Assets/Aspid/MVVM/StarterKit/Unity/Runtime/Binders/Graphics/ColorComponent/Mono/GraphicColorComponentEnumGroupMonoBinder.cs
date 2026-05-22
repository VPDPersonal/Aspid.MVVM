using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{Graphic}"/> that sets a single <see cref="ColorComponent"/> channel
    /// of the <see cref="Graphic.color"/> property on each <see cref="Graphic"/> in the group
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "EnumGroup")]
    public sealed class GraphicColorComponentEnumGroupMonoBinder : EnumGroupFloatMonoBinder<Graphic>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;

        /// <inheritdoc/>
        protected override void SetValue(Graphic element, float value) =>
            element.SetColorComponent(_colorComponent, value);
    }
}