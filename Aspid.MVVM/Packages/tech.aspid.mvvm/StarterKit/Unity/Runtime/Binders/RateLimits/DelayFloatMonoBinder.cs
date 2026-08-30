using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="DelayMonoBinder{T}">DelayMonoBinder&lt;float&gt;</see> that forwards every value, late for a number.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Delay Binder – Float")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class DelayFloatMonoBinder : DelayMonoBinder<float>, IFloatBinder { }
}
