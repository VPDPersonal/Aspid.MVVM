using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="DelayMonoBinder{TValue}"/> that forwards every <see langword="float"/> after the interval.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Delay Binder – Float")]
    public sealed class DelayFloatMonoBinder : DelayMonoBinder<float>, IFloatBinder { }
}
