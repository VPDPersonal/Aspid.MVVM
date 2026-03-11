#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{BoxCollider, Vector3, IConverter{Vector3, Vector3}}"/> that switches the <see cref="BoxCollider.size"/>
    /// property between two <see cref="Vector3"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the BoxCollider size between two Vector3 values based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private BoxColliderSizeSwitcherBinder _isExpanded;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isExpanded;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private BoxCollider _boxCollider;
    ///    
    ///     private BoxColliderSizeSwitcherBinder IsExpanded => new(
    ///         _boxCollider,
    ///         trueValue: new Vector3(2, 2, 2),
    ///         falseValue: Vector3.one);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isExpanded;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class BoxColliderSizeSwitcherBinder : SwitcherBinder<BoxCollider, Vector3, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BoxColliderSizeSwitcherBinder"/> targeting the specified <see cref="BoxCollider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="BoxCollider"/> whose <see cref="BoxCollider.size"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="Vector3"/> size value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="Vector3"/> size value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode.</param>
        public BoxColliderSizeSwitcherBinder(
            BoxCollider target,
            Vector3 trueValue,
            Vector3 falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="BoxColliderSizeSwitcherBinder"/> targeting the specified <see cref="BoxCollider"/>.
        /// </summary>
        /// <param name="target">The <see cref="BoxCollider"/> whose <see cref="BoxCollider.size"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="Vector3"/> size value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="Vector3"/> size value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound boolean value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode.</param>
        public BoxColliderSizeSwitcherBinder(
            BoxCollider target,
            Vector3 trueValue,
            Vector3 falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            Target.size = value;
    }
}