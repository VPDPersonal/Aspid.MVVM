using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AspectRatioFitter}"/> that binds <see cref="AspectRatioFitter.aspectRatio"/>.
    /// </summary>
    /// <remarks>
    /// The ratio itself — the width of the image the ViewModel just loaded, divided by its height. Unity
    /// clamps the range; a non-finite value is refused here, because Unity's clamp is written as comparisons
    /// and every comparison against NaN is false.
    /// <para/>
    /// Bind <see cref="AspectRatioFitter.aspectMode"/> too, or set it in the Inspector: while it is
    /// <see cref="AspectRatioFitter.AspectMode.None"/>, the fitter recomputes the ratio from the element's current
    /// rect on every layout pass outside play mode, and a written value does not survive.
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
                // Unity сама зажимает соотношение в 0.001..1000, но NaN проходит сквозь её Clamp
                // (любое сравнение с NaN ложно) и обнуляет размер элемента.
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.aspectRatio = value;
            }
        }
    }
}
