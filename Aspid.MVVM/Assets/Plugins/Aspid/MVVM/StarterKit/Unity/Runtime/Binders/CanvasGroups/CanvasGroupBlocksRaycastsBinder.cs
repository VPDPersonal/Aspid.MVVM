#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.blocksRaycasts"/> property.
    /// </summary>
    /// <example>
    /// Set the CanvasGroup blocksRaycasts property based on a boolean ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private CanvasGroupBlocksRaycastsBinder _blocksRaycasts;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _blocksRaycasts;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private CanvasGroup _canvasGroup;
    ///    
    ///     private CanvasGroupBlocksRaycastsBinder BlocksRaycasts =>
    ///         new(_canvasGroup);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _blocksRaycasts;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class CanvasGroupBlocksRaycastsBinder : TargetBoolBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.blocksRaycasts;
            set => Target.blocksRaycasts = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupBlocksRaycastsBinder"/> targeting the specified <see cref="CanvasGroup"/>
        /// with inversion disabled.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.blocksRaycasts"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CanvasGroupBlocksRaycastsBinder(CanvasGroup target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="CanvasGroupBlocksRaycastsBinder"/> targeting the specified <see cref="CanvasGroup"/>.
        /// </summary>
        /// <param name="target">The <see cref="CanvasGroup"/> whose <see cref="CanvasGroup.blocksRaycasts"/> property is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CanvasGroupBlocksRaycastsBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}