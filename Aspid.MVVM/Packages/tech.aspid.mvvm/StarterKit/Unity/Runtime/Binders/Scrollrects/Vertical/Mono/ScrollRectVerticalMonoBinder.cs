using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder<ScrollRect>"/> that binds <see cref="ScrollRect.vertical"/>.
    /// </summary>
    /// <remarks>
    /// Whether the user may scroll vertically.
    /// </remarks>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Vertical")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Vertical Enabled")]
    public class ScrollRectVerticalMonoBinder : ComponentBoolMonoBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.vertical;
            set => CachedComponent.vertical = value;
        }
    }
}
