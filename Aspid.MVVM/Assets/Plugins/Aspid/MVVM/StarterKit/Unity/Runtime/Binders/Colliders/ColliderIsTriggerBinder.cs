#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="Collider.isTrigger"/> property on a <see cref="Collider"/>
    /// when the bound ViewModel value changes. Supports optional value inversion.
    /// </summary>
    [Serializable]
    public sealed class ColliderIsTriggerBinder : TargetBoolBinder<Collider>
    {
        protected override bool Property
        {
            get => Target.isTrigger;
            set => Target.isTrigger = value;
        }

        public ColliderIsTriggerBinder(Collider target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        public ColliderIsTriggerBinder(Collider target, bool isInvert, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}