using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="MonoBinder"/> that targets a <typeparamref name="TComponent"/>, taken from the
    /// serialized field or found on the same GameObject.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> this binder targets.</typeparam>
    public abstract class ComponentMonoBinder<TComponent> : MonoBinder
        where TComponent : Component
    {
        [Tooltip("Target component. Found on this GameObject when empty.")]
        [SerializeField] private TComponent _component;

        private bool _isCached;

        /// <summary>
        /// Gets a value indicating whether binding is allowed: <see langword="false"/> when no target component can be found.
        /// </summary>
        public override bool CanBind
        {
            get
            {
                if (!IsAssigned(_component))
                    _isCached = false;

                return IsAssigned(CachedComponent);
            }
        }

        /// <summary>
        /// Gets the target component: the serialized one if assigned, otherwise the result of <see cref="ResolveComponent"/>, cached.
        /// </summary>
        /// <remarks>A missing or destroyed cached component is resolved again when <see cref="CanBind"/> is checked.</remarks>
        protected TComponent CachedComponent
        {
            get
            {
                if (_isCached) 
                    return _component;

                if (!IsAssigned(_component))
                    _component = ResolveComponent();

                _isCached = true;
                return _component;
            }
        }

        /// <summary>
        /// Called by Unity in the Editor when a serialized value changes. Fills the empty component field outside Play mode.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnValidate()</c>.
        /// </remarks>
        protected virtual void OnValidate()
        {
            if (Application.isPlaying) return;
            if (IsAssigned(_component)) return;

            _component = ResolveComponent();
        }

        /// <summary>
        /// Called when the serialized field is empty to find the target component. Override when the plain
        /// <see cref="Component.GetComponent{T}"/> is ambiguous, such as for a base type like <see cref="Behaviour"/>.
        /// </summary>
        /// <returns>The component to target.</returns>
        protected virtual TComponent ResolveComponent() =>
            GetComponent<TComponent>();

        // Unity's bool conversion: a destroyed component is not null to C#.
        private static bool IsAssigned(TComponent component) =>
            component;
    }
}
