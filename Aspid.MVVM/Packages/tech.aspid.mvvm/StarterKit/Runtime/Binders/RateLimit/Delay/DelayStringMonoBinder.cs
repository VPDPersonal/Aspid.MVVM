using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="DelayMonoBinder{TValue}"/> that forwards every <see langword="string"/> after the interval.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Delay Binder – String")]
    public sealed class DelayStringMonoBinder : DelayMonoBinder<string> { }
}
