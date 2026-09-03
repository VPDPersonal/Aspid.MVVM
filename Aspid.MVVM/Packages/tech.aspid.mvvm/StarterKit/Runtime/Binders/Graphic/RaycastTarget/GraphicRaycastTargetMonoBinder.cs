using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Graphic.raycastTarget"/>.
    /// </summary>
    /// <remarks>
    /// Off, the graphic ignores pointer input while it stays visible, so clicks pass through it.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_RaycastTarget")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Raycast Target")]
    public class GraphicRaycastTargetMonoBinder : ComponentMonoBinder<Graphic, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.raycastTarget;
            set => CachedComponent.raycastTarget = value;
        }
    }
}
