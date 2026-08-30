using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="DebounceMonoBinder{T}">DebounceMonoBinder&lt;string&gt;</see> that holds a value until the values stop for a string.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Debounce Binder – String")]
    [AddBinderContextMenuByType(typeof(string))]
    public sealed partial class DebounceStringMonoBinder : DebounceMonoBinder<string>
    {    }
}
