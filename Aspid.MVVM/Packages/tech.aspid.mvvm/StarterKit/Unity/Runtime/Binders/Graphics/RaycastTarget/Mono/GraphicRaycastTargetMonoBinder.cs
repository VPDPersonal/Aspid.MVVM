using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds the <see cref="Graphic.raycastTarget"/> property.
    /// </summary>
    /// <remarks>
    /// Turning this off makes the graphic invisible to pointer input while it stays on screen — the usual way to let clicks pass through an overlay.
    /// </remarks>
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
