#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{MeshCollider}"/> that sets the <see cref="MeshCollider.convex"/> property.
    /// </summary>
    /// <example>
    /// Set the MeshCollider convex property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private MeshColliderConvexBinder _convex;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _convex;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private MeshCollider _meshCollider;
    ///    
    ///     private MeshColliderConvexBinder Convex =>
    ///         new(_meshCollider);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _convex;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class MeshColliderConvexBinder : TargetBoolBinder<MeshCollider>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.convex;
            set => Target.convex = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MeshColliderConvexBinder"/> targeting the specified <see cref="MeshCollider"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="MeshCollider"/> whose <see cref="MeshCollider.convex"/> property is bound.</param>
        /// <param name="bindMode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public MeshColliderConvexBinder(MeshCollider target, BindMode bindMode)
            : this(target, isInvert: false, bindMode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="MeshColliderConvexBinder"/> targeting the specified <see cref="MeshCollider"/>.
        /// </summary>
        /// <param name="target">The <see cref="MeshCollider"/> whose <see cref="MeshCollider.convex"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public MeshColliderConvexBinder(MeshCollider target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            Mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}