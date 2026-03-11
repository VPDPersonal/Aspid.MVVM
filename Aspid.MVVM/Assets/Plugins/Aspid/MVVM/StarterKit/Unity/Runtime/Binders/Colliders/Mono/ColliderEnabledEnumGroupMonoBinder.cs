using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{Collider, bool}"/> that sets the <see cref="Collider.enabled"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Enabled EnumGroup")]
    public sealed class ColliderEnabledEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Collider element, bool value) =>
            element.enabled = value;
    }
}