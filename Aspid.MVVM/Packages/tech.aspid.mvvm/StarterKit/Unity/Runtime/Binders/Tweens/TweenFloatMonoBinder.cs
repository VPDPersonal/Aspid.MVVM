using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="TweenMonoBinder{T}">TweenMonoBinder&lt;float&gt;</see> that also implements
    /// <see cref="IFloatBinder"/>, easing a number toward each value it receives.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Tween/Tween Binder – Float")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class TweenFloatMonoBinder : TweenMonoBinder<float>, IFloatBinder
    {
        /// <inheritdoc/>
        protected override float Interpolate(float from, float to, float progress) =>
            Mathf.LerpUnclamped(from, to, progress);
    }
}
