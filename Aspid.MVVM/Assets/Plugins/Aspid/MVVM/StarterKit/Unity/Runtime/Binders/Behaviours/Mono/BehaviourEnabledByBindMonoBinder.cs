using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled By Bind")]
    [BindModeOverride(modes: BindMode.OneTime)]
    public sealed partial class BehaviourEnabledByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [SerializeField] private bool _isInvert;

        protected override void OnBound() => 
            SetEnable();

        protected override void OnUnbound() => 
            SetEnable();
        
        [BinderLog]
        public void SetValue<T>(T value) { }
        
        private void SetEnable() =>
            enabled = _isInvert ? !IsBound : IsBound;
    }
}