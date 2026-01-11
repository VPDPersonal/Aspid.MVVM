using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [BindModeOverride(modes: BindMode.OneTime)]
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled By Bind")]
    public sealed class BehaviourEnabledByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [SerializeField] private bool _isInvert;

        protected override void OnBound() => 
            SetEnable();

        protected override void OnUnbound() => 
            SetEnable();
        
        public void SetValue<T>(T value) { }
        
        private void SetEnable() =>
            enabled = _isInvert ? !IsBound : IsBound;
    }
}