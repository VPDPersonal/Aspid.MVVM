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
    [BindModeOverride(modes: BindMode.OneTime)]
    public sealed partial class UnityEventBoolByBindMonoBinder : MonoBinder, IAnyBinder
    {
        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneTime;

        [Tooltip("Inverts the bound state before passing it to the event.")]
        [SerializeField] private bool _isInvert;
        [Tooltip("The event invoked with the current bound state.")]
        [SerializeField] private UnityEvent<bool> _set;

        private void OnValidate() =>
            InvokeEvent();

        private void OnEnable() =>
            InvokeEvent();

        /// <summary>
        /// Called when binding is established. Invokes the event with the bound state it implies.
        /// </summary>
        /// <remarks>
        /// This binder never reads a value — the invoked state follows whether a binding exists, so both this
        /// hook and its unbound counterpart do the same thing.
        /// </remarks>
        protected override void OnBound() =>
            InvokeEvent();

        /// <summary>
        /// Called when the binding is released. Invokes the event with the bound state it implies.
        /// </summary>
        protected override void OnUnbound() =>
            InvokeEvent();

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value) { }

        private void InvokeEvent() =>
            _set?.Invoke(_isInvert ? !IsBound : IsBound);
    }
}
