using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Graphic}"/> that binds a single <see cref="ColorComponent"/> channel
    /// of the <see cref="Graphic.color"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current channel value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component")]
    public class GraphicColorComponentMonoBinder : ComponentFloatMonoBinder<Graphic>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.GetColorComponent(_colorComponent);
            set => CachedComponent.SetColorComponent(_colorComponent, GetConvertedValue(value));
        }
    }
}