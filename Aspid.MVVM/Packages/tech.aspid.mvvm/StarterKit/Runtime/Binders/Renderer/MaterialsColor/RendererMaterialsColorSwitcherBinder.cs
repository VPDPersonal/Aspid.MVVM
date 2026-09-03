#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget, T}"/> that switches a color property on all materials of
    /// a <see cref="Renderer"/>.
    /// </summary>
    /// <remarks>
    /// Writes to <see cref="Renderer.materials"/>, so the materials are instanced for this renderer.
    /// </remarks>
    [Serializable]
    public sealed class RendererMaterialsColorSwitcherBinder : SwitcherBinder<Renderer, Color>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Shader color property set on every material.")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        private ShaderPropertyId _colorPropertyId;
        private Material[]? _materials;

        /// <param name="target">The renderer to bind.</param>
        /// <param name="trueValue">The color applied when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The color applied when the bound value is <see langword="false"/>.</param>
        /// <param name="colorPropertyName">The shader color property set on every material.</param>
        /// <param name="converter">
        /// The converter applied to the chosen value, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        public RendererMaterialsColorSwitcherBinder(
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

        /// <inheritdoc/>
        protected override void SetValue(Color value)
        {
            _materials ??= Target.materials;

            foreach (var material in _materials)
                material.SetColor(ColorPropertyId, value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            _materials = null;
            base.OnUnbound();
        }
    }
}
