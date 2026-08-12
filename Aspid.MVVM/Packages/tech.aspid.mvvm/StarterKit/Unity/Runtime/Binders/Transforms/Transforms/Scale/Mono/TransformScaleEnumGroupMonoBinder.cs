using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupVector3MonoBinder{Transform}"/> that sets the <see cref="Transform.localScale"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale EnumGroup")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale", SubPath = "EnumGroup")]
    public sealed class TransformScaleEnumGroupMonoBinder : EnumGroupVector3MonoBinder<Transform>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets the <see cref="Transform.localScale"/> of the element to the resolved value.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(Transform element, Vector3 value) =>
            element.localScale = value;
    }
}