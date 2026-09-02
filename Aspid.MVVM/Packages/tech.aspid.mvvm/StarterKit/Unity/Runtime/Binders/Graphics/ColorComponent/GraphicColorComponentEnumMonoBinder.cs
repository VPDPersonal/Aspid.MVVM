using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;Graphic, float&gt;</see> that sets a single <see cref="ColorComponent"/> channel
    /// of the <see cref="Graphic.color"/> property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component Enum")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Enum")]
    public sealed class GraphicColorComponentEnumMonoBinder : EnumMonoBinder<Graphic, float>
    {
        [Tooltip("Which color channel the bound value writes to; others keep their value.")]
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.SetColorComponent(_colorComponent, value);
    }
}