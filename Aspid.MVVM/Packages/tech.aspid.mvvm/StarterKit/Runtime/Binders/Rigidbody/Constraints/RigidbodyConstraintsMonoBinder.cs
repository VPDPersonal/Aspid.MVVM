using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Rigidbody.constraints"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Rigidbody), serializePropertyNames: "m_Constraints")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody Binder – Constraints")]
    public class RigidbodyConstraintsMonoBinder : ComponentMonoBinder<Rigidbody, RigidbodyConstraints>
    {
        /// <inheritdoc/>
        protected sealed override RigidbodyConstraints Property
        {
            get => CachedComponent.constraints;
            set => CachedComponent.constraints = value;
        }
    }
}
