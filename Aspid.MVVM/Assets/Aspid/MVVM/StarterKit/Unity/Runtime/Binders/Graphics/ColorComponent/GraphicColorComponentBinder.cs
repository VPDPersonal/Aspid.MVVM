#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Graphic}"/> that sets a single <see cref="ColorComponent"/> channel
    /// of the <see cref="Graphic.color"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Graphic-ColorComponent-1.1.0.xml" path="doc//member[@name='GraphicColorComponentBinder']/*" />
    [Serializable]
    public class GraphicColorComponentBinder : TargetFloatBinder<Graphic>
    {
        [Tooltip("The color channel of the Graphic's color property to bind.")]
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.GetColorComponent(_colorComponent);
            set => Target.SetColorComponent(_colorComponent, value);
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="GraphicColorComponentBinder"/> targeting the specified <see cref="Graphic"/>.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> whose color channel is bound.</param>
        /// <param name="colorComponent">The color channel to bind.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public GraphicColorComponentBinder(Graphic target, ColorComponent colorComponent = ColorComponent.A, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _colorComponent = colorComponent;
        }
    }
}