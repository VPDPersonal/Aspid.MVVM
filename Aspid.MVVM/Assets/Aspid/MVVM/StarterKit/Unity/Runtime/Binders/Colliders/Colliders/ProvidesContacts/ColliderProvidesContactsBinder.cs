#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Collider}"/> that sets the <see cref="Collider.providesContacts"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Collider-ProvidesContacts-1.1.0.xml" path="doc//member[@name='ColliderProvidesContactsBinder']/*" />
    [Serializable]
    public class ColliderProvidesContactsBinder : TargetBoolBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.providesContacts;
            set => Target.providesContacts = value;
        }

        /// <inheritdoc/>
        public ColliderProvidesContactsBinder(Collider target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}