using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Enum binder that sets the <see cref="Collider.isTrigger"/> property on a cached
    /// <see cref="Collider"/> component to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger Enum")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger", SubPath = "Enum")]
    public sealed class ColliderIsTriggerEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.isTrigger = value;
    }
}