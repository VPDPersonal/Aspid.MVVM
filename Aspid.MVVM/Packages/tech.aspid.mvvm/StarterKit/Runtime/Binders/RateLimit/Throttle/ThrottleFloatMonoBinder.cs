using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ThrottleMonoBinder{TValue}"/> that forwards at most one <see langword="float"/> per interval.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Throttle Binder – Float")]
    public sealed class ThrottleFloatMonoBinder : ThrottleMonoBinder<float>, IFloatBinder { }
}
