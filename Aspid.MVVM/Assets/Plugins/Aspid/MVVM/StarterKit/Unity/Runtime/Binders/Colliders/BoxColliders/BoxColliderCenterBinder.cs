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
    /// <see cref="TargetVector3Binder{BoxCollider}"/> that sets the <see cref="BoxCollider.center"/> property.
    /// </summary>
    /// <example>
    /// Set the BoxCollider center based on a Vector3 ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private BoxColliderCenterBinder _center;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Vector3 _center;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private BoxCollider _boxCollider;
    ///    
    ///     private BoxColliderCenterBinder Center =>
    ///         new(_boxCollider);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public Vector3 _center;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class BoxColliderCenterBinder : TargetVector3Binder<BoxCollider>
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => Target.center;
            set => Target.center = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BoxColliderCenterBinder"/> targeting the specified <see cref="BoxCollider"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="BoxCollider"/> whose <see cref="BoxCollider.center"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public BoxColliderCenterBinder(BoxCollider target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="BoxColliderCenterBinder"/> targeting the specified <see cref="BoxCollider"/>.
        /// </summary>
        /// <param name="target">The <see cref="BoxCollider"/> whose <see cref="BoxCollider.center"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector3"/> value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public BoxColliderCenterBinder(BoxCollider target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}