using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Rigidbody.mass"/>.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Rigidbody), serializePropertyNames: "m_Mass")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody Binder – Mass")]
    public class RigidbodyMassMonoBinder : ComponentFloatMonoBinder<Rigidbody>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.mass;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.mass = value;
            }
        }
    }
}
