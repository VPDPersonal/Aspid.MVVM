using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="ComponentMonoBinder{TComponent}"/> that hands the ViewModel one operation on an
    /// <see cref="Animator"/> trigger as an <see cref="Action"/> or an <see cref="IRelayCommand"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWayToSource)]
    public abstract class AnimatorTriggerMonoBinder : ComponentMonoBinder<Animator>,
        IReverseBinder<Action>,
        IReverseBinder<IRelayCommand>
    {
        [Tooltip("Trigger parameter to fire.")]
        [SerializeField] private string _triggerName;

        private IRelayCommand _command;
        private AnimatorParameterProbe _probe;
        private Action<Action> _reverseAction;
        private Action<IRelayCommand> _reverseCommand;

        event Action<Action> IReverseBinder<Action>.ValueChanged
        {
            add => _reverseAction += value;
            remove => _reverseAction -= value;
        }

        event Action<IRelayCommand> IReverseBinder<IRelayCommand>.ValueChanged
        {
            add => _reverseCommand += value;
            remove => _reverseCommand -= value;
        }

        /// <summary>
        /// The trigger parameter to fire.
        /// </summary>
        protected string TriggerName => _triggerName;

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneWayToSource;

        /// <summary>
        /// Refreshes the command's <see cref="IRelayCommand.CanExecute()"/>.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnEnable()</c>.
        /// </remarks>
        protected virtual void OnEnable() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Refreshes the command's <see cref="IRelayCommand.CanExecute()"/>.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnDisable()</c>.
        /// </remarks>
        protected virtual void OnDisable() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Notifies the command handed to the ViewModel that <see cref="IRelayCommand.CanExecute()"/> may have changed.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            _command?.NotifyCanExecuteChanged();

        /// <inheritdoc/>
        protected sealed override void OnBound()
        {
            if (_reverseCommand is not null)
            {
                _command = new RelayCommand(Run, CanExecute);
                _reverseCommand.Invoke(_command);
            }
            else
            {
                _reverseAction?.Invoke(Run);
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
        /// Performs the operation on the trigger named <paramref name="triggerName"/>.
        /// </summary>
        /// <param name="triggerName">The trigger, already checked to exist.</param>
        protected abstract void Apply(string triggerName);

        /// <summary>
        /// Whether the trigger may be fired: the animator is active and its controller has the trigger.
        /// </summary>
        /// <returns><see langword="true"/> when the trigger may be fired.</returns>
        protected virtual bool CanExecute() =>
            CachedComponent && CachedComponent.gameObject.activeInHierarchy &&
            _probe.IsUsable(CachedComponent, TriggerName, AnimatorControllerParameterType.Trigger, this);

        private void Run()
        {
            if (CanExecute())
                Apply(TriggerName);
        }
    }
}
