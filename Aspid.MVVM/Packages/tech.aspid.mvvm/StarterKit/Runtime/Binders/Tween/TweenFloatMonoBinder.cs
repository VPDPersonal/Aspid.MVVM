using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TweenMonoBinder{TValue}"/> that eases a <see langword="float"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/Tween/Tween Binder – Float")]
    public sealed partial class TweenFloatMonoBinder : TweenMonoBinder<float>, IFloatBinder
    {
        /// <inheritdoc/>
        protected override float Interpolate(float from, float to, float progress) =>
            Mathf.LerpUnclamped(from, to, progress);
    }
}
