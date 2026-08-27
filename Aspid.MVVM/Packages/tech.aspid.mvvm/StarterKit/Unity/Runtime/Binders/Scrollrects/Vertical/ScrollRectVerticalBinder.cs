#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{ScrollRect}"/> that binds <see cref="ScrollRect.vertical"/>.
    /// </summary>
    [Serializable]
    public class ScrollRectVerticalBinder : TargetBoolBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.vertical;
            set => Target.vertical = value;
        }

        /// <inheritdoc/>
        public ScrollRectVerticalBinder(
            ScrollRect target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
