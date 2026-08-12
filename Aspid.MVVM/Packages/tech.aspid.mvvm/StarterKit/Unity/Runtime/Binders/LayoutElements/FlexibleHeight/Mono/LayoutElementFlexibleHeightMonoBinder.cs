using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder<LayoutElement>"/> that binds <see cref="LayoutElement.flexibleHeight"/>.
    /// </summary>
    /// <remarks>
    /// The vertical counterpart of the flexible width.
    /// </remarks>
    [AddBinderContextMenu(typeof(LayoutElement), serializePropertyNames: "m_FlexibleHeight")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutElement/LayoutElement Binder – Flexible Height")]
    public class LayoutElementFlexibleHeightMonoBinder : ComponentFloatMonoBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.flexibleHeight;
            set => CachedComponent.flexibleHeight = value;
        }
    }
}
