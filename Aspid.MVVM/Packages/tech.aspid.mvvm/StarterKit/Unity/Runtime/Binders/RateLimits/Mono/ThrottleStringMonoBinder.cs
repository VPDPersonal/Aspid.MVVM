using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ThrottleMonoBinder{T}">ThrottleMonoBinder&lt;string&gt;</see> that lets at most one value through per interval for a string.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Throttle Binder – String")]
    [AddBinderContextMenuByType(typeof(string))]
    public sealed partial class ThrottleStringMonoBinder : ThrottleMonoBinder<string>
    {    }
}
