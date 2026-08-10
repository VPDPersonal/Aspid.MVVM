#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Renderer}"/> that sets the <see cref="Renderer.enabled"/> property.
    /// </summary>
    /// <remarks>
    /// A <see cref="Renderer"/> is a <see cref="Component"/> and not a <see cref="Behaviour"/>, so the behaviour binders cannot take one — this is the equivalent for it.
    /// </remarks>
    [Serializable]
    public class RendererEnabledBinder : TargetBoolBinder<Renderer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.enabled;
            set => Target.enabled = value;
        }

        /// <inheritdoc/>
        public RendererEnabledBinder(Renderer target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
