#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Collider}"/> that sets the <see cref="Collider.enabled"/> property.
    /// </summary>
    /// <example>
    /// Set the Collider enabled property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private ColliderEnabledBinder _enabled;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _enabled;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Collider _collider;
    ///    
    ///     private ColliderEnabledBinder Enabled =>
    ///         new(_collider);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _enabled;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class ColliderEnabledBinder : TargetBoolBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.enabled;
            set => Target.enabled = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ColliderEnabledBinder"/> targeting the specified <see cref="Collider"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.enabled"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ColliderEnabledBinder(Collider target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ColliderEnabledBinder"/> targeting the specified <see cref="Collider"/>.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.enabled"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ColliderEnabledBinder(Collider target, bool isInvert, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}