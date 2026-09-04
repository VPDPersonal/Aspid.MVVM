using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="AspectRatioFitter.aspectRatio"/>.
    /// </summary>
    /// <remarks>
    /// Unity clamps the ratio to [0.001, 1000] but lets NaN through, so a non-finite value is refused here.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.aspectRatio = value;
            }
        }
    }
}
