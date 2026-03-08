using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Enum binder that sets the <see cref="Collider.enabled"/> property on a cached
    /// <see cref="Collider"/> component to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Enabled Enum")]
    public sealed class ColliderEnabledEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}