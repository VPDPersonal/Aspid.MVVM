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
    /// <see cref="SwitcherBinder{SphereCollider, float, Converter{float, float}}"/> that switches the <see cref="SphereCollider.radius"/>
    /// property between two <see langword="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the SphereCollider radius between two float values based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private SphereColliderRadiusSwitcherBinder _isExpanded;
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
    ///     [SerializeField] private SphereCollider _sphereCollider;
    ///    
    ///     private SphereColliderRadiusSwitcherBinder IsExpanded => new(
    ///         _sphereCollider,
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
    public sealed class SphereColliderRadiusSwitcherBinder : SwitcherBinder<SphereCollider, float, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SphereColliderRadiusSwitcherBinder"/> targeting the specified <see cref="SphereCollider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="SphereCollider"/> whose <see cref="SphereCollider.radius"/> property is switched.</param>
        /// <param name="trueValue">The radius value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The radius value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode.</param>
        public SphereColliderRadiusSwitcherBinder(
            SphereCollider target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="SphereColliderRadiusSwitcherBinder"/> targeting the specified <see cref="SphereCollider"/>.
        /// </summary>
        /// <param name="target">The <see cref="SphereCollider"/> whose <see cref="SphereCollider.radius"/> property is switched.</param>
        /// <param name="trueValue">The radius value applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The radius value applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound boolean value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode.</param>
        public SphereColliderRadiusSwitcherBinder(
            SphereCollider target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(float value) =>
            Target.radius = value;
    }
}