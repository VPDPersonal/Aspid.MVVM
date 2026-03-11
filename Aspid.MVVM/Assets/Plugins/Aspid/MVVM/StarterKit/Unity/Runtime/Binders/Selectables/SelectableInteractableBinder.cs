#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Selectable}"/> that sets the <see cref="Selectable.interactable"/> property.
    /// </summary>
    /// <example>
    /// Set the Selectable interactable property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private SelectableInteractableBinder _interactable;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _interactable;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Selectable _selectable;
    ///    
    ///     private SelectableInteractableBinder Interactable =>
    ///         new(_selectable);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _interactable;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class SelectableInteractableBinder : TargetBoolBinder<Selectable>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.interactable;
            set => Target.interactable = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SelectableInteractableBinder"/> targeting the specified <see cref="Selectable"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="Selectable"/> whose <see cref="Selectable.interactable"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public SelectableInteractableBinder(Selectable target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="SelectableInteractableBinder"/> targeting the specified <see cref="Selectable"/>.
        /// </summary>
        /// <param name="target">The <see cref="Selectable"/> whose <see cref="Selectable.interactable"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public SelectableInteractableBinder(Selectable target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}