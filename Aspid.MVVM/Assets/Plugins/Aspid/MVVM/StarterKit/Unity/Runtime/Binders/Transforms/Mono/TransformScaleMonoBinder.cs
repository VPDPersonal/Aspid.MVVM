using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale")]
    public class TransformScaleMonoBinder : ComponentVector3MonoBinder<Transform>
    {
        protected sealed override Vector3 Property
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }
    }
}