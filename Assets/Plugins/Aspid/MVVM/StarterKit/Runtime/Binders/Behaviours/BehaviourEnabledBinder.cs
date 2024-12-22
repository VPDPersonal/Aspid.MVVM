#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class BehaviourEnabledBinder : Binder, IBinder<bool>
    {
        [Header("Component")]
        [SerializeField] private Behaviour _behaviour;
        
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public BehaviourEnabledBinder(MonoBehaviour behaviour, bool isInvert = false)
        {
            _isInvert = isInvert;
            _behaviour = behaviour ?? throw new ArgumentNullException(nameof(behaviour));
        }

        public void SetValue(bool value) =>
            _behaviour.enabled = _isInvert ? !value : value;
    }
}