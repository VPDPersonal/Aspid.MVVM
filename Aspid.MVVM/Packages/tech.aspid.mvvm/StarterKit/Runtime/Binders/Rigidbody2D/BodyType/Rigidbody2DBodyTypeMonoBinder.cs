using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Rigidbody2D.bodyType"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Rigidbody2D), serializePropertyNames: "m_BodyType")]
    [AddComponentMenu("Aspid/MVVM/Binders/Physics/Rigidbody2D Binder – Body Type")]
    public class Rigidbody2DBodyTypeMonoBinder : ComponentMonoBinder<Rigidbody2D, RigidbodyType2D>
    {
        /// <inheritdoc/>
        protected sealed override RigidbodyType2D Property
        {
            get => CachedComponent.bodyType;
            set => CachedComponent.bodyType = value;
        }
    }
}
