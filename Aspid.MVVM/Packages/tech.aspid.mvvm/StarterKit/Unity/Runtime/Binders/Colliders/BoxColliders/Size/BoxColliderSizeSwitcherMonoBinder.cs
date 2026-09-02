using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{BoxCollider, Vector3}"/> that switches the <see cref="BoxCollider.size"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size Switcher")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size", SubPath = "Switcher")]
    public sealed class BoxColliderSizeSwitcherMonoBinder : SwitcherMonoBinder<BoxCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.size = this.NonNegative(value);
    }
}