using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Graphic}"/> that binds the <see cref="Graphic.raycastTarget"/> property.
    /// </summary>
    /// <remarks>
    /// Turning this off makes the graphic invisible to pointer input while it stays on screen — the usual way to let clicks pass through an overlay.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current value is sent back
    /// to the ViewModel. Supports optional value inversion.
    /// </remarks>
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_RaycastTarget")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Raycast Target")]
    public class GraphicRaycastTargetMonoBinder : ComponentBoolMonoBinder<Graphic>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.raycastTarget;
            set => CachedComponent.raycastTarget = value;
        }
    }
}
