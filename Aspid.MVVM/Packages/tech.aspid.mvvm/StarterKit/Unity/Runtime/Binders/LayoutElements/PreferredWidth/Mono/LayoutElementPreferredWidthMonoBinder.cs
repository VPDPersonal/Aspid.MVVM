using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder<LayoutElement>"/> that binds <see cref="LayoutElement.preferredWidth"/>.
    /// </summary>
    /// <remarks>
    /// A LayoutElement is how a single child overrides what its layout group would otherwise decide, and none of its numbers could be bound. A negative value means "no preference", which is why it is passed through rather than clamped.
    /// </remarks>
    [AddBinderContextMenu(typeof(LayoutElement), serializePropertyNames: "m_PreferredWidth")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutElement/LayoutElement Binder – Preferred Width")]
    public class LayoutElementPreferredWidthMonoBinder : ComponentFloatMonoBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.preferredWidth;
            set => CachedComponent.preferredWidth = value;
        }
    }
}
