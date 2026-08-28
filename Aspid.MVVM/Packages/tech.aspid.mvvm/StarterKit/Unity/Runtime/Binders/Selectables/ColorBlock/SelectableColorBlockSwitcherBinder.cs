#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget,T}"/> that switches the <see cref="Selectable.colors"/>
    /// property between two <see cref="ColorBlock"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Selectable-ColorBlock-1.1.0.xml" path="doc//member[@name='SelectableColorBlockSwitcherBinder']/*" />
    [Serializable]
    public sealed class SelectableColorBlockSwitcherBinder : SwitcherBinder<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        public SelectableColorBlockSwitcherBinder(
            Selectable target,
            ColorBlock trueValue,
            ColorBlock falseValue,
            IConverter<ColorBlock, ColorBlock>? converter,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(ColorBlock value) =>
            Target.colors = value;
    }
}
