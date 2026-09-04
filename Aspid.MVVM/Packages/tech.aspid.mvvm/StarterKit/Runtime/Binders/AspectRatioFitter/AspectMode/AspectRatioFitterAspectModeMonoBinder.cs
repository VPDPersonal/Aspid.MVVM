using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="AspectRatioFitter.aspectMode"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AspectRatioFitter), serializePropertyNames: "m_AspectMode")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/AspectRatioFitter/AspectRatioFitter Binder – Aspect Mode")]
    public class AspectRatioFitterAspectModeMonoBinder
        : ComponentMonoBinder<AspectRatioFitter, AspectRatioFitter.AspectMode>
    {
        /// <inheritdoc/>
        protected sealed override AspectRatioFitter.AspectMode Property
        {
            get => CachedComponent.aspectMode;
            set => CachedComponent.aspectMode = value;
        }
    }
}
