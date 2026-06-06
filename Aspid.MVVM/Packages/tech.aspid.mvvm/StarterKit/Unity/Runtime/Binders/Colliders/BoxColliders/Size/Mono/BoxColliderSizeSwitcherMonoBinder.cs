using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3MonoBinder{BoxCollider}"/> that switches the <see cref="BoxCollider.size"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size Switcher")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size", SubPath = "Switcher")]
    public sealed class BoxColliderSizeSwitcherMonoBinder : SwitcherVector3MonoBinder<BoxCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.size = value;
    }
}