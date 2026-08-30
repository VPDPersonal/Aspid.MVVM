using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{CapsuleCollider}"/> that binds <see cref="CapsuleCollider.direction"/>.
    /// </summary>
    /// <remarks>
    /// Clamped to 0–2 (X/Y/Z); Unity silently treats an out-of-range value as 0.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Direction")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Direction")]
    public class CapsuleColliderDirectionMonoBinder : ComponentIntMonoBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.direction;
            set => CachedComponent.direction = Mathf.Clamp(value, 0, 2);
        }
    }
}
