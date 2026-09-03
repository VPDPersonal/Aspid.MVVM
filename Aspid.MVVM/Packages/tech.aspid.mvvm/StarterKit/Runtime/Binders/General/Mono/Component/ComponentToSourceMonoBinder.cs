using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that sends the target <typeparamref name="TComponent"/>
    /// reference to the ViewModel on binding.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> sent to the ViewModel.</typeparam>
    [BindModeOverride(BindMode.OneWayToSource)]
    public abstract class ComponentToSourceMonoBinder<TComponent> : ComponentMonoBinder<TComponent>, IReverseBinder<TComponent>
        where TComponent : Component
    {
        /// <inheritdoc/>
        public event Action<TComponent> ValueChanged;

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneWayToSource;

        /// <summary>
        /// Raises <see cref="ValueChanged"/> with the target component.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnBound()</c>.
        /// </remarks>
        protected override void OnBound() =>
            ValueChanged?.Invoke(CachedComponent);
    }

    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for any <see cref="Component"/> that also reports it as
    /// <see langword="object"/> via <see cref="IAnyReverseBinder"/>.
    /// </summary>
    /// <remarks>
    /// The <see langword="object"/> channel is not type-checked at compile time: a mismatch with the ViewModel member surfaces only at runtime.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Components/Component To Source Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Component/Component To Source Binder")]
    public sealed class ComponentToSourceMonoBinder : ComponentToSourceMonoBinder<Component>, IAnyReverseBinder
    {
        /// <inheritdoc/>
        public new event Action<object> ValueChanged;

        /// <inheritdoc/>
        protected override void OnBound()
        {
            base.OnBound();
            ValueChanged?.Invoke(CachedComponent);
        }
    }
}
