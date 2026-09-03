#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget, TProperty}"/> that binds a color property on all materials of
    /// a <see cref="Renderer"/>.
    /// </summary>
    /// <remarks>
    /// Writes to <see cref="Renderer.materials"/>, so the materials are instanced for this renderer.
    /// </remarks>
    [Serializable]
    public class RendererMaterialsColorBinder : TargetBinder<Renderer, Color>, IColorBinder
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Shader color property set on every material.")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        private ShaderPropertyId _colorPropertyId;
        private Material[]? _materials;

        /// <param name="target">The renderer to bind.</param>
        /// <param name="colorPropertyName">The shader color property set on every material.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public RendererMaterialsColorBinder(
            Renderer target,
            string colorPropertyName = "_BaseColor",
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _colorPropertyName = colorPropertyName;
        }

        private int ColorPropertyId => _colorPropertyId.Resolve(_colorPropertyName);

        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.sharedMaterial
                ? Target.sharedMaterial.GetColor(ColorPropertyId)
                : default;
            set
            {
                _materials ??= Target.materials;

                foreach (var material in _materials)
                    material.SetColor(ColorPropertyId, value);
            }
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            _materials = null;
            base.OnUnbound();
        }
    }
}
