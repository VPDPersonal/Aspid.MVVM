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
    /// <see cref="SwitcherBinder{Collider, Mesh, IConverter{PhysicsMaterial, PhysicsMaterial}}"/> that switches the <see cref="Collider.material"/>
    /// property between two <see cref="PhysicsMaterial"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the Collider material between two PhysicsMaterial assets based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private ColliderMaterialSwitcherBinder _isActive;
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
    ///     [SerializeField] private Collider _collider;
    ///     [SerializeField] private PhysicsMaterial _activeMaterial;
    ///     [SerializeField] private PhysicsMaterial _inactiveMaterial;
    ///    
    ///     private ColliderMaterialSwitcherBinder IsActive => new(
    ///         _collider,
    ///         trueValue: _activeMaterial,
    ///         falseValue: _inactiveMaterial);
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
    public sealed class ColliderMaterialSwitcherBinder : SwitcherBinder<Collider, PhysicsMaterial, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ColliderMaterialSwitcherBinder"/> targeting the specified <see cref="Collider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.material"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="PhysicsMaterial"/> value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="PhysicsMaterial"/> value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode.</param>
        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue,
            PhysicsMaterial falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ColliderMaterialSwitcherBinder"/> targeting the specified <see cref="Collider"/>.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.material"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="PhysicsMaterial"/> value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="PhysicsMaterial"/> value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound boolean value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode.</param>
        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue,
            PhysicsMaterial falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(PhysicsMaterial? value) =>
            Target.material = value;
    }
}