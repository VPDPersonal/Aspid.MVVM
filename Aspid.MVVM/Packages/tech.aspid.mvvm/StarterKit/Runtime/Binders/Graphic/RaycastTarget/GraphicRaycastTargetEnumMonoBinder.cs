using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Graphic.raycastTarget"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_RaycastTarget", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Raycast Target Enum")]
    public sealed class GraphicRaycastTargetEnumMonoBinder : EnumMonoBinder<Graphic, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.raycastTarget = value;
    }
}
