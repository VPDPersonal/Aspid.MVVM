using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{Collider, bool}"/> that sets the <see cref="Collider.enabled"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Enabled Enum")]
    public sealed class ColliderEnabledEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}