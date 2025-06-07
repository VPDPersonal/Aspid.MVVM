using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Collider), "m_IsTrigger")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - IsTrigger EnumGroup")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - IsTrigger EnumGroup")]
    public sealed class ColliderIsTriggerEnumGroupMonoBinder : EnumGroupMonoBinder<Collider>
    {
        [Header("Parameters")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;
        
        protected override void SetDefaultValue(Collider element) =>
            element.isTrigger = _defaultValue;

        protected override void SetSelectedValue(Collider element) =>
            element.isTrigger = _selectedValue;
    }
}