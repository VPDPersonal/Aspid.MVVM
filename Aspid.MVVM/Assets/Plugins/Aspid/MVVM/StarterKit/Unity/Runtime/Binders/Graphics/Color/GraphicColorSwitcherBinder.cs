#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{Graphic, Color, Converter}"/> that switches the <see cref="Graphic.color"/>
    /// property between two <see cref="Color"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Graphic-Color-1.1.0.xml" path="doc//member[@name='GraphicColorSwitcherBinder']/*" />
    [Serializable]
    public sealed class GraphicColorSwitcherBinder : SwitcherColorBinder<Graphic>
    {
        /// <inheritdoc/>
        public GraphicColorSwitcherBinder(
            Graphic target,
            Color trueColor,
            Color falseColor,
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueColor, falseColor, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            Target.color = value;
    }
}