using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="DebounceMonoBinder{TValue}"/> that forwards the last <see langword="string"/> once the values stop
    /// arriving.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/RateLimit/Debounce Binder – String")]
    public sealed class DebounceStringMonoBinder : DebounceMonoBinder<string> { }
}
