using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ThrottleMonoBinder{T}">ThrottleMonoBinder&lt;float&gt;</see> that lets at most one value through per interval for a number.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Throttle Binder – Float")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class ThrottleFloatMonoBinder : ThrottleMonoBinder<float>, IFloatBinder { }
}
