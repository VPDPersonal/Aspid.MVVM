using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Renderer}"/> that writes one shader property through a
    /// <see cref="MaterialPropertyBlock"/>.
    /// </summary>
    /// <remarks>
    /// The property name is resolved to an id once, when binding is established. A blank name logs an error and
    /// disables writes until the binder is rebound.
    /// </remarks>
    /// <typeparam name="TValue">The type of value written to the shader property.</typeparam>
    public abstract partial class RendererPropertyBlockMonoBinder<TValue> : ComponentMonoBinder<Renderer>, IBinder<TValue>
    {
        [Tooltip("Shader property name, exactly as declared (including the leading underscore).")]
        [SerializeField] private string _propertyName;

        private int _propertyId;
        private bool _isUsable;
        private MaterialPropertyBlock _block;

        /// <summary>
        /// Gets the id the property name was resolved to.
        /// </summary>
        protected int PropertyId => _propertyId;

        /// <summary>
        /// Gets the block every value is written into.
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

        /// <summary>
        /// Called when the binder is bound. Resolves the property name to an id and reports a blank one.
        /// </summary>
        protected override void OnBound()
        {
            _isUsable = !string.IsNullOrWhiteSpace(_propertyName);

            if (!_isUsable)
            {
                Debug.LogError($"[{GetType().Name}] No shader property name set.", context: this);
                return;
            }

            _propertyId = Shader.PropertyToID(_propertyName);
            CachedComponent.GetPropertyBlock(Block);
        }

        /// <summary>
        /// Writes <paramref name="value"/> into <see cref="Block"/> under <see cref="PropertyId"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected abstract void Write(TValue value);
    }
}
