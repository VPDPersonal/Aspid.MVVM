#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class BehaviourEnabledBinder : TargetBoolBinder<Behaviour>
    {
        protected sealed override bool Property
        {
            get => Target.enabled;
            set => Target.enabled = value;
        }
        
        public BehaviourEnabledBinder(Behaviour target, BindMode mode)
            : this(target, false, mode) { }
        
        public BehaviourEnabledBinder(Behaviour target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}