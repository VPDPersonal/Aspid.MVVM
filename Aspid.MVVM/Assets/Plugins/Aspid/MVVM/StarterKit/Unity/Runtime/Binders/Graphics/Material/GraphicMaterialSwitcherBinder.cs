#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{Graphic, Material, Converter}"/> that switches the <see cref="Graphic.material"/>
    /// property between two <see cref="Material"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Graphic-Material-1.1.0.xml" path="doc//member[@name='GraphicMaterialSwitcherBinder']/*" />
    [Serializable]
    public sealed class GraphicMaterialSwitcherBinder : SwitcherBinder<Graphic, Material, Converter>
    {
        /// <inheritdoc/>
        public GraphicMaterialSwitcherBinder(
            RawImage target,
            Material trueValue,
            Material falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Material? value) =>
            Target.material = value;
    }
}