#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{LineRenderer}"/> that binds <see cref="LineRenderer.loop"/>.
    /// </summary>
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
