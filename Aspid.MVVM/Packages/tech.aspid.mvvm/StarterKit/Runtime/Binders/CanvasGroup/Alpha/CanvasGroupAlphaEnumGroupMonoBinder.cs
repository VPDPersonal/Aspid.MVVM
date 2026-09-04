using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets
    /// <see cref="CanvasGroup.alpha"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha EnumGroup")]
    public sealed class CanvasGroupAlphaEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(CanvasGroup element, float value) =>
            element.alpha = this.SafeClamp01(value);
    }
}
