#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class BehaviourEnabledBinder : TargetBinder<Behaviour>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public BehaviourEnabledBinder(Behaviour target, bool isInvert = false)
            : base(target)
        {
            _isInvert = isInvert; 
        }

        public void SetValue(bool value) =>
            Target.enabled = _isInvert ? !value : value;
    }
}