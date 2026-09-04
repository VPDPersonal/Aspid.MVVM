using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds
    /// <see cref="CanvasScaler.referenceResolution"/>.
    /// </summary>
    /// <remarks>
    /// Applies only in <see cref="CanvasScaler.ScaleMode.ScaleWithScreenSize"/>. Each component is raised to at
    /// least one.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasScaler), serializePropertyNames: "m_ReferenceResolution")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Reference Resolution")]
    public class CanvasScalerReferenceResolutionMonoBinder : ComponentMonoBinder<CanvasScaler, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.referenceResolution;
            set => CachedComponent.referenceResolution = new Vector2(
                this.SafeClamp(value.x, 1f, float.MaxValue),
                this.SafeClamp(value.y, 1f, float.MaxValue));
        }
    }
}
