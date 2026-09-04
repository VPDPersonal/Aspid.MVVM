using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Canvas.overrideSorting"/>.
    /// </summary>
    /// <remarks>
    /// Unity ignores this on a root canvas.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Canvas), serializePropertyNames: "m_OverrideSorting")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas/Canvas Binder – Override Sorting")]
    public class CanvasOverrideSortingMonoBinder : ComponentMonoBinder<Canvas, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.overrideSorting;
            set => CachedComponent.overrideSorting = value;
        }
    }
}
