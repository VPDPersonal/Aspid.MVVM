#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetColorBinder{Renderer}"/> that sets a named color property on all materials of a <see cref="Renderer"/>.
    /// The color property name defaults to <c>"_BaseColor"</c> and can be customized via the constructor.
    /// </summary>
    /// <include file="XmlExampleDoc-Renderer-MaterialsColor-1.1.0.xml" path="doc//member[@name='RendererMaterialColorBinder']/*" />
    [Serializable]
    public class RendererMaterialColorBinder : TargetColorBinder<Renderer>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("The name of the shader color property to set on all materials. Defaults to \"_BaseColor\".")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        protected sealed override Color Property
        {
            get => Target.material.GetColor(ColorPropertyId);
            set
            {
                foreach (var material in Target.materials)
                    material.SetColor(ColorPropertyId, value);
            }
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="RendererMaterialColorBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Renderer"/> to bind.</param>
        /// <param name="colorPropertyName">The name of the shader color property to set. Defaults to <c>"_BaseColor"</c>.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Color"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public RendererMaterialColorBinder(
            Renderer target,
            string colorPropertyName = "_BaseColor",
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _colorPropertyName = colorPropertyName;
        }
    }
}