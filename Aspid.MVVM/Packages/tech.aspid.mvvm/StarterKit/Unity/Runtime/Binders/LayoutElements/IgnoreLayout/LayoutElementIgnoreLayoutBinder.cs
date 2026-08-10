#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{LayoutElement}"/> that binds <see cref="LayoutElement.ignoreLayout"/>.
    /// </summary>
    /// <remarks>
    /// Takes the child out of its layout group entirely — the usual way to let one element float free while the rest stay arranged.
    /// </remarks>
    [Serializable]
    public class LayoutElementIgnoreLayoutBinder : TargetBoolBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.ignoreLayout;
            set => Target.ignoreLayout = value;
        }

        /// <inheritdoc/>
        public LayoutElementIgnoreLayoutBinder(
            LayoutElement target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
