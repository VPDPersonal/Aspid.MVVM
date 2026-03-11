#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{Animator}"/> that sets a typed parameter on a
    /// <see cref="Animator"/> when the bound ViewModel value changes.
    /// </summary>
    /// <typeparam name="T">The type of the Animator parameter value.</typeparam>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWay"/>, <see cref="BindMode.OneTime"/>, and
    /// <see cref="BindMode.OneWayToSource"/>. In <see cref="BindMode.OneWayToSource"/> mode the binder
    /// exposes <see cref="SetParameter"/> to the ViewModel either as a plain <see cref="Action{T}"/>
    /// or as an <see cref="IRelayCommand{T}"/> whose <c>CanExecute</c> mirrors
    /// <see cref="CanExecute"/>.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract class AnimatorSetParameterBinder<T> : TargetBinder<Animator>,
        IBinder<T>,
        IReverseBinder<Action<T>?>,
        IReverseBinder<IRelayCommand<T>?>
    {
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

        private IRelayCommand<T>? _command;
        private Action<Action<T>?>? _reverseAction;
        private Action<IRelayCommand<T>?>? _reverseCommand;
        
        [field: SerializeField]
        [field: Tooltip("The name of the Animator parameter to set.")]
        protected string ParameterName { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorSetParameterBinder{T}"/>.
        /// </summary>
        /// <param name="target">The <see cref="Animator"/> whose parameter is set.</param>
        /// <param name="parameterName">The name of the Animator parameter to set.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="parameterName"/> is <see langword="null"/>.
        /// </exception>
        protected AnimatorSetParameterBinder(Animator target, string parameterName, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
        }

        /// <summary>
        /// Notifies the bound <see cref="IRelayCommand{T}"/> that its <c>CanExecute</c> state may have changed.
        /// Has no effect when the binder is not in <see cref="BindMode.OneWayToSource"/> mode.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Receives <paramref name="value"/> from the ViewModel and forwards it to <see cref="SetParameter"/>
        /// if <see cref="CanExecute"/> returns <see langword="true"/>.
        /// </summary>
        /// <param name="value">The new parameter value received from the ViewModel.</param>
        public void SetValue(T? value)
        {
            if (!CanExecute(value)) return;
            SetParameter(value);
        }

        /// <summary>
        /// Applies <paramref name="value"/> to the Animator parameter identified by <see cref="ParameterName"/>.
        /// </summary>
        /// <param name="value">The value to apply to the Animator parameter.</param>
        protected abstract void SetParameter(T? value);

        /// <summary>
        /// Called when binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, exposes <see cref="SetParameter"/> to the ViewModel
        /// as an <see cref="IRelayCommand{T}"/> or a plain <see cref="Action{T}"/>.
        /// </summary>
        protected sealed override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            if (_reverseCommand is not null)
            {
                _command = new RelayCommand<T>(SetParameter, CanExecute);
                _reverseCommand.Invoke(_command);
            }
            else
            {
                _reverseAction?.Invoke(SetParameter);
            }
        }

        /// <summary>
        /// Called when the binding is being released.
        /// Clears the internal command and notifies reverse-binder subscribers with <see langword="null"/>.
        /// </summary>
        protected sealed override void OnUnbinding()
        {
            _command = null;
            _reverseAction?.Invoke(null);
            _reverseCommand?.Invoke(null);
        }

        /// <summary>
        /// Determines whether the Animator parameter may be set.
        /// Returns <see langword="true"/> when the <see cref="Animator"/>'s <see cref="UnityEngine.GameObject"/> is active in the hierarchy.
        /// </summary>
        /// <param name="value">The value that would be applied (not used in the default implementation).</param>
        protected virtual bool CanExecute(T? value) =>
            Target.gameObject.activeInHierarchy;
    }
}