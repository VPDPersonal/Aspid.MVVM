#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Behaviour}"/> that sets the <see cref="Behaviour.enabled"/> property.
    /// </summary>
    /// <example>
    /// Set the Behaviour enabled property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private BehaviourEnabledBinder _enabled;
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
    ///     [SerializeField] private Behaviour _behaviour;
    ///    
    ///     private BehaviourEnabledBinder Enabled =>
    ///         new(_behaviour);
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
    public class BehaviourEnabledBinder : TargetBoolBinder<Behaviour>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.enabled;
            set => Target.enabled = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BehaviourEnabledBinder"/> targeting the specified <see cref="Behaviour"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="Behaviour"/> whose <see cref="Behaviour.enabled"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public BehaviourEnabledBinder(Behaviour target, BindMode mode)
            : this(target, false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="BehaviourEnabledBinder"/> targeting the specified <see cref="Behaviour"/>.
        /// </summary>
        /// <param name="target">The <see cref="Behaviour"/> whose <see cref="Behaviour.enabled"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public BehaviourEnabledBinder(Behaviour target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}