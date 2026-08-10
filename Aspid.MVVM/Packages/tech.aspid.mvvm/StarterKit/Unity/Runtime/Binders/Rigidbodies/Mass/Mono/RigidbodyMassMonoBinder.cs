using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Rigidbody}"/> that binds <see cref="Rigidbody.mass"/>.
    /// </summary>
    /// <remarks>
    /// Physics had no binders at all, while every 3D collider was covered.
    /// <para>
    /// A non-finite mass is dropped rather than written. Unity clamps the range on its own — zero, a negative and
    /// an infinity all land on a legal mass — but <see cref="float.NaN"/> is stored verbatim, and a body with a NaN
    /// mass leaves the simulation without a word. Dropping the write keeps the body on the last mass that worked,
    /// which is what <see cref="Rigidbody2D"/> does natively — its binder drops it too, so the pair behaves alike.
    /// </para>
    /// </remarks>
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
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.mass = value;
            }
        }
    }
}
