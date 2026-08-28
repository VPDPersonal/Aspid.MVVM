using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="DebounceMonoBinder{T}">DebounceMonoBinder&lt;float&gt;</see> that holds a value until the values stop for a number.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Debounce Binder – Float")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class DebounceFloatMonoBinder : DebounceMonoBinder<float>, IFloatBinder { }
}
