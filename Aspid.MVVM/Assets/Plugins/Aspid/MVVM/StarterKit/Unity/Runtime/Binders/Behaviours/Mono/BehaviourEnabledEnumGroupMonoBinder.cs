using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{Behaviour, bool}"/> that sets the <see cref="Behaviour.enabled"/>
    /// property on each <see cref="Behaviour"/> in the group based on the bound enum ViewModel value.
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