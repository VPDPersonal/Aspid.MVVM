#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Collider, bool}"/> that sets the <see cref="Collider.isTrigger"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Collider-IsTrigger-1.1.0.xml" path="doc//member[@name='ColliderIsTriggerBinder']/*" />
    [Serializable]
    public sealed class ColliderIsTriggerBinder : TargetBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override bool Property
        {
            get => Target.isTrigger;
            set => Target.isTrigger = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ColliderIsTriggerBinder(Collider target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}