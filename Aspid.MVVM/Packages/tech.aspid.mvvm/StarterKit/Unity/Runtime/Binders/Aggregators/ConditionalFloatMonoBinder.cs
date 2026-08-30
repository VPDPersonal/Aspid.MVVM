using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ConditionalMonoBinder{T}">ConditionalMonoBinder&lt;float&gt;</see> that chooses between two
    /// configured float values.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Conditional/Conditional Binder – Float")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ConditionalFloatMonoBinder : ConditionalMonoBinder<float> { }
}
