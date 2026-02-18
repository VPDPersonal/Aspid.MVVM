#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class ColliderEnabledBinder : TargetBoolBinder<Collider>
    {
        protected sealed override bool Property
        {
            get => Target.enabled;
            set => Target.enabled = value;
        }
        
        public ColliderEnabledBinder(Collider target, BindMode mode)
            : this(target, isInvert: false, mode) { }
        
        public ColliderEnabledBinder(Collider target, bool isInvert, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}