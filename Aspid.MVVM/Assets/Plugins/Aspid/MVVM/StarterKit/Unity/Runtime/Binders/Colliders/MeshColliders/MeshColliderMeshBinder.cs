#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Mesh?, UnityEngine.Mesh?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMesh;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{MeshCollider, Mesh, IConverter{Mesh, Mesh}}"/> that sets the <see cref="MeshCollider.sharedMesh"/> property.
    /// </summary>
    /// <example>
    /// Set the MeshCollider shared mesh based on a Mesh ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private MeshColliderMeshBinder _mesh;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Mesh _mesh;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private MeshCollider _meshCollider;
    ///    
    ///     private MeshColliderMeshBinder Mesh =>
    ///         new(_meshCollider);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Mesh _mesh;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class MeshColliderMeshBinder : TargetBinder<MeshCollider, Mesh, Converter>
    {
        /// <inheritdoc/>
        protected sealed override Mesh? Property
        {
            get => Target.sharedMesh;
            set => Target.sharedMesh = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MeshColliderMeshBinder"/> targeting the specified <see cref="MeshCollider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="MeshCollider"/> whose <see cref="MeshCollider.sharedMesh"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public MeshColliderMeshBinder(MeshCollider target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="MeshColliderMeshBinder"/> targeting the specified <see cref="MeshCollider"/>.
        /// </summary>
        /// <param name="target">The <see cref="MeshCollider"/> whose <see cref="MeshCollider.sharedMesh"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Mesh"/> value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public MeshColliderMeshBinder(MeshCollider target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}