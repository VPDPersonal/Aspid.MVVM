using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{Behaviour, bool}"/> that sets the <see cref="Behaviour.enabled"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Behaviour), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled Enum")]
    public sealed class BehaviourEnabledEnumMonoBinder : EnumMonoBinder<Behaviour, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}