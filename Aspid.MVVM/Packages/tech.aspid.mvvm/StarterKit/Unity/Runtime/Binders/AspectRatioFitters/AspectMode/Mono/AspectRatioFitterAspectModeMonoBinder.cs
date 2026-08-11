using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;AspectRatioFitter, AspectRatioFitter.AspectMode&gt;</see> that binds
    /// <see cref="AspectRatioFitter.aspectMode"/>.
    /// </summary>
    /// <remarks>
    /// How the element reconciles its aspect ratio with the space it is given — fit inside, envelope, or
    /// follow one axis. A video or a portrait that can arrive in any shape decides this at runtime, from the
    /// shape it actually got.
    /// </remarks>
    [AddBinderContextMenu(typeof(AspectRatioFitter), serializePropertyNames: "m_AspectMode")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/AspectRatioFitter/AspectRatioFitter Binder – Aspect Mode")]
    public class AspectRatioFitterAspectModeMonoBinder : ComponentMonoBinder<AspectRatioFitter, AspectRatioFitter.AspectMode>
    {
        /// <inheritdoc/>
        protected sealed override AspectRatioFitter.AspectMode Property
        {
            get => CachedComponent.aspectMode;
            set => CachedComponent.aspectMode = value;
        }
    }
}
