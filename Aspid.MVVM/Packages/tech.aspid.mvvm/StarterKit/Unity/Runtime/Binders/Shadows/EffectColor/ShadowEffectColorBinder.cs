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
    /// <remarks><see cref="Outline"/> is a <see cref="Shadow"/>, so this binder targets either.</remarks>
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
