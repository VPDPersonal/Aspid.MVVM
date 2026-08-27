using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;ContentSizeFitter, ContentSizeFitter.FitMode&gt;</see> that binds
    /// <see cref="ContentSizeFitter.horizontalFit"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(ContentSizeFitter), serializePropertyNames: "m_HorizontalFit")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ContentSizeFitter/ContentSizeFitter Binder – Horizontal Fit")]
    public class ContentSizeFitterHorizontalFitMonoBinder : ComponentMonoBinder<ContentSizeFitter, ContentSizeFitter.FitMode>
    {
        /// <inheritdoc/>
        protected sealed override ContentSizeFitter.FitMode Property
        {
            get => CachedComponent.horizontalFit;
            set => CachedComponent.horizontalFit = value;
        }
    }
}
