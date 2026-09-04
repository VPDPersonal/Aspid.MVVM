using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConditionalMonoBinder{TValue}"/> for <see langword="string"/> values.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/Conditional/Conditional Binder – String")]
    public sealed class ConditionalStringMonoBinder : ConditionalMonoBinder<string> { }
}
