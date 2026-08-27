using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Graphic}"/> that binds a single <see cref="ColorComponent"/> channel
    /// of the <see cref="Graphic.color"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component")]
    public class GraphicColorComponentMonoBinder : ComponentFloatMonoBinder<Graphic>
    {
        [Tooltip("Which color channel the bound value writes to; others keep their value.")]
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.GetColorComponent(_colorComponent);
            set => CachedComponent.SetColorComponent(_colorComponent, value);
        }
    }
}