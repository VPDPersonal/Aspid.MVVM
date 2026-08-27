using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{ScrollRect}"/> that binds <see cref="ScrollRect.horizontal"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Horizontal")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Horizontal Enabled")]
    public class ScrollRectHorizontalMonoBinder : ComponentBoolMonoBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.horizontal;
            set => CachedComponent.horizontal = value;
        }
    }
}
