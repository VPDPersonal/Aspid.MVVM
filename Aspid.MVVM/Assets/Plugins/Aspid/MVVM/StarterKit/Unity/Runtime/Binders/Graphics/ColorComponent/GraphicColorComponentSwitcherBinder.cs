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
        [SerializeField] private ColorComponent _component = ColorComponent.A;

        /// <inheritdoc/>
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