using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumVector3MonoBinder{Transform}"/> that sets the <see cref="Transform.localScale"/> property
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale Enum")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale", SubPath = "Enum")]
    public sealed class TransformScaleEnumMonoBinder : EnumVector3MonoBinder<Transform>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the <see cref="Transform.localScale"/> to the resolved value.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            transform.localScale = value;
    }
}