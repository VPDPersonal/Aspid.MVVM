using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger EnumGroup")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger", SubPath = "EnumGroup")]
    public sealed class ColliderIsTriggerEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, bool>
    {
        protected override void SetValue(Collider element, bool value) =>
            element.isTrigger = value;
    }
}