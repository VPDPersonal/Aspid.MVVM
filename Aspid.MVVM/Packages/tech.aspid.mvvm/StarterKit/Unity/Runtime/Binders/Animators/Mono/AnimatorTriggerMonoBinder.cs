using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Animator}"/> that hands the ViewModel one operation on a trigger
    /// parameter — setting it or resetting it — as an <see cref="Action"/> or an <see cref="IRelayCommand"/>.
    /// </summary>
    /// <remarks>
    /// Only <see cref="BindMode.OneWayToSource"/> is supported. When binding is established, the binder
    /// exposes an internal <see cref="Animator.SetTrigger(string)"/> action to the ViewModel either as a plain <see cref="Action"/>
    /// or as an <see cref="IRelayCommand"/> whose <see cref="IRelayCommand.CanExecute()"/> mirrors <see cref="CanExecute()"/>.
    /// </remarks>
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    public abstract class AnimatorTriggerMonoBinder : ComponentMonoBinder<Animator>, 
        IReverseBinder<Action>,
        IReverseBinder<IRelayCommand>
    {
        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneWayToSource;

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
        
        private IRelayCommand _command;
        private AnimatorParameterProbe _probe;
        private Action<Action> _reverseAction;
        private Action<IRelayCommand> _reverseCommand;
        
        [field: SerializeField] 
        [field: Tooltip("The name of the trigger Animator parameter to fire.")]
        protected string TriggerName { get; private set; }

        /// <summary>
        /// Notifies the bound command that <see cref="IRelayCommand.CanExecute()"/> may have changed.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnEnable()</c> to preserve
        /// the command notification behavior.
        /// </remarks>
        protected virtual void OnEnable() => 
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Notifies the bound command that <see cref="IRelayCommand.CanExecute()"/> may have changed.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnDisable()</c> to preserve
        /// the command notification behavior.
        /// </remarks>
        protected virtual void OnDisable() =>
            _command?.NotifyCanExecuteChanged();

        private void Run()
        {
            if (!CanExecute()) return;
            Apply(TriggerName);
        }

        /// <summary>
        /// Performs the operation this binder exposes on <paramref name="triggerName"/>.
        /// </summary>
        /// <param name="triggerName">The name of the trigger parameter, already checked to exist.</param>
        protected abstract void Apply(string triggerName);

        /// <summary>
        /// Called when binding is established.
        /// Exposes the operation to the ViewModel as an <see cref="IRelayCommand"/> or a plain <see cref="Action"/>.
        /// </summary>
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
        /// Determines whether the trigger may be fired.
        /// Returns <see langword="true"/> when the <see cref="Animator"/>'s <see cref="UnityEngine.GameObject"/> is
        /// active in the hierarchy and <see cref="TriggerName"/> names a trigger its controller actually has.
        /// </summary>
        /// <remarks>
        /// The activity check comes first because it is the cheaper of the two and because a binder on an inactive
        /// object has nothing to say about a trigger it is not going to set.
        /// </remarks>
        protected virtual bool CanExecute() =>
            CachedComponent && CachedComponent.gameObject.activeInHierarchy &&
            _probe.IsUsable(CachedComponent, TriggerName, AnimatorControllerParameterType.Trigger, this);
    }
}