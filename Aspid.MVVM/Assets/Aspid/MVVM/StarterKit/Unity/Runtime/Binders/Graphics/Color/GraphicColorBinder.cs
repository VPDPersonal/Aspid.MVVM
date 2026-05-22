#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetColorBinder{Graphic}"/> that sets the <see cref="Graphic.color"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Graphic-Color-1.1.0.xml" path="doc//member[@name='GraphicColorBinder']/*" />
    [Serializable]
    public class GraphicColorBinder : TargetColorBinder<Graphic>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.color;
            set => Target.color = value;
        }

        /// <inheritdoc/>
        public GraphicColorBinder(Graphic target, IConverter<Color, Color>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}