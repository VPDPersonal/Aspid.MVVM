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
    /// Friction and bounce as one swappable asset: ice, mud, rubber. The 3D domain had this binder and the 2D one had
    /// nothing at all.
    /// <para/>
    /// Reads and writes <see cref="Collider2D.sharedMaterial"/> rather than <c>material</c>, which instantiates a copy
    /// on read and leaks it into the scene. Writing the shared asset does affect every collider using it, which is
    /// what swapping a surface material is meant to do.
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
        public Collider2DMaterialBinder(Collider2D target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
