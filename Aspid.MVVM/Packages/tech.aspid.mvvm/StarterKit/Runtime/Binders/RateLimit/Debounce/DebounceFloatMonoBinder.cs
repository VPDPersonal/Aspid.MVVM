using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="DebounceMonoBinder{TValue}"/> that forwards the last <see langword="float"/> once the values stop
    /// arriving.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Debounce Binder – Float")]
    public sealed class DebounceFloatMonoBinder : DebounceMonoBinder<float>, IFloatBinder { }
}
