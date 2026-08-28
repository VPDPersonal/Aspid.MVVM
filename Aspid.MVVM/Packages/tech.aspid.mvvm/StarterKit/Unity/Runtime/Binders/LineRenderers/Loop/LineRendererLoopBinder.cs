#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{LineRenderer, bool}"/> that binds <see cref="LineRenderer.loop"/>.
    /// </summary>
    [Serializable]
    public class LineRendererLoopBinder : TargetBinder<LineRenderer, bool>
    {
        /// <inheritdoc/>
        public LineRendererLoopBinder(
            LineRenderer target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.loop;
            set => Target.loop = value;
        }
    }
}
