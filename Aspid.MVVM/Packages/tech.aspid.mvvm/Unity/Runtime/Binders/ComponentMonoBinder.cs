using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="MonoBinder"/> with an assigned target component or a fallback on the same GameObject.
    /// </summary>
    /// <typeparam name="TComponent">The target <see cref="Component"/> type.</typeparam>
    public abstract class ComponentMonoBinder<TComponent> : MonoBinder
        where TComponent : Component
    {
        [Tooltip("Target component. Found on this GameObject when empty.")]
        [SerializeField] private TComponent _component;

        private bool _isCached;

        /// <summary>
        /// Gets the cached target component. Uses <see cref="ResolveComponent"/> if no component is assigned.
        /// </summary>
        /// <remarks>Binding resolves the target again if the cached component is missing or destroyed.</remarks>
        protected TComponent CachedComponent
        {
            get
            {
                if (_isCached)
                    return _component;

                if (_component == null)
                    _component = ResolveComponent();

                _isCached = true;
                return _component;
            }
        }

        /// <summary>
        /// Called when Unity adds or resets the binder in the Editor to find its default target.
        /// </summary>
        /// <remarks>Overrides must call the base implementation.</remarks>
        protected override void Reset()
        {
            base.Reset();
            
            _isCached = false;
            _component = ResolveComponent();
        }

        /// <summary>
        /// Called before attachment to resolve the required target component.
        /// </summary>
        /// <remarks>Overrides must call the base implementation before acquiring resources.</remarks>
        /// <exception cref="InvalidOperationException">No live target component can be resolved.</exception>
        protected override void OnBinding()
        {
            _isCached = true;
            if (_component != null) return;

            _component = ResolveComponent();
            if (_component == null)
                throw new InvalidOperationException($"Assign a {typeof(TComponent).Name} target or add one to the same GameObject before binding.");
        }

        /// <summary>
        /// Called to resolve the default target. Override when <see cref="Component.GetComponent{T}()"/> is ambiguous.
        /// </summary>
        /// <returns>The target component or <see langword="null"/> if none is found.</returns>
        protected virtual TComponent ResolveComponent() =>
            GetComponent<TComponent>();
    }
}
