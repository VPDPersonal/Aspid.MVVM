#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.PhysicsMaterial?, UnityEngine.PhysicsMaterial?>;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverterPhysicsMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Collider, PhysicsMaterial, IConverter{PhysicsMaterial, PhysicsMaterial}}"/> that sets the <see cref="Collider.material"/> property.
    /// </summary>
    /// <example>
    /// Set the Collider material based on a Material ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private ColliderMaterialBinder _material;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public PhysicsMaterial _material;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Collider _collider;
    ///    
    ///     private ColliderMaterialBinder Material =>
    ///         new(_collider);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public PhysicsMaterial _material;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class ColliderMaterialBinder : TargetBinder<Collider, PhysicsMaterial, Converter>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial? Property
        {
            get => Target.material;
            set => Target.material = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ColliderMaterialBinder"/> targeting the specified <see cref="Collider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.material"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ColliderMaterialBinder(Collider target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ColliderMaterialBinder"/> targeting the specified <see cref="Collider"/>.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.material"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="PhysicsMaterial"/> value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ColliderMaterialBinder(Collider target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}