using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base class for <see cref="MonoBinder"/> implementations that operate on a <see cref="Component"/>.
    /// Provides lazy resolution of the target component — either from the serialized field or via <see cref="Component.GetComponent{T}"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> this binder targets.</typeparam>
    public abstract class ComponentMonoBinder<TComponent> : MonoBinder
        where TComponent : Component
    {
        [Tooltip("Target component this binder operates on. Resolved automatically via GetComponent<TComponent> if left empty.")]
        [SerializeField] private TComponent _component;

        private bool _isCached;

        /// <summary>
        /// Gets the target component.
        /// Returns the serialized value if assigned; otherwise resolves it via <see cref="Component.GetComponent{T}"/> and caches the result.
        /// </summary>
        protected TComponent CachedComponent
        {
            get
            {
                if (_isCached) return _component;
                _isCached = true;

                if (_component is not null) return _component;
                return _component = GetComponent<TComponent>();
            }
        }

        /// <summary>
        /// Called by Unity in the Editor when a serialized field value changes.
        /// Automatically resolves and assigns the component if it is not yet set and the application is not playing.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnValidate()</c> to preserve
        /// automatic component resolution in the Editor.
        /// </remarks>
        protected virtual void OnValidate()
        {
            if (Application.isPlaying) return;
            if (_component is not null) return;

            _component = GetComponent<TComponent>();
        }
    }
}