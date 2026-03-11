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
    /// <see cref="TargetFloatBinder{SphereCollider}"/> that sets the <see cref="SphereCollider.radius"/> property.
    /// </summary>
    /// <example>
    /// Set the SphereCollider radius based on a float ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private SphereColliderRadiusBinder _radius;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _radius;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private SphereCollider _sphereCollider;
    ///    
    ///     private SphereColliderRadiusBinder Radius =>
    ///         new(_sphereCollider);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public float _radius;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class SphereColliderRadiusBinder : TargetFloatBinder<SphereCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.radius;
            set => Target.radius = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SphereColliderRadiusBinder"/> targeting the specified <see cref="SphereCollider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="SphereCollider"/> whose <see cref="SphereCollider.radius"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public SphereColliderRadiusBinder(SphereCollider target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="SphereColliderRadiusBinder"/> targeting the specified <see cref="SphereCollider"/>.
        /// </summary>
        /// <param name="target">The <see cref="SphereCollider"/> whose <see cref="SphereCollider.radius"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound <see langword="float"/> value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public SphereColliderRadiusBinder(
            SphereCollider target,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}