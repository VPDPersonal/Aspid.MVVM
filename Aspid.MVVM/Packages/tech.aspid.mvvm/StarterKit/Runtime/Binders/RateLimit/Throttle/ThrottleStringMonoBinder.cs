using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ThrottleMonoBinder{TValue}"/> that forwards at most one <see langword="string"/> per interval.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Throttle Binder – String")]
    public sealed class ThrottleStringMonoBinder : ThrottleMonoBinder<string> { }
}
