using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder<Canvas>"/> that binds <see cref="Canvas.overrideSorting"/>.
    /// </summary>
    /// <remarks>
    /// Whether this canvas sorts independently of its parent — the switch that makes the sorting order above take effect on a nested canvas. Unity ignores it on a root canvas, which already sorts
    /// independently, so binding it there has no observable effect.
    /// </remarks>
    [AddBinderContextMenu(typeof(Canvas), serializePropertyNames: "m_OverrideSorting")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas/Canvas Binder – Override Sorting")]
    public class CanvasOverrideSortingMonoBinder : ComponentBoolMonoBinder<Canvas>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.overrideSorting;
            set => CachedComponent.overrideSorting = value;
        }
    }
}
