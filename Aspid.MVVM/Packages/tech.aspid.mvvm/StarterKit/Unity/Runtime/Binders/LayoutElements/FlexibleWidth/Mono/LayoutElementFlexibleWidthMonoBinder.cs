using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{LayoutElement}"/> that binds <see cref="LayoutElement.flexibleWidth"/>.
    /// </summary>
    /// <remarks>A non-finite value is rejected instead of being written.</remarks>
    [AddBinderContextMenu(typeof(LayoutElement), serializePropertyNames: "m_FlexibleWidth")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutElement/LayoutElement Binder – Flexible Width")]
    public class LayoutElementFlexibleWidthMonoBinder : ComponentFloatMonoBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.flexibleWidth;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.flexibleWidth = value;
            }
        }
    }
}
