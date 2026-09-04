using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="ContentSizeFitter.verticalFit"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(ContentSizeFitter), serializePropertyNames: "m_VerticalFit")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ContentSizeFitter/ContentSizeFitter Binder – Vertical Fit")]
    public class ContentSizeFitterVerticalFitMonoBinder
        : ComponentMonoBinder<ContentSizeFitter, ContentSizeFitter.FitMode>
    {
        /// <inheritdoc/>
        protected sealed override ContentSizeFitter.FitMode Property
        {
            get => CachedComponent.verticalFit;
            set => CachedComponent.verticalFit = value;
        }
    }
}
