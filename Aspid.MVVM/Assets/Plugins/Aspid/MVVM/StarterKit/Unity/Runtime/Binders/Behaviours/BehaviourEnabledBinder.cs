#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class BehaviourEnabledBinder : TargetBinder<Behaviour>, IBinder<bool>
    {
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