#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="TargetBinder{TTarget}"/> that sets a typed <see cref="Animator"/> parameter.
    /// </summary>
    /// <remarks>
    /// In <see cref="BindMode.OneWayToSource"/> the setter is handed to the ViewModel as an
    /// <see cref="IRelayCommand{T}"/> or an <see cref="Action{T}"/>.
    /// </remarks>
    /// <typeparam name="T">The parameter value type.</typeparam>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract class AnimatorSetParameterBinder<T> : TargetBinder<Animator>,
        IBinder<T>,
        IReverseBinder<Action<T>?>,
        IReverseBinder<IRelayCommand<T>?>
    {
        private IRelayCommand<T>? _command;
        private AnimatorParameterProbe _probe;
        private Action<Action<T>?>? _reverseAction;
        private Action<IRelayCommand<T>?>? _reverseCommand;

        [field: Tooltip("Animator parameter to set.")]
        [field: SerializeField] protected string ParameterName { get; private set; } = string.Empty;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected AnimatorSetParameterBinder() { }

        /// <param name="target">The animator to bind.</param>
        /// <param name="parameterName">The parameter to set.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parameterName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        protected AnimatorSetParameterBinder(Animator target, string parameterName, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
        }

        event Action<Action<T>?>? IReverseBinder<Action<T>?>.ValueChanged
        {
            add => _reverseAction += value;
            remove => _reverseAction -= value;
        }

        event Action<IRelayCommand<T>?>? IReverseBinder<IRelayCommand<T>?>.ValueChanged
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
        /// Notifies the command handed to the ViewModel that <see cref="IRelayCommand.CanExecute()"/> may have changed.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Sets the parameter when <see cref="CanExecute"/> allows it.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(T? value) =>
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
            _command = null;
            _reverseAction?.Invoke(null);
            _reverseCommand?.Invoke(null);
        }

        /// <summary>
        /// Writes <paramref name="value"/> to the parameter named <see cref="ParameterName"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        protected abstract void SetParameter(T? value);

        /// <summary>
        /// Whether the parameter may be set: the animator is active and its controller has the parameter.
        /// </summary>
        /// <param name="value">The value that would be written.</param>
        /// <returns><see langword="true"/> when the parameter may be set.</returns>
        protected virtual bool CanExecute(T? value) =>
            Target && Target.gameObject.activeInHierarchy &&
            _probe.IsUsable(Target, ParameterName, ParameterType, this);

        private void SetParameterChecked(T? value)
        {
            if (CanExecute(value))
                SetParameter(value);
        }
    }
}
