using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector3MonoBinder{Transform}"/> that sets the <see cref="Transform.localScale"/> property.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale")]
    public class TransformScaleMonoBinder : ComponentVector3MonoBinder<Transform>
    {
        protected sealed override Vector3 Property
        {
            get => CachedComponent.localScale;
            set => CachedComponent.localScale = value;
        }
    }
}