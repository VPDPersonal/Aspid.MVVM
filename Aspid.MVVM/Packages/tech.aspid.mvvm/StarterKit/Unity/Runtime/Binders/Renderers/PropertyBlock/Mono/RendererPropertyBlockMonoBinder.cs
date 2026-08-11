using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Renderer}"/> that writes one shader property through a
    /// <see cref="MaterialPropertyBlock"/>.
    /// </summary>
    /// <remarks>
    /// The only path the package offered to a shader value was <see cref="Renderer.material"/>, which instantiates a
    /// copy of the material on first touch: every bound object gets its own material, batching stops, and the copies leak
    /// into the scene. A property block overrides the value per renderer while the material stays shared.
    /// <para/>
    /// The block is created once and reused, so a value per frame allocates nothing. The property name is resolved to an
    /// id when binding is established — <see cref="Shader.PropertyToID"/> hashes the string, and doing it per value
    /// would pay for the hash on every write.
    /// <para/>
    /// A blank property name is reported once rather than on every value, and nothing is written until it is fixed.
    /// </remarks>
    /// <typeparam name="TValue">The type of value written to the shader property.</typeparam>
    public abstract partial class RendererPropertyBlockMonoBinder<TValue> : ComponentMonoBinder<Renderer>, IBinder<TValue>
    {
        [Tooltip("Name of the shader property to override, exactly as the shader declares it — including the leading underscore.")]
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
