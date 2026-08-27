#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetColorBinder{Light}"/> that binds <see cref="Light.color"/>.
    /// </summary>
    [Serializable]
    public class LightColorBinder : TargetColorBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.color;
            set => Target.color = value;
        }

        /// <inheritdoc/>
        public LightColorBinder(
            Light target,
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
