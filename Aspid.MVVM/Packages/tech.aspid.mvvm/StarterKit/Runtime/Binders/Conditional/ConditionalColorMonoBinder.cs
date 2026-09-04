using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConditionalMonoBinder{TValue}"/> for <see cref="Color"/> values.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/Conditional/Conditional Binder – Color")]
    public sealed class ConditionalColorMonoBinder : ConditionalMonoBinder<Color> { }
}
