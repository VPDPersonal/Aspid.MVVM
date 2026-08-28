using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AspectRatioFitter}"/> that binds <see cref="AspectRatioFitter.aspectRatio"/>.
    /// </summary>
    /// <remarks>
    /// Unity clamps the ratio to 0.001..1000, but a NaN value passes that clamp unchanged (every
    /// comparison against NaN is false) and is rejected here instead.
    /// <para/>
    /// While <see cref="AspectRatioFitter.aspectMode"/> is <see cref="AspectRatioFitter.AspectMode.None"/>, the
    /// fitter recomputes the ratio from the element's current rect on every layout pass outside play mode, and a
    /// written value does not survive.
    /// </remarks>
    [AddBinderContextMenu(typeof(AspectRatioFitter), serializePropertyNames: "m_AspectRatio")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/AspectRatioFitter/AspectRatioFitter Binder – Aspect Ratio")]
    public class AspectRatioFitterAspectRatioMonoBinder : ComponentFloatMonoBinder<AspectRatioFitter>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.aspectRatio;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.aspectRatio = value;
            }
        }
    }
}
