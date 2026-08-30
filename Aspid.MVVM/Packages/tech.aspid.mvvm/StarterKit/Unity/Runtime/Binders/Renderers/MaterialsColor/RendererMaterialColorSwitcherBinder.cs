#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget,T}">SwitcherBinder&lt;Renderer, Color&gt;</see> that switches a named color property on all materials of a <see cref="Renderer"/>
    /// between two <see cref="Color"/> values based on the bound boolean ViewModel value.
    /// </summary>
    [Serializable]
    public sealed class RendererMaterialColorSwitcherBinder : SwitcherBinder<Renderer, Color>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("The name of the shader color property to set on all materials.")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        private ShaderPropertyId _colorPropertyId;

        private Material[]? _materials;

        /// <param name="target">The <see cref="Renderer"/> to bind.</param>
        /// <param name="trueValue">The color applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The color applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="colorPropertyName">The name of the shader color property to set.</param>
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

        private int ColorPropertyId => _colorPropertyId.Resolve(_colorPropertyName);

        /// <summary>
        /// Sets the named color property on all Renderer materials.
        /// </summary>
        /// <remarks>
        /// The Renderer's materials array is fetched once and cached, avoiding the per-call
        /// allocation that <see cref="Renderer.materials"/> incurs on every access.
        /// </remarks>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Color value)
        {
            _materials ??= Target.materials;

            foreach (var material in _materials)
                material.SetColor(ColorPropertyId, value);
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
