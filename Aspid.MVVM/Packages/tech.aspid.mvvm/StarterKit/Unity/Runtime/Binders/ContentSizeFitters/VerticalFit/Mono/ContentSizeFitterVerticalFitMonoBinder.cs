using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;ContentSizeFitter, ContentSizeFitter.FitMode&gt;</see> that binds
    /// <see cref="ContentSizeFitter.verticalFit"/>.
    /// </summary>
    /// <remarks>
    /// Whether the element sizes itself to its content on this axis. A dialog that grows to fit a message
    /// and then stops growing has to turn the fit off, and turning it off was not bindable —
    /// <see cref="ContentSizeFitter.FitMode.Unconstrained"/> is what hands the axis back to the layout.
    /// </remarks>
    [AddBinderContextMenu(typeof(ContentSizeFitter), serializePropertyNames: "m_VerticalFit")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ContentSizeFitter/ContentSizeFitter Binder – Vertical Fit")]
    public class ContentSizeFitterVerticalFitMonoBinder : ComponentMonoBinder<ContentSizeFitter, ContentSizeFitter.FitMode>
    {
        /// <inheritdoc/>
        protected sealed override ContentSizeFitter.FitMode Property
        {
            get => CachedComponent.verticalFit;
            set => CachedComponent.verticalFit = value;
        }
    }
}
