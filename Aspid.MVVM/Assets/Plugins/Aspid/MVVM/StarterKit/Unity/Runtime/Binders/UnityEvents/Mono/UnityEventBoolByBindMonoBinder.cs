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

        protected override void OnBound() =>
            SetVisible();

        protected override void OnUnbound() =>
            SetVisible();

        /// <inheritdoc/>
        public void SetValue<T>(T value) { }

        private void SetVisible() =>
            _set?.Invoke(_isInvert ? !IsBound : IsBound);
    }
}
