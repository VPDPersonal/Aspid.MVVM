using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Rigidbody2D}"/> that binds <see cref="Rigidbody2D.mass"/>.
    /// </summary>
    /// <remarks>A non-finite value is ignored, keeping the last mass that was successfully applied.</remarks>
    [GenerateSerializableBinder]
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
                if (!this.RequireFinite(value)) return;
                CachedComponent.mass = value;
            }
        }
    }
}
