#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Collider}"/> that sets the <see cref="Collider.providesContacts"/> property.
    /// </summary>
    /// <example>
    /// Set the Collider providesContacts property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private ColliderEnabledBinder _providesContacts;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _providesContacts;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Collider _collider;
    ///    
    ///     private ColliderEnabledBinder ProvidesContacts =>
    ///         new(_collider);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _providesContacts;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class ColliderProvidesContactsBinder : TargetBoolBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.providesContacts;
            set => Target.providesContacts = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ColliderProvidesContactsBinder"/> targeting the specified <see cref="Collider"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.providesContacts"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ColliderProvidesContactsBinder(Collider target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ColliderProvidesContactsBinder"/> targeting the specified <see cref="Collider"/>.
        /// </summary>
        /// <param name="target">The <see cref="Collider"/> whose <see cref="Collider.providesContacts"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ColliderProvidesContactsBinder(Collider target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}