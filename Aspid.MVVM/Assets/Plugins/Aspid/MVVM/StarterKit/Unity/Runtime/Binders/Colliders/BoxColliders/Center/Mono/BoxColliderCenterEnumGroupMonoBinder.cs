using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupVector3MonoBinder{BoxCollider}"/> that sets the <see cref="BoxCollider.center"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Center EnumGroup")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    public sealed class BoxColliderCenterEnumGroupMonoBinder : EnumGroupVector3MonoBinder<BoxCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(BoxCollider element, Vector3 value) =>
            element.center = value;
    }
}