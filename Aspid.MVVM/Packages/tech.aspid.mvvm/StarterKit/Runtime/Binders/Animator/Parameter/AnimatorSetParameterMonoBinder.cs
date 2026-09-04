using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="ComponentMonoBinder{TComponent}"/> that sets a typed <see cref="Animator"/> parameter.
    /// </summary>
    /// <remarks>
    /// In <see cref="BindMode.OneWayToSource"/> the setter is handed to the ViewModel as an
    /// <see cref="IRelayCommand{T}"/> or an <see cref="Action{T}"/>. The last value is re-applied on enable.
    /// </remarks>
    /// <typeparam name="T">The parameter value type.</typeparam>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract partial class AnimatorSetParameterMonoBinder<T> : ComponentMonoBinder<Animator>,
        IBinder<T>,
        IReverseBinder<Action<T>>,
        IReverseBinder<IRelayCommand<T>>
    {
        [NonSerialized] private T _value;
        [NonSerialized] private bool _hasValue;

        private IRelayCommand<T> _command;
        private AnimatorParameterProbe _probe;
        private Action<Action<T>> _reverseAction;
        private Action<IRelayCommand<T>> _reverseCommand;

        [field: Tooltip("Animator parameter to set.")]
        [field: SerializeField] protected string ParameterName { get; private set; }

        event Action<Action<T>> IReverseBinder<Action<T>>.ValueChanged
        {
            add => _reverseAction += value;
            remove => _reverseAction -= value;
        }

        event Action<IRelayCommand<T>> IReverseBinder<IRelayCommand<T>>.ValueChanged
        {
            add => _reverseCommand += value;
            remove => _reverseCommand -= value;
        }

        /// <summary>
        /// The parameter type inferred from <typeparamref name="T"/>, or <see langword="null"/> to match by name only.
        /// </summary>
        protected virtual AnimatorControllerParameterType? ParameterType =>
            AnimatorParameterTypes.Of<T>();

        /// <summary>
        /// Re-applies the last value and refreshes the command's <see cref="IRelayCommand.CanExecute()"/>.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnEnable()</c>.
        /// </remarks>
        protected virtual void OnEnable()
        {
            if (_hasValue) SetParameterChecked(_value);
            _command?.NotifyCanExecuteChanged();
        }

        /// <summary>
        /// Refreshes the command's <see cref="IRelayCommand.CanExecute()"/>.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnDisable()</c>.
        /// </remarks>
        protected virtual void OnDisable() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Sets the parameter when <see cref="CanExecute"/> allows it.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(T value) =>
            SetParameterChecked(value);

        /// <inheritdoc/>
        protected sealed override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            if (_reverseCommand is not null)
            {
                _command = new RelayCommand<T>(SetParameterChecked, CanExecute);
                _reverseCommand.Invoke(_command);
            }
            else
            {
                _reverseAction?.Invoke(SetParameterChecked);
            }
        }

        /// <inheritdoc/>
        protected sealed override void OnUnbinding()
        {
            _value = default;
            _hasValue = false;

            _command = null;
            _reverseAction?.Invoke(null);
            _reverseCommand?.Invoke(null);
        }

        /// <summary>
        /// Writes <paramref name="value"/> to the parameter named <see cref="ParameterName"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        protected abstract void SetParameter(T value);

        /// <summary>
        /// Whether the parameter may be set: the animator is active and its controller has the parameter.
        /// </summary>
        /// <param name="value">The value that would be written.</param>
        /// <returns><see langword="true"/> when the parameter may be set.</returns>
        protected virtual bool CanExecute(T value) =>
            CachedComponent && CachedComponent.gameObject.activeInHierarchy &&
            _probe.IsUsable(CachedComponent, ParameterName, ParameterType, this);

        private void SetParameterChecked(T value)
        {
            _value = value;
            _hasValue = true;

            if (CanExecute(value))
                SetParameter(value);
        }
    }
}
