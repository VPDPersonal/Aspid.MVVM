#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{LineRenderer}"/> that binds <see cref="LineRenderer.loop"/>.
    /// </summary>
    /// <remarks>
    /// Whether the last point connects back to the first — the difference between a path and an outline, which
    /// is what a selection ring or a closed zone needs.
    /// </remarks>
    [Serializable]
    public class LineRendererLoopBinder : TargetBoolBinder<LineRenderer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.loop;
            set => Target.loop = value;
        }

        /// <inheritdoc/>
        public LineRendererLoopBinder(
            LineRenderer target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
