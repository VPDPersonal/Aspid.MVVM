using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ConditionalMonoBinder{T}">ConditionalMonoBinder&lt;Color&gt;</see> that chooses between two
    /// configured Color values.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Conditional/Conditional Binder – Color")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ConditionalColorMonoBinder : ConditionalMonoBinder<Color> { }
}
