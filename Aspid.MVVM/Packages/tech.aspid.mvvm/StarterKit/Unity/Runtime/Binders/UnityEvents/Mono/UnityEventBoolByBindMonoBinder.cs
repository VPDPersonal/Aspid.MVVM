using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the current bound state each time binding changes.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Bool By Bind")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Bool By Bind")]
    public sealed class UnityEventBoolByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [Tooltip("When enabled, the bound state value is logically inverted before being passed to the event.")]
        [SerializeField] private bool _isInvert;
        [Tooltip("The event invoked with the current bound state.")]
        [SerializeField] private UnityEvent<bool> _set;

        private void OnValidate() =>
            SetVisible();

        private void OnEnable() =>
            SetVisible();

        /// <summary>
        /// Called when binding is established. Applies the visibility that the new bound state implies.
        /// </summary>
        /// <remarks>
        /// This binder never reads a value — visibility follows whether a binding exists, so both this hook and
        /// its unbound counterpart do the same thing.
        /// </remarks>
        protected override void OnBound() =>
            SetVisible();

        /// <summary>
        /// Called when the binding is released. Applies the visibility that the new bound state implies.
        /// </summary>
        /// <inheritdoc cref="OnBound"/>
        protected override void OnUnbound() =>
            SetVisible();

        /// <inheritdoc/>
        public void SetValue<T>(T value) { }

        private void SetVisible() =>
            _set?.Invoke(_isInvert ? !IsBound : IsBound);
    }
}
