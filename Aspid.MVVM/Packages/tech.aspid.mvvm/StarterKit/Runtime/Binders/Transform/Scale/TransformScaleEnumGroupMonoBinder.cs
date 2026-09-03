using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Transform.localScale"/>
    /// on each element.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale EnumGroup")]
    public sealed class TransformScaleEnumGroupMonoBinder : EnumGroupMonoBinder<Transform, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Transform element, Vector3 value)
        {
            if (this.RequireFinite(value))
                element.localScale = value;
        }
    }
}
