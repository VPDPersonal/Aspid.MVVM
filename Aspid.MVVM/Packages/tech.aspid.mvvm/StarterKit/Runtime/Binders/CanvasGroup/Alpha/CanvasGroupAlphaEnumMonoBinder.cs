using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="CanvasGroup.alpha"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha Enum")]
    public sealed class CanvasGroupAlphaEnumMonoBinder : EnumMonoBinder<CanvasGroup, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.alpha = this.SafeClamp01(value);
    }
}
