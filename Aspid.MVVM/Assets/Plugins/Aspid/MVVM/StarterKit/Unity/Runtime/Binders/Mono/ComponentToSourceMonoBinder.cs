using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that sends the cached <typeparamref name="TComponent"/>
    /// reference back to the ViewModel via <see cref="IReverseBinder{T}"/> when binding is established.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> whose reference is sent to the ViewModel.</typeparam>
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    public abstract class ComponentToSourceMonoBinder<TComponent> : ComponentMonoBinder<TComponent>, IReverseBinder<TComponent>
        where TComponent : Component
    {
        /// <summary>
        /// Raised with the cached <typeparamref name="TComponent"/> reference when binding is established.
        /// </summary>
        public event Action<TComponent> ValueChanged;

        /// <summary>
        /// Called after binding is established.
        /// Raises <see cref="ValueChanged"/> with the cached <typeparamref name="TComponent"/> reference.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnBound()</c> to ensure
        /// <see cref="ValueChanged"/> is raised with the component reference.
        /// </remarks>
        protected override void OnBound() =>
            ValueChanged?.Invoke(CachedComponent);
    }

    /// <summary>
    /// Concrete <see cref="ComponentToSourceMonoBinder{TComponent}"/> that also implements <see cref="IAnyReverseBinder"/>,
    /// raising <see cref="ValueChanged"/> with the <see cref="Component"/> reference cast to <see langword="object"/>.
    /// </summary>
    /// <remarks>
    /// Use with caution: since <see cref="ValueChanged"/> carries a non-generic <see langword="object"/>,
    /// the type contract between the View and ViewModel is not enforced at compile time.
    /// A type mismatch will only be detected at runtime.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Components/Component To Source Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Component/Component To Source Binder")]
    public sealed class ComponentToSourceMonoBinder : ComponentToSourceMonoBinder<Component>, IAnyReverseBinder
    {
        /// <summary>
        /// Raised with the attached <see cref="Component"/> reference as <see langword="object"/> when binding is established.
        /// </summary>
        public new event Action<object> ValueChanged;

        /// <inheritdoc/>
        protected override void OnBound()
        {
            base.OnBound();
            ValueChanged?.Invoke(CachedComponent);
        }
    }
}