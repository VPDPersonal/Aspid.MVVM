using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder - Enabled EnumGroup")]
    [AddComponentContextMenu(typeof(Behaviour),"Add Behaviour Binder/Behaviour Binder - Enabled EnumGroup")]
    public sealed class BehaviourEnabledEnumGroupMonoBinder : EnumGroupMonoBinder<Behaviour>
    {
        [Header("Values")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;
        
        protected override void SetDefaultValue(Behaviour element) =>
            element.enabled = _defaultValue;

        protected override void SetSelectedValue(Behaviour element) =>
            element.enabled = _selectedValue;
    }
}