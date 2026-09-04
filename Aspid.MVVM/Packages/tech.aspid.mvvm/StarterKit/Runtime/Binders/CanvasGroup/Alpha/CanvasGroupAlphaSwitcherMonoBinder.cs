using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="CanvasGroup.alpha"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha Switcher")]
    public sealed class CanvasGroupAlphaSwitcherMonoBinder : SwitcherMonoBinder<CanvasGroup, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.alpha = this.SafeClamp01(value);
    }
}
