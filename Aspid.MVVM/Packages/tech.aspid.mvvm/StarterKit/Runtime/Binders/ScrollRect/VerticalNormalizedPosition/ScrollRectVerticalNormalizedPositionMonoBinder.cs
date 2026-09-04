using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds
    /// <see cref="ScrollRect.verticalNormalizedPosition"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Vertical Scroll")]
    public class ScrollRectVerticalNormalizedPositionMonoBinder : ComponentFloatMonoBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.verticalNormalizedPosition;
            set => CachedComponent.verticalNormalizedPosition = this.SafeClamp01(value);
        }
    }
}
