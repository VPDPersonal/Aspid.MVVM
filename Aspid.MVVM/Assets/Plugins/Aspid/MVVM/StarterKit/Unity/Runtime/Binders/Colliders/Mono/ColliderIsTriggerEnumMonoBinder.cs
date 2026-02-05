using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger Enum")]
    public sealed class ColliderIsTriggerEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.isTrigger = value;
    }
}