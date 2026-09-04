using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="LayoutElement.flexibleWidth"/>.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.flexibleWidth = value;
            }
        }
    }
}
