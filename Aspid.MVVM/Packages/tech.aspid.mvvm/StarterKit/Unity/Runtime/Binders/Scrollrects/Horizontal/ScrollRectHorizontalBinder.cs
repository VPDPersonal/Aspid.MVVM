#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{ScrollRect, bool}"/> that binds <see cref="ScrollRect.horizontal"/>.
    /// </summary>
    [Serializable]
    public class ScrollRectHorizontalBinder : TargetBinder<ScrollRect, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.horizontal;
            set => Target.horizontal = value;
        }

        /// <inheritdoc/>
        public ScrollRectHorizontalBinder(
            ScrollRect target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
