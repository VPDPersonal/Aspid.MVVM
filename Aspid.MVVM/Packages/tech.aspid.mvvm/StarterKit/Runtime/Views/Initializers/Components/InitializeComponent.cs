#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Serializable slot that resolves a <typeparamref name="T"/> from a component, a plain reference,
    /// a <see cref="ScriptableObject"/> or the DI container, as chosen by <see cref="StarterKit.ResolveType"/>.
    /// </summary>
    /// <typeparam name="T">The resolved type.</typeparam>
    [Serializable]
    public abstract class InitializeComponent<T>
        where T : class
    {
        [Tooltip("Where the instance comes from.")]
        [SerializeField] private ResolveType _resolveType;

        [Tooltip("Component used when Resolve Type is Component.")]
        [SerializeField] private Component? _component;

        [Tooltip("ScriptableObject used when Resolve Type is ScriptableObject.")]
        [SerializeField] private ScriptableObject? _scriptableObject;

        [Tooltip("Plain instance used when Resolve Type is Reference.")]
        [SerializeReference] private T? _reference;

#if ASPID_MVVM_ZENJECT_INTEGRATION
        [field: NonSerialized]
        internal Zenject.DiContainer? ZenjectContainer { get; set; }
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
        [field: NonSerialized]
        internal VContainer.IObjectResolver? VContainerContainer { get; set; }
#endif

        /// <summary>
        /// Gets the chosen source of the instance.
        /// </summary>
        protected ResolveType ResolveType => _resolveType;

        /// <summary>
        /// Resolves the instance from the chosen source.
        /// </summary>
        /// <returns>The instance, or <see langword="null"/> when the reference is empty or of the wrong type.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the DI type is missing or not registered.</exception>
        public T? Resolve()
        {
            switch (_resolveType)
            {
                case ResolveType.Component: return _component as T;
                case ResolveType.Reference: return _reference;
                case ResolveType.ScriptableObject: return _scriptableObject as T;
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case ResolveType.Di:
                    var type = GetTypeForDi() ??
                        throw new InvalidOperationException("DI type is not set or not found");
#if ASPID_MVVM_ZENJECT_INTEGRATION
                    if (ZenjectContainer?.TryResolve(type) is T zenjectResult) return zenjectResult;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
                    if (VContainerContainer?.TryResolve(type, out var vcontainerResult) ?? false)
                        return vcontainerResult as T;
#endif
                    throw new InvalidOperationException($"{type} is not registered in the DI container");
#endif
                default: throw new ArgumentOutOfRangeException(nameof(_resolveType), _resolveType, null);
            }
        }

        /// <summary>
        /// Clears the references that do not belong to the chosen <see cref="ResolveType"/> source.
        /// </summary>
        public virtual void Validate()
        {
            switch (_resolveType)
            {
                case ResolveType.Component:
                    _reference = null;
                    _scriptableObject = null;
                    break;

                case ResolveType.Reference:
                    _component = null;
                    _scriptableObject = null;
                    break;

                case ResolveType.ScriptableObject:
                    _component = null;
                    _reference = null;
                    break;
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION

                case ResolveType.Di:
                    _component = null;
                    _reference = null;
                    _scriptableObject = null;
                    break;
#endif

                default: throw new ArgumentOutOfRangeException(nameof(_resolveType), _resolveType, null);
            }
        }

        /// <summary>
        /// Gets the type requested from the DI container.
        /// </summary>
        /// <returns>The type, or <see langword="null"/> when none is configured.</returns>
        protected abstract Type? GetTypeForDi();
    }
}
