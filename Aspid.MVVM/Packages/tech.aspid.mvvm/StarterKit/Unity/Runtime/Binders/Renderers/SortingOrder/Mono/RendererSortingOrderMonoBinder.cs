using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{Renderer}"/> that binds <see cref="Renderer.sortingOrder"/>.
    /// </summary>
    /// <remarks>
    /// Draw order within a sorting layer, for every renderer and not only a sprite one: the package bound
    /// <see cref="SpriteRenderer"/>'s order and left the property it inherits from
    /// <see cref="Renderer"/> unbound, so a line, a mesh or a trail could not be brought to the front.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Sorting Order")]
    public class RendererSortingOrderMonoBinder : ComponentIntMonoBinder<Renderer>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.sortingOrder;
            set => CachedComponent.sortingOrder = value;
        }
    }
}
