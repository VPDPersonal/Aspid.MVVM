using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="Rigidbody.useGravity"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Rigidbody), serializePropertyNames: "m_UseGravity")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody Binder – Use Gravity")]
    public class RigidbodyUseGravityMonoBinder : ComponentMonoBinder<Rigidbody, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.useGravity;
            set => CachedComponent.useGravity = value;
        }
    }
}
