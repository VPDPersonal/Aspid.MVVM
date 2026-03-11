#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.interactable"/> property.
    /// </summary>
    /// <example>
    /// Set the CanvasGroup interactable property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private CanvasGroupInteractableBinder _interactable;
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
    ///     [SerializeField] private CanvasGroup _canvasGroup;
    ///    
    ///     private CanvasGroupInteractableBinder Interactable =>
    ///         new(_canvasGroup);
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
    public class CanvasGroupInteractableBinder : TargetBoolBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.interactable;
            set => Target.interactable = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupInteractableBinder"/> targeting the specified <see cref="CanvasGroup"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.interactable"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CanvasGroupInteractableBinder(CanvasGroup target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupInteractableBinder"/> targeting the specified <see cref="CanvasGroup"/>.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.interactable"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CanvasGroupInteractableBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}