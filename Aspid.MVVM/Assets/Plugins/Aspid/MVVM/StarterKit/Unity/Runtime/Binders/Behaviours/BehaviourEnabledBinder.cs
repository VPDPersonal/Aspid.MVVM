#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Behaviour}"/> that sets the <see cref="Behaviour.enabled"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Behaviour-Enabled-1.1.0.xml" path="doc//member[@name='BehaviourEnabledBinder']/*" />
    [Serializable]
    public class BehaviourEnabledBinder : TargetBoolBinder<Behaviour>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.enabled;
            set => Target.enabled = value;
        }

        /// <inheritdoc/>
        public BehaviourEnabledBinder(Behaviour target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}