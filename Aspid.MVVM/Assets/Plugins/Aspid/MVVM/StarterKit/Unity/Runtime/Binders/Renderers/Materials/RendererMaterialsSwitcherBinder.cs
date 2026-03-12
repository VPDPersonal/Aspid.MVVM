#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{Renderer, Material[]}"/> that switches the <see cref="Renderer.materials"/> array
    /// between two predefined <see cref="Material"/> arrays based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Renderer-Materials-1.1.0.xml" path="doc//member[@name='RendererMaterialsSwitcherBinder']/*" />
    [Serializable]
    public sealed class RendererMaterialsSwitcherBinder : SwitcherBinder<Renderer, Material[]?>
    {
        [Tooltip("The optional converter applied to each material before it is assigned to the Renderer.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="RendererMaterialsSwitcherBinder"/> without a converter.
        /// </summary>
        /// <param name="target">The <see cref="Renderer"/> to bind.</param>
        /// <param name="trueValue">The materials array applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The materials array applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode.</param>
        public RendererMaterialsSwitcherBinder(
            Renderer target,
            Material[]? trueValue,
            Material[]? falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <inheritdoc/>
        public RendererMaterialsSwitcherBinder(
            Renderer target,
            Material[]? trueValue,
            Material[]? falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        /// <summary>
        /// Called when applying the selected value to the <see cref="Renderer.materials"/> array.
        /// Applies the optional converter to each material before assignment.
        /// </summary>
        protected override void SetValue(Material[]? values) =>
            Target.SetMaterials(_converter, values);
    }
}