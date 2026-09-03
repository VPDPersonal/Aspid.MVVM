using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Transform.localScale"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale Enum")]
    public sealed class TransformScaleEnumMonoBinder : EnumMonoBinder<Transform, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value)
        {
            if (this.RequireFinite(value))
                CachedComponent.localScale = value;
        }
    }
}
