using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{Graphic, Boolean}"/> that sets <see cref="Graphic.raycastTarget"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_RaycastTarget", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Raycast Target EnumGroup")]
    public sealed class GraphicRaycastTargetEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Graphic element, bool value) =>
            element.raycastTarget = value;
    }
}
