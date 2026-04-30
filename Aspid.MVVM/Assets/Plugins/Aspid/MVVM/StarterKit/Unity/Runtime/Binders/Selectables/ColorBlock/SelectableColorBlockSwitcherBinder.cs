#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.UI.ColorBlock, UnityEngine.UI.ColorBlock>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColorBlock;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{Selectable, ColorBlock, Converter}"/> that switches the <see cref="Selectable.colors"/>
    /// property between two <see cref="ColorBlock"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Selectable-ColorBlock-1.1.0.xml" path="doc//member[@name='SelectableColorBlockSwitcherBinder']/*" />
    [Serializable]
    public sealed class SelectableColorBlockSwitcherBinder : SwitcherBinder<Selectable, ColorBlock, Converter>
    {
        /// <inheritdoc/>
        public SelectableColorBlockSwitcherBinder(
            Selectable target,
            ColorBlock trueValue,
            ColorBlock falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(ColorBlock value) =>
            Target.colors = value;
    }
}
#endif