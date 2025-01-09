using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Behaviour/Behaviour Binder - Enabled EnumGroup")]
    public sealed class BehaviourEnabledEnumGroupMonoBinder : EnumGroupMonoBinder<Behaviour>
    {
        [Header("Parameters")]
        [SerializeField] private bool _defaultValue;
        [SerializeField] private bool _selectedValue;
        
        protected override void SetDefaultValue(Behaviour element) =>
            element.enabled = _defaultValue;

        protected override void SetSelectedValue(Behaviour element) =>
            element.enabled = _selectedValue;
    }
}