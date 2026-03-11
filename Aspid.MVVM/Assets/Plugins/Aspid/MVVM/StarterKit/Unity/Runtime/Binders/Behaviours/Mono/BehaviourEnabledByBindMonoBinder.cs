using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that enables or disables itself depending on whether the ViewModel
    /// exposes a matching binding field for this binder.
    /// </summary>
    /// <remarks>
    /// Unlike conventional binders, this binder does not forward ViewModel values to a component property.
    /// Instead, it sets its own <see cref="Behaviour.enabled"/> to <see langword="true"/> when the ViewModel
    /// contains the expected binding field, and to <see langword="false"/> when it does not
    /// (or vice versa when <c>_isInvert</c> is enabled).
    /// <para>
    /// Only <see cref="BindMode.OneTime"/> is supported.
    /// </para>
    /// </remarks>
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled By Bind")]
    [BindModeOverride(modes: BindMode.OneTime)]
    public sealed partial class BehaviourEnabledByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [SerializeField] private bool _isInvert;

        private void OnEnable() =>
            SetEnable();

        /// <summary>
        /// Called when the ViewModel exposes a matching binding field.
        /// Enables this <see cref="Behaviour"/> (or disables it when <c>_isInvert</c> is <see langword="true"/>).
        /// </summary>
        protected override void OnBound() => 
            SetEnable();

        /// <summary>
        /// Called when the binder is unbound from a ViewModel property.
        /// Disables this <see cref="Behaviour"/> (or enables it when <c>_isInvert</c> is <see langword="true"/>).
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