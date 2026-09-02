using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{BoxCollider, Vector3}"/> that sets the <see cref="BoxCollider.center"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Center Enum")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Center", SubPath = "Enum")]
    public sealed class BoxColliderCenterEnumMonoBinder : EnumMonoBinder<BoxCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = value;
    }
}