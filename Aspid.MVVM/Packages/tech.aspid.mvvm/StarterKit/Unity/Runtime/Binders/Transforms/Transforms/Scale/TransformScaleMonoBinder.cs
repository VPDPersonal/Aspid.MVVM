using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Transform, Vector3}"/> that sets the <see cref="Transform.localScale"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale")]
    public class TransformScaleMonoBinder : ComponentMonoBinder<Transform, Vector3>, IVector3Binder
    {
        protected sealed override Vector3 Property
        {
            get => CachedComponent.localScale;
            set => CachedComponent.localScale = value;
        }
    }
}