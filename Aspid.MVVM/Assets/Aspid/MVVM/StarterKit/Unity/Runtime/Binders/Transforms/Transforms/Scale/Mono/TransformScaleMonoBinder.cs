using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector3MonoBinder{Transform}"/> that sets the <see cref="Transform.localScale"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current local scale
    /// is sent back to the ViewModel.
    /// </remarks>
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