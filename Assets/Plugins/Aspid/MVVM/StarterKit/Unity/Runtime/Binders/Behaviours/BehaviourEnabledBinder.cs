#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class BehaviourEnabledBinder : TargetBinder<Behaviour>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public BehaviourEnabledBinder(Behaviour target, BindMode mode)
            : this(target, false, mode) { }
        
        public BehaviourEnabledBinder(Behaviour target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isInvert = isInvert; 
        }

        public void SetValue(bool value) =>
            Target.enabled = _isInvert ? !value : value;
    }
}