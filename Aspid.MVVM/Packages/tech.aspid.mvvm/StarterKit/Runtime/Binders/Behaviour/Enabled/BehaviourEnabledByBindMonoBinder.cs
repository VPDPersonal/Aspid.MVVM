using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that stays enabled while a binding exists and disables itself otherwise.
    /// </summary>
    /// <remarks>
    /// The bound value is ignored; only the presence of a binding matters.
    /// </remarks>
    [BindModeOverride(BindMode.OneTime)]
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled By Bind")]
    public sealed partial class BehaviourEnabledByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [Tooltip("Disable while bound instead of enabling.")]
        [SerializeField] private bool _isInvert;

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneTime;

        private void OnEnable() =>
            SetEnabled();

        /// <inheritdoc/>
        protected override void OnBound() =>
            SetEnabled();

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            SetEnabled();

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value) { }

        private void SetEnabled() =>
            enabled = _isInvert ? !IsBound : IsBound;
    }
}
