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
    /// <see cref="TargetFloatBinder{CapsuleCollider}"/> that sets the <see cref="CapsuleCollider.radius"/> property.
    /// </summary>
    /// <example>
    /// Set the CapsuleCollider radius based on a float ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private CapsuleColliderRadiusBinder _radius;
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
    ///     [SerializeField] private CapsuleCollider _capsuleCollider;
    ///    
    ///     private CapsuleColliderRadiusBinder Radius =>
    ///         new(_capsuleCollider);
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
    public class CapsuleColliderRadiusBinder : TargetFloatBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.radius;
            set => Target.radius = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CapsuleColliderRadiusBinder"/> targeting the specified <see cref="CapsuleCollider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="CapsuleCollider"/> whose <see cref="CapsuleCollider.radius"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CapsuleColliderRadiusBinder(CapsuleCollider target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="CapsuleColliderRadiusBinder"/> targeting the specified <see cref="CapsuleCollider"/>.
        /// </summary>
        /// <param name="target">The <see cref="CapsuleCollider"/> whose <see cref="CapsuleCollider.radius"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound <see langword="float"/> value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CapsuleColliderRadiusBinder(
            CapsuleCollider target,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}