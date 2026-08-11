#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetColorBinder{Shadow}"/> that binds <see cref="Shadow.effectColor"/>.
    /// </summary>
    /// <remarks>
    /// The colour of a shadow or an outline — <see cref="Outline"/> is a <see cref="Shadow"/>, so this binder
    /// takes either. It is how a rarity glow, a team colour or a selection highlight is expressed without a
    /// second graphic, and neither effect could be bound at all.
    /// </remarks>
    [Serializable]
    public class ShadowEffectColorBinder : TargetColorBinder<Shadow>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.effectColor;
            set => Target.effectColor = value;
        }

        /// <inheritdoc/>
        public ShadowEffectColorBinder(
            Shadow target,
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
