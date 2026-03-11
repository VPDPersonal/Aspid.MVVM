using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that shows or hides the <see cref="GameObject"/> this component is attached to
    /// based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// <see cref="GameObject.activeSelf"/> value is sent back to the ViewModel.
    /// Supports optional value inversion via <c>_isInvert</c>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Visible")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed partial class GameObjectVisibleMonoBinder : MonoBinder, 
        IBinder<bool>, 
        IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool> ValueChanged;
        
        [Tooltip("When enabled, inverts the bound bool value before applying it.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Shows or hides the <see cref="GameObject"/> by calling <see cref="GameObject.SetActive"/>
        /// with <paramref name="value"/> (optionally inverted).
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value) =>
            gameObject.SetActive(GetConvertedValue(value));

        /// <summary>
        /// Called when binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, fires <see cref="ValueChanged"/> with the
        /// current <see cref="GameObject.activeSelf"/> value.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(gameObject.activeSelf));
        }

        private bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}