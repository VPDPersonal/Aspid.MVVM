#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="TargetBinder{TTarget}"/> that hands the ViewModel one operation on an
    /// <see cref="Animator"/> trigger as an <see cref="Action"/> or an <see cref="IRelayCommand"/>.
    /// </summary>
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public abstract class AnimatorTriggerBinder : TargetBinder<Animator>,
        IReverseBinder<Action?>,
        IReverseBinder<IRelayCommand?>
    {
        private IRelayCommand? _command;
        private AnimatorParameterProbe _probe;
        private Action<Action?>? _reverseAction;
        private Action<IRelayCommand?>? _reverseCommand;

        [field: Tooltip("Trigger parameter to fire.")]
        [field: SerializeField] protected string TriggerName { get; private set; } = string.Empty;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected AnimatorTriggerBinder() { }

        /// <param name="target">The animator to bind.</param>
        /// <param name="triggerName">The trigger parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="triggerName"/> is <see langword="null"/>.
        /// </exception>
        protected AnimatorTriggerBinder(Animator target, string triggerName)
            : base(target, BindMode.OneWayToSource)
        {
            TriggerName = triggerName ?? throw new ArgumentNullException(nameof(triggerName));
        }

        event Action<Action?>? IReverseBinder<Action?>.ValueChanged
        {
            add => _reverseAction += value;
            remove => _reverseAction -= value;
        }

        event Action<IRelayCommand?>? IReverseBinder<IRelayCommand?>.ValueChanged
        {
            add => _reverseCommand += value;
            remove => _reverseCommand -= value;
        }

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
            Target && Target.gameObject.activeInHierarchy &&
            _probe.IsUsable(Target, TriggerName, AnimatorControllerParameterType.Trigger, this);

        private void Run()
        {
            if (CanExecute())
                Apply(TriggerName);
        }
    }
}
