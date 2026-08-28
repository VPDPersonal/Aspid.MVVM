#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Light, Color}"/> that binds <see cref="Light.color"/>.
    /// </summary>
    [Serializable]
    public class LightColorBinder : TargetBinder<Light, Color>, IColorBinder
    {
        /// <inheritdoc/>
        public LightColorBinder(
            Light target,
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.color;
            set => Target.color = value;
        }
    }
}
