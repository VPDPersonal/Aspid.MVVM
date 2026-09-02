using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="TweenMonoBinder{T}">TweenMonoBinder&lt;Color&gt;</see> that eases a colour toward each value
    /// it receives.
    /// </summary>
    /// <remarks>Interpolation is linear in the colour space the values are already in, alpha included.</remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Tween/Tween Binder – Color")]
    [AddBinderContextMenuByType(typeof(Color))]
    public sealed partial class TweenColorMonoBinder : TweenMonoBinder<Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected override Color Interpolate(Color from, Color to, float progress) =>
            Color.LerpUnclamped(from, to, progress);
    }
}
