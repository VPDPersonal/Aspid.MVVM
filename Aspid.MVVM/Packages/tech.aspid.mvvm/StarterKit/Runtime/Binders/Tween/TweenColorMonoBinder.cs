using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TweenMonoBinder{TValue}"/> that eases a <see cref="Color"/>.
    /// </summary>
    /// <remarks>
    /// Interpolation is linear in the given color space, alpha included.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(Color))]
    [AddComponentMenu("Aspid/MVVM/Binders/Tween/Tween Binder – Color")]
    public sealed partial class TweenColorMonoBinder : TweenMonoBinder<Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected override Color Interpolate(Color from, Color to, float progress) =>
            Color.LerpUnclamped(from, to, progress);
    }
}
