using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Rigidbody2D}"/> that binds <see cref="Rigidbody2D.mass"/>.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart, and it drops a non-finite mass for the same reason the 3D one does — but not for the
    /// same effect. Unity refuses such a mass here on its own and logs an error naming the object; the 3D body
    /// accepts it in silence. Dropping the write in both keeps the pair behaving alike: a ViewModel that produces
    /// <see cref="float.NaN"/> leaves the body on its last good mass either way, without the console filling up
    /// in one dimension and staying quiet in the other. The range needs no guard — Unity clamps it itself.
    /// </remarks>
    [AddBinderContextMenu(typeof(Rigidbody2D), serializePropertyNames: "m_Mass")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody2D Binder – Mass")]
    public class Rigidbody2DMassMonoBinder : ComponentFloatMonoBinder<Rigidbody2D>
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
