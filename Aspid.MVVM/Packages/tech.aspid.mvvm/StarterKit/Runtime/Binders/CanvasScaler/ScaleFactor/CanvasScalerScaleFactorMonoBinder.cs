using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="CanvasScaler.scaleFactor"/>.
    /// </summary>
    /// <remarks>
    /// Applies only in <see cref="CanvasScaler.ScaleMode.ConstantPixelSize"/>. The value is raised to Unity's own
    /// floor of 0.01.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasScaler), serializePropertyNames: "m_ScaleFactor")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Scale Factor")]
    public class CanvasScalerScaleFactorMonoBinder : ComponentFloatMonoBinder<CanvasScaler>
    {
        private const float MinScaleFactor = 0.01f;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.scaleFactor;
            set => CachedComponent.scaleFactor = this.SafeClamp(value, MinScaleFactor, float.MaxValue);
        }
    }
}
