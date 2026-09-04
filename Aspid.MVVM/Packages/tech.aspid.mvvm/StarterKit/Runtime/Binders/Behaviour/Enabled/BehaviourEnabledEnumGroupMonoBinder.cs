using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Behaviour.enabled"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(Behaviour), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled EnumGroup")]
    public sealed class BehaviourEnabledEnumGroupMonoBinder : EnumGroupMonoBinder<Behaviour, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Behaviour element, bool value) =>
            element.enabled = value;
    }
}
