using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{ScrollRect, Vector2}"/> that binds <see cref="ScrollRect.normalizedPosition"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Normalized Position")]
    public class ScrollRectNormalizedPositionMonoBinder : ComponentMonoBinder<ScrollRect, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.normalizedPosition;
            set => CachedComponent.normalizedPosition = new Vector2(this.SafeClamp01(value.x), this.SafeClamp01(value.y));
        }
    }
}
