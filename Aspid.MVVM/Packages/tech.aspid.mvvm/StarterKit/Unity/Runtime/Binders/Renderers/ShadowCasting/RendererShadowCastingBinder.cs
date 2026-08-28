#nullable enable
using System;
using UnityEngine;
using UnityEngine.Rendering;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;Renderer, UnityEngine.Rendering.ShadowCastingMode&gt;</see> that binds
    /// <see cref="Renderer.shadowCastingMode"/>.
    /// </summary>
    [Serializable]
    public class RendererShadowCastingBinder : TargetBinder<Renderer, ShadowCastingMode>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public RendererShadowCastingBinder(Renderer target, IConverter<ShadowCastingMode, ShadowCastingMode>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override ShadowCastingMode Property
        {
            get => Target.shadowCastingMode;
            set => Target.shadowCastingMode = value;
        }
    }
}
