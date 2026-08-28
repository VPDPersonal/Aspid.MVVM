#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;Collider, PhysicsMaterial&gt;</see> that sets the <see cref="Collider.material"/> property.
    /// </summary>
    /// <remarks>
    /// Reads back <see cref="Collider.sharedMaterial"/>, not <see cref="Collider.material"/> — reading the latter
    /// would replace the asset with a private clone and break equality with what the ViewModel sent.
    /// </remarks>
    /// <include file="XmlExampleDoc-Collider-Material-1.1.0.xml" path="doc//member[@name='ColliderMaterialBinder']/*" />
    [Serializable]
    public class ColliderMaterialBinder : TargetBinder<Collider, PhysicsMaterial>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial? Property
        {
            get => Target.sharedMaterial;
            set => Target.material = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ColliderMaterialBinder(Collider target, IConverter<PhysicsMaterial?, PhysicsMaterial?>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}