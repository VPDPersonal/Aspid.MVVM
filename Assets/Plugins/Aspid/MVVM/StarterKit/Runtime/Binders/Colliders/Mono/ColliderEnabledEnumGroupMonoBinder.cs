using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - Enabled EnumGroup")]
    public sealed class ColliderEnabledEnumGroupMonoBinder : EnumGroupMonoBinder<Collider>
    {
        [Header("Parameters")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;
        
        protected override void SetDefaultValue(Collider element) =>
            element.enabled = _defaultValue;

        protected override void SetSelectedValue(Collider element) =>
            element.enabled = _selectedValue;
    }
}