#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherColorBinder{Renderer}"/> that switches a named color property on all materials of a <see cref="Renderer"/>
    /// between two <see cref="Color"/> values based on the bound boolean ViewModel value.
    /// The color property name defaults to <c>"_BaseColor"</c> and can be customized via the constructor.
    /// </summary>
    /// <include file="XmlExampleDoc-Renderer-MaterialsColor-1.1.0.xml" path="doc//member[@name='RendererMaterialColorSwitcherBinder']/*" />
    [Serializable]
    public sealed class RendererMaterialColorSwitcherBinder : SwitcherColorBinder<Renderer>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("The name of the shader color property to set on all materials. Defaults to \"_BaseColor\".")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        private int? _colorPropertyId;

        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        /// <summary>
        /// Initializes a new instance of <see cref="RendererMaterialColorSwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Renderer"/> to bind.</param>
        /// <param name="trueValue">The color applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The color applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="colorPropertyName">The name of the shader color property to set. Defaults to <c>"_BaseColor"</c>.</param>
        /// <param name="converter">The converter used to transform the selected <see cref="Color"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode.</param>
        public RendererMaterialColorSwitcherBinder(
            Renderer target,
            Color trueValue,
            Color falseValue,
            string colorPropertyName = "_BaseColor",
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _colorPropertyName = colorPropertyName;
        }

        /// <summary>
        /// Called when applying the selected value to the material color property.
        /// Sets the named color property on all Renderer materials.
        /// </summary>
        protected override void SetValue(Color value)
        {
            foreach (var material in Target.materials)
                material.SetColor(ColorPropertyId, value);
        }
    }
}