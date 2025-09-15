using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - Enabled EnumGroup")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - Enabled EnumGroup")]
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