#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;Renderer, UnityEngine.Rendering.ShadowCastingMode&gt;</see> that binds
    /// <see cref="Renderer.shadowCastingMode"/>.
    /// </summary>
    /// <remarks>
    /// Whether the renderer casts a shadow, and whether it casts one when invisible. It is a quality setting as
    /// much as a look: turning shadows off per object is the cheapest way to buy frames back on a weak device.
    /// </remarks>
    [Serializable]
    public class RendererShadowCastingBinder : TargetBinder<Renderer, UnityEngine.Rendering.ShadowCastingMode>
    {
        /// <inheritdoc/>
        protected sealed override UnityEngine.Rendering.ShadowCastingMode Property
        {
            get => Target.shadowCastingMode;
            set => Target.shadowCastingMode = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public RendererShadowCastingBinder(Renderer target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
