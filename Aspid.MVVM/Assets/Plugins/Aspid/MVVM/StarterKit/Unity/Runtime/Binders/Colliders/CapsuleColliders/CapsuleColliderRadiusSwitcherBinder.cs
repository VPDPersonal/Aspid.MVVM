#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{CapsuleCollider, float, IConverter{float, float}}"/> that switches the <see cref="CapsuleCollider.radius"/>
    /// property between two <see langword="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the CapsuleCollider radius between two float values based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private CapsuleColliderRadiusSwitcherBinder _isExpanded;
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
    ///     [SerializeField] private CapsuleCollider _capsuleCollider;
    ///    
    ///     private CapsuleColliderRadiusSwitcherBinder IsExpanded => new(
    ///         _capsuleCollider,
    ///         trueValue: 1f,
    ///         falseValue: 0.5f);
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
    public sealed class CapsuleColliderRadiusSwitcherBinder : SwitcherBinder<CapsuleCollider, float, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CapsuleColliderRadiusSwitcherBinder"/> targeting the specified <see cref="CapsuleCollider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="CapsuleCollider"/> whose <see cref="CapsuleCollider.radius"/> property is switched.</param>
        /// <param name="trueValue">The radius value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The radius value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode.</param>
        public CapsuleColliderRadiusSwitcherBinder(
            CapsuleCollider target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="CapsuleColliderRadiusSwitcherBinder"/> targeting the specified <see cref="CapsuleCollider"/>.
        /// </summary>
        /// <param name="target">The <see cref="CapsuleCollider"/> whose <see cref="CapsuleCollider.radius"/> property is switched.</param>
        /// <param name="trueValue">The radius value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The radius value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound boolean value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode.</param>
        public CapsuleColliderRadiusSwitcherBinder(
            CapsuleCollider target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.radius = value;
    }
}