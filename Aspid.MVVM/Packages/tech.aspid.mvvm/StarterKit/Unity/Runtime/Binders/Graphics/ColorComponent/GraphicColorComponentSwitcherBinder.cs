#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatBinder{Graphic}"/> that switches a single <see cref="ColorComponent"/>
    /// channel of the <see cref="Graphic.color"/> property between two <see cref="float"/> values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Graphic-ColorComponent-1.1.0.xml" path="doc//member[@name='GraphicColorComponentSwitcherBinder']/*" />
    [Serializable]
    public sealed class GraphicColorComponentSwitcherBinder : SwitcherFloatBinder<Graphic>
    {
        [Tooltip("Which color channel the bound value writes to; others keep their value.")]
        [SerializeField] private ColorComponent _component = ColorComponent.A;

        /// <param name="target">The <see cref="Graphic"/> whose color channel is switched.</param>
        /// <param name="trueColor">The channel value used when the bound boolean value is <see langword="true"/>.</param>
        /// <param name="falseColor">The channel value used when the bound boolean value is <see langword="false"/>.</param>
        /// <param name="component">Which color channel the bound value writes to; others keep their value.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode.</param>
        public GraphicColorComponentSwitcherBinder(
            Graphic target,
            float trueColor,
            float falseColor,
            ColorComponent component = ColorComponent.A,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueColor, falseColor, converter, mode)
        {
            _component = component;
        }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.SetColorComponent(_component, value);
    }
}