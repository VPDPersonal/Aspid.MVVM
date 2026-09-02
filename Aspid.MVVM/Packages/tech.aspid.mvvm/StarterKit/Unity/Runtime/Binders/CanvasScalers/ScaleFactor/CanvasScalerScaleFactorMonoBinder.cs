using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{CanvasScaler}"/> that binds <see cref="CanvasScaler.scaleFactor"/>.
    /// </summary>
    /// <remarks>
    /// Only read when <see cref="CanvasScaler.uiScaleMode"/> is <see cref="CanvasScaler.ScaleMode.ConstantPixelSize"/>.
    /// Clamped to the same floor Unity applies in its own setter, <c>0.01</c> — a non-finite value lands there
    /// rather than reaching the scaler.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasScaler), serializePropertyNames: "m_ScaleFactor")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Scale Factor")]
    public class CanvasScalerScaleFactorMonoBinder : ComponentFloatMonoBinder<CanvasScaler>
    {
        /// <summary>
        /// The smallest scale Unity's own setter accepts; anything below it is raised to this value.
        /// </summary>
        private const float MinimumScaleFactor = 0.01f;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.scaleFactor;
            set => CachedComponent.scaleFactor = this.SafeClamp(value, MinimumScaleFactor, float.MaxValue);
        }
    }
}
