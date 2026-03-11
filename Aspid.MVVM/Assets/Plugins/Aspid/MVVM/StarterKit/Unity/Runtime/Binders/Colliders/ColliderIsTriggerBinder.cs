#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Collider}"/> that sets the <see cref="Collider.isTrigger"/> property.
    /// </summary>
    /// <example>
    /// Set the Collider isTrigger property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private ColliderEnabledBinder _isTrigger;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isTrigger;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Collider _collider;
    ///    
    ///     private ColliderEnabledBinder IsTrigger =>
    ///         new(_collider);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isTrigger;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class ColliderIsTriggerBinder : TargetBoolBinder<Collider>
    {
        /// <inheritdoc/>
        protected override bool Property
        {
            get => Target.isTrigger;
            set => Target.isTrigger = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ColliderIsTriggerBinder"/> targeting the specified <see cref="Collider"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.isTrigger"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ColliderIsTriggerBinder(Collider target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ColliderIsTriggerBinder"/> targeting the specified <see cref="Collider"/>.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.isTrigger"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ColliderIsTriggerBinder(Collider target, bool isInvert, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}