#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.ignoreParentGroups"/> property.
    /// </summary>
    /// <example>
    /// Set the CanvasGroup ignoreParentGroups property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private CanvasGroupIgnoreParentGroupsBinder _ignoreParentGroups;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _ignoreParentGroups;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private CanvasGroup _canvasGroup;
    ///    
    ///     private CanvasGroupIgnoreParentGroupsBinder IgnoreParentGroups =>
    ///         new(_canvasGroup);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _ignoreParentGroups;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class CanvasGroupIgnoreParentGroupsBinder : TargetBoolBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected override bool Property
        {
            get => Target.ignoreParentGroups;
            set => Target.ignoreParentGroups = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupIgnoreParentGroupsBinder"/> targeting the specified <see cref="CanvasGroup"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.ignoreParentGroups"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CanvasGroupIgnoreParentGroupsBinder(CanvasGroup target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupIgnoreParentGroupsBinder"/> targeting the specified <see cref="CanvasGroup"/>.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.ignoreParentGroups"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CanvasGroupIgnoreParentGroupsBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}