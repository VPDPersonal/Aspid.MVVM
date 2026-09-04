using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="LayoutElement.preferredWidth"/>.
    /// </summary>
    /// <remarks>
    /// A negative value means no preference and is kept; a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.preferredWidth = value;
            }
        }
    }
}
