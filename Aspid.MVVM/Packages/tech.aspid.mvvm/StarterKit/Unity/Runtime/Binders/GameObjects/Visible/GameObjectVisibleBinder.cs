#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{GameObject}"/> that sets the active state of a <see cref="GameObject"/>
    /// via <see cref="GameObject.SetActive"/> when the bound ViewModel value changes.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// <see cref="GameObject.activeSelf"/> value is sent back to the ViewModel.
    /// Supports optional value inversion.
    /// </remarks>
    /// <include file="XmlExampleDoc-GameObject-Visible-1.1.0.xml" path="doc//member[@name='GameObjectVisibleBinder']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed class GameObjectVisibleBinder : TargetBinder<GameObject>, 
        IBinder<bool>,
        IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool>? ValueChanged;
        
        [Tooltip("When enabled, inverts the bound bool value before applying it.")]
        [SerializeField] private bool _isInvert;
        
        /// <summary>
        /// Initializes a new instance of <see cref="GameObjectVisibleBinder"/> targeting the specified <see cref="GameObject"/>.
        /// </summary>
        /// <param name="target">The <see cref="GameObject"/> whose active state is bound.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public GameObjectVisibleBinder(GameObject target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _isInvert = isInvert;
        }

        /// <summary>
        /// Sets the <see cref="GameObject"/> active state to <paramref name="value"/> (optionally inverted).
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            Target.SetActive(GetConvertedValue(value));
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(Target.activeSelf));
        }

        private bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}