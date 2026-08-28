using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{LayoutElement}"/> that binds <see cref="LayoutElement.preferredHeight"/>.
    /// </summary>
    /// <remarks>
    /// A negative value means "no preference". A non-finite value is rejected instead of being written.
    /// </remarks>
    [AddBinderContextMenu(typeof(LayoutElement), serializePropertyNames: "m_PreferredHeight")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutElement/LayoutElement Binder – Preferred Height")]
    public class LayoutElementPreferredHeightMonoBinder : ComponentFloatMonoBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.preferredHeight;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.preferredHeight = value;
            }
        }
    }
}
