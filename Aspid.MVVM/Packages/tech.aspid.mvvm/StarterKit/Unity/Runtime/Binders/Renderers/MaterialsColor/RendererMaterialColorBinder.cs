#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Renderer, Color}"/> that sets a named color property on all materials of a <see cref="Renderer"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-Renderer-MaterialsColor-1.1.0.xml" path="doc//member[@name='RendererMaterialColorBinder']/*" />
    [Serializable]
    public class RendererMaterialColorBinder : TargetBinder<Renderer, Color>, IColorBinder
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("The name of the shader color property to set on all materials.")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private ShaderPropertyId _colorPropertyId;
        private Material[]? _materials;
        
        private int ColorPropertyId => _colorPropertyId.Resolve(_colorPropertyName);
        
        protected sealed override Color Property
        {
            get => Target.sharedMaterial.GetColor(ColorPropertyId);
            set
            {
                _materials ??= Target.materials;

                foreach (var material in _materials)
                    material.SetColor(ColorPropertyId, value);
            }
        }
        
        /// <param name="target">The <see cref="Renderer"/> to bind.</param>
        /// <param name="colorPropertyName">The name of the shader color property to set.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Color"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
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

        /// <summary>
        /// Called after unbinding. Clears the cached materials array so it is re-fetched on the next bind.
        /// </summary>
        protected override void OnUnbound()
        {
            _materials = null;
            base.OnUnbound();
        }
    }
}