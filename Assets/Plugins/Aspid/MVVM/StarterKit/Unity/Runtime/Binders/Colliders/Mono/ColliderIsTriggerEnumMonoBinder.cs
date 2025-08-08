using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Collider), "m_IsTrigger")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - IsTrigger Enum")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - IsTrigger Enum")]
    public sealed class ColliderIsTriggerEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.isTrigger = value;
    }
}