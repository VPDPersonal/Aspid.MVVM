using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;Rigidbody, RigidbodyConstraints&gt;</see> that binds
    /// <see cref="Rigidbody.constraints"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Rigidbody))]
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
