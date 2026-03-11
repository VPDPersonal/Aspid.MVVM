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
    /// <see cref="SwitcherBinder{MeshCollider, Mesh, IConverter{Mesh, Mesh}}"/> that switches the <see cref="MeshCollider.sharedMesh"/>
    /// property between two <see cref="Mesh"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the MeshCollider shared mesh between two Mesh assets based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private MeshColliderMeshSwitcherBinder _isActive;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isActive;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private MeshCollider _meshCollider;
    ///     [SerializeField] private Mesh _activeMesh;
    ///     [SerializeField] private Mesh _inactiveMesh;
    ///    
    ///     private MeshColliderMeshSwitcherBinder IsActive => new(
    ///         _meshCollider,
    ///         trueValue: _activeMesh,
    ///         falseValue: _inactiveMesh);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isActive;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class MeshColliderMeshSwitcherBinder : SwitcherBinder<MeshCollider, Mesh, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MeshColliderMeshSwitcherBinder"/> targeting the specified <see cref="MeshCollider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="MeshCollider"/> whose <see cref="MeshCollider.sharedMesh"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="Mesh"/> value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="Mesh"/> value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode.</param>
        public MeshColliderMeshSwitcherBinder(
            MeshCollider target,
            Mesh trueValue,
            Mesh falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="MeshColliderMeshSwitcherBinder"/> targeting the specified <see cref="MeshCollider"/>.
        /// </summary>
        /// <param name="target">The <see cref="MeshCollider"/> whose <see cref="MeshCollider.sharedMesh"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="Mesh"/> value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="Mesh"/> value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound boolean value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode.</param>
        public MeshColliderMeshSwitcherBinder(
            MeshCollider target,
            Mesh trueValue,
            Mesh falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Mesh value) =>
            Target.sharedMesh = value;
    }
}