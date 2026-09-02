using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="DelayMonoBinder{T}">DelayMonoBinder&lt;string&gt;</see> that forwards every value, late for a string.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Delay Binder – String")]
    [AddBinderContextMenuByType(typeof(string))]
    public sealed partial class DelayStringMonoBinder : DelayMonoBinder<string>
    {    }
}
