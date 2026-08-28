using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{LayoutElement}"/> that binds <see cref="LayoutElement.preferredWidth"/>.
    /// </summary>
    /// <remarks>
    /// A negative value means "no preference", which is why it is passed through rather than clamped. A
    /// non-finite value is rejected instead of being written.
    /// </remarks>
    [AddBinderContextMenu(typeof(LayoutElement), serializePropertyNames: "m_PreferredWidth")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutElement/LayoutElement Binder – Preferred Width")]
    public class LayoutElementPreferredWidthMonoBinder : ComponentFloatMonoBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.preferredWidth;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.preferredWidth = value;
            }
        }
    }
}
