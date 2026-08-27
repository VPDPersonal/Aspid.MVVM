#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{LayoutElement}"/> that binds <see cref="LayoutElement.ignoreLayout"/>.
    /// </summary>
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
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
