using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base class for <see cref="MonoBinder"/> implementations that operate on a <see cref="Component"/>.
    /// Provides lazy resolution of the target component — either from
    /// the serialized field or via <see cref="Component.GetComponent{T}"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> this binder targets.</typeparam>
    public abstract class ComponentMonoBinder<TComponent> : MonoBinder
        where TComponent : Component
    {
        [Tooltip("Target component this binder operates on. Resolved automatically via GetComponent<TComponent> if left empty.")]
        [SerializeField] private TComponent _component;

        private bool _isCached;

        /// <summary>
        /// Indicates whether binding is allowed: <see langword="false"/> when no target component can be found.
        /// </summary>
        /// <remarks>
        /// Without this the binder bound successfully and then threw a <see cref="System.NullReferenceException"/>
        /// on the first value, from inside a leaf class's property setter — a message naming neither the binder nor
        /// the GameObject it sits on. The serializable binders have had the equivalent guard on
        /// <c>TargetBinder</c> all along; this is the same check on the component side.
        /// </remarks>
        public override bool IsBind => IsAssigned(CachedComponent);

        /// <summary>
        /// Gets the target component.
        /// Returns the serialized value if assigned;
        /// otherwise resolves it via <see cref="Component.GetComponent{T}"/> and caches the result.
        /// </summary>
        /// <remarks>
        /// "Assigned" is decided with Unity's own <see cref="Object"/> conversion rather than <c>is not null</c>:
        /// an empty or broken object reference reaches managed code as a wrapper that is not <see langword="null"/>
        /// to C# yet points at nothing, and treating that as assigned would skip the fallback and hand every
        /// caller a component it cannot use.
        /// </remarks>
        protected TComponent CachedComponent
        {
            get
            {
                if (_isCached) return _component;
                _isCached = true;

                if (IsAssigned(_component)) return _component;
                return _component = ResolveComponent();
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
            if (IsAssigned(_component)) return;

            _component = ResolveComponent();
        }

        /// <summary>
        /// Finds the target component on this GameObject when the serialized field is empty.
        /// </summary>
        /// <remarks>
        /// Override where the plain search is ambiguous. It is fine for a concrete component type — there is only
        /// one <c>Slider</c> to find — but a binder typed on a base class such as <see cref="Behaviour"/> matches
        /// anything, including the binder itself, and a binder that disables itself stops working with no
        /// indication of why.
        /// </remarks>
        protected virtual TComponent ResolveComponent() =>
            GetComponent<TComponent>();

        /// <summary>
        /// Reports whether <paramref name="component"/> refers to a live component.
        /// </summary>
        /// <remarks>
        /// The conversion to <see langword="bool"/> is Unity's own operator, reached through the
        /// <see cref="Component"/> constraint: a destroyed component is not a <see langword="null"/>
        /// reference, so <c>is not null</c> would report it as assigned.
        /// </remarks>
        private static bool IsAssigned(TComponent component) => component;
    }
}