#nullable enable
using System;
using UnityEngine;
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.PhysicsMaterial?, UnityEngine.PhysicsMaterial?>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2, T3}">TargetBinder&lt;Collider, PhysicsMaterial, IConverter&lt;PhysicsMaterial, PhysicsMaterial&gt;&gt;</see> that sets the <see cref="Collider.material"/> property.
    /// </summary>
    /// <remarks>
    /// The value read back is <see cref="Collider.sharedMaterial"/>, not <see cref="Collider.material"/>: reading
    /// the latter makes Unity replace the assigned asset with a private clone named <c>"… (Instance)"</c> — the
    /// ViewModel would receive something that no longer compares equal to the asset it handed over, and the clone
    /// lives until the collider is destroyed.
    /// </remarks>
    /// <include file="XmlExampleDoc-Collider-Material-1.1.0.xml" path="doc//member[@name='ColliderMaterialBinder']/*" />
    [Serializable]
    public class ColliderMaterialBinder : TargetBinder<Collider, PhysicsMaterial, Converter>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial? Property
        {
            get => Target.sharedMaterial;
            set => Target.material = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ColliderMaterialBinder(Collider target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}