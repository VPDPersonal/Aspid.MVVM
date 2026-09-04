using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="LayoutElement.preferredHeight"/>.
    /// </summary>
    /// <remarks>
    /// A negative value means no preference and is kept; a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.preferredHeight = value;
            }
        }
    }
}
