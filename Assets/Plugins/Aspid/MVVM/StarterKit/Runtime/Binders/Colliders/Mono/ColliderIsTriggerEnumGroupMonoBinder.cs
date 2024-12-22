using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/Collider Binder - IsTrigger EnumGroup")]
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