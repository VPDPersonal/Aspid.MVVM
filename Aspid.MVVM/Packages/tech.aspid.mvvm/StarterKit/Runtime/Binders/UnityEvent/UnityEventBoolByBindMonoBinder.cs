using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with whether a binding exists.
    /// </summary>
    /// <remarks>
    /// The bound value is ignored; only the presence of a binding matters.
    /// </remarks>
    [BindModeOverride(BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Bool By Bind")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Bool By Bind")]
    public sealed partial class UnityEventBoolByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [Tooltip("Invoke with the inverted state.")]
        [SerializeField] private bool _isInvert;

        [Tooltip("Invoked with the bound state.")]
        [SerializeField] private UnityEvent<bool> _set;

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneTime;

        private void OnValidate() =>
            Invoke();

        private void OnEnable() =>
            Invoke();

        /// <inheritdoc/>
        protected override void OnBound() =>
            Invoke();

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            Invoke();

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value) { }

        private void Invoke() =>
            _set?.Invoke(_isInvert ? !IsBound : IsBound);
    }
}
