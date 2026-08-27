using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that enables or disables itself depending on whether the ViewModel
    /// exposes a matching binding field for this binder.
    /// </summary>
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled By Bind")]
    [BindModeOverride(modes: BindMode.OneTime)]
    public sealed partial class BehaviourEnabledByBindMonoBinder : MonoBinder, IAnyBinder
    {
        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneTime;

        [Tooltip("Inverts enabled state (disable while bound). Ignores the bound value entirely.")]
        [SerializeField] private bool _isInvert;

        private void OnEnable() =>
            SetEnable();
        
        /// <summary>
        /// Refreshes <see cref="Behaviour.enabled"/> based on whether a binding is currently established.
        /// </summary>
        protected override void OnBound() =>
            SetEnable();

        /// <summary>
        /// Applies the current binding state to <see cref="Behaviour.enabled"/>.
        /// </summary>
        protected override void OnUnbound() =>
            SetEnable();
        
        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value) { }
        
        private void SetEnable() =>
            enabled = _isInvert ? !IsBound : IsBound;
    }
}