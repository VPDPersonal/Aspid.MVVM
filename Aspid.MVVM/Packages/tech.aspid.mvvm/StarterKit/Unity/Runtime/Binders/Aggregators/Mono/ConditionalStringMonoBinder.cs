using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ConditionalMonoBinder{T}">ConditionalMonoBinder&lt;string&gt;</see> that chooses between two
    /// configured string values.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Conditional/Conditional Binder – String")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ConditionalStringMonoBinder : ConditionalMonoBinder<string> { }
}
