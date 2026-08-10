using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{Graphic, Boolean}"/> that sets <see cref="Graphic.raycastTarget"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_RaycastTarget", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Raycast Target Enum")]
    public sealed class GraphicRaycastTargetEnumMonoBinder : EnumMonoBinder<Graphic, bool>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(bool value) =>
            CachedComponent.raycastTarget = value;
    }
}
