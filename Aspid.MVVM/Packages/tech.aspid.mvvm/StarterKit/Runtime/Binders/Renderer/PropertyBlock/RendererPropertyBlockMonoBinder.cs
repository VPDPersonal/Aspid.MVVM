using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that writes one shader property through a
    /// <see cref="MaterialPropertyBlock"/>.
    /// </summary>
    /// <remarks>
    /// The property name is resolved once, on binding. A blank name is reported and disables writes until the
    /// next bind.
    /// </remarks>
    /// <typeparam name="TValue">The type of value written to the shader property.</typeparam>
    public abstract partial class RendererPropertyBlockMonoBinder<TValue> : ComponentMonoBinder<Renderer>,
        IBinder<TValue>
    {
        [Tooltip("Shader property name, including the leading underscore.")]
        [SerializeField] private string _propertyName;

        private int _propertyId;
        private bool _isUsable;
        private MaterialPropertyBlock _block;

        /// <summary>
        /// Gets the id the property name resolved to.
        /// </summary>
        protected int PropertyId => _propertyId;

        /// <summary>
        /// Gets the block values are written into.
        /// </summary>
        protected MaterialPropertyBlock Block => _block ??= new MaterialPropertyBlock();

        /// <summary>
        /// Writes <paramref name="value"/> into the block and applies the block to the renderer.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(TValue value)
        {
            if (!_isUsable) return;

            Write(value);
            CachedComponent.SetPropertyBlock(Block);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            _isUsable = !string.IsNullOrWhiteSpace(_propertyName);

            if (!_isUsable)
            {
                this.LogError(
                    problem: "no shader property name is set",
                    consequence: "No value is written to the renderer.");

                return;
            }

            _propertyId = Shader.PropertyToID(_propertyName);
            CachedComponent.GetPropertyBlock(Block);
        }

        /// <summary>
        /// Writes <paramref name="value"/> into <see cref="Block"/> under <see cref="PropertyId"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        protected abstract void Write(TValue value);
    }
}
