using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="BoxCollider.center"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Center EnumGroup")]
    public sealed class BoxColliderCenterEnumGroupMonoBinder : EnumGroupMonoBinder<BoxCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(BoxCollider element, Vector3 value)
        {
            if (this.RequireFinite(value))
                element.center = value;
        }
    }
}
