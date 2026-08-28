#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetObjectBinder{T1, T2}">TargetObjectBinder&lt;Collider2D, PhysicsMaterial2D&gt;</see> that binds
    /// <see cref="Collider2D.sharedMaterial"/>.
    /// </summary>
    /// <remarks>
    /// Reads and writes <see cref="Collider2D.sharedMaterial"/> rather than <c>material</c>, which instantiates a
    /// copy on read and leaks it into the scene.
    /// </remarks>
    [Serializable]
    public class Collider2DMaterialBinder : TargetObjectBinder<Collider2D, PhysicsMaterial2D>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial2D? Property
        {
            get => Target.sharedMaterial;
            set => Target.sharedMaterial = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public Collider2DMaterialBinder(Collider2D target, IConverter<PhysicsMaterial2D?, PhysicsMaterial2D?>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
