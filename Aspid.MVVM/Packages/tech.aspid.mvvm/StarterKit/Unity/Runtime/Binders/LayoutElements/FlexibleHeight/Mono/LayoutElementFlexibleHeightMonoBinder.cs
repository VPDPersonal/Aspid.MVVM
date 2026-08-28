using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{LayoutElement}"/> that binds <see cref="LayoutElement.flexibleHeight"/>.
    /// </summary>
    /// <remarks>A non-finite value is rejected instead of being written.</remarks>
    [AddBinderContextMenu(typeof(LayoutElement), serializePropertyNames: "m_FlexibleHeight")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutElement/LayoutElement Binder – Flexible Height")]
    public class LayoutElementFlexibleHeightMonoBinder : ComponentFloatMonoBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.flexibleHeight;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.flexibleHeight = value;
            }
        }
    }
}
