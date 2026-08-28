using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{BoxCollider, Vector3}"/> that sets the <see cref="BoxCollider.center"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Center EnumGroup")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    public sealed class BoxColliderCenterEnumGroupMonoBinder : EnumGroupMonoBinder<BoxCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(BoxCollider element, Vector3 value) =>
            element.center = value;
    }
}