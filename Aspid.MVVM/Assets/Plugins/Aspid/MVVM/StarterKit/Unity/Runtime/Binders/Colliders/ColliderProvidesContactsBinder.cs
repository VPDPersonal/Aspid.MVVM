#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="Collider.providesContacts"/> property on a <see cref="Collider"/>
    /// when the bound ViewModel value changes. Supports optional value inversion.
    /// </summary>
    [Serializable]
    public class ColliderProvidesContactsBinder : TargetBoolBinder<Collider>
    {
        protected sealed override bool Property
        {
            get => Target.providesContacts;
            set => Target.providesContacts = value;
        }

        public ColliderProvidesContactsBinder(Collider target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        public ColliderProvidesContactsBinder(Collider target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}