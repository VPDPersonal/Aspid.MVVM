#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Animator}"/> that fires a trigger parameter on a Unity <see cref="Animator"/>
    /// when the bound ViewModel command or action is invoked.
    /// </summary>
    /// <remarks>
    /// Only <see cref="BindMode.OneWayToSource"/> is supported. When binding is established, the binder
    /// exposes an internal <c>SetTrigger</c> action to the ViewModel either as a plain <see cref="Action"/>
    /// or as an <see cref="IRelayCommand"/> whose <c>CanExecute</c> mirrors <see cref="CanExecute"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-Animator-1.1.0.xml" path="doc//member[@name='AnimatorSetTriggerBinder']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class AnimatorSetTriggerBinder : TargetBinder<Animator>,
        IReverseBinder<Action?>,
        IReverseBinder<IRelayCommand?>
    {
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

        private IRelayCommand? _command;
        private Action<Action?>? _reverseAction;
        private Action<IRelayCommand?>? _reverseCommand;

        [field: SerializeField]
        [field: Tooltip("The name of the trigger Animator parameter to fire.")]
        protected string TriggerName { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorSetTriggerBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Animator"/> whose trigger parameter is fired.</param>
        /// <param name="triggerName">The name of the trigger Animator parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="triggerName"/> is <see langword="null"/>.
        /// </exception>
        public AnimatorSetTriggerBinder(Animator target, string triggerName)
            : base(target, BindMode.OneWayToSource)
        {
            TriggerName = triggerName ?? throw new ArgumentNullException(nameof(triggerName));
        }

        /// <summary>
        /// Notifies the bound <see cref="IRelayCommand"/> that its <c>CanExecute</c> state may have changed.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
            _command?.NotifyCanExecuteChanged();

        private void SetTrigger()
        {
            if (!CanExecute()) return;
            Target.SetTrigger(TriggerName);
        }

        /// <summary>
        /// Called when binding is established.
        /// Exposes <c>SetTrigger</c> to the ViewModel as an <see cref="IRelayCommand"/> or a plain <see cref="Action"/>.
        /// </summary>
        protected sealed override void OnBound()
        {
            if (_reverseCommand is not null)
            {
                _command = new RelayCommand(SetTrigger, CanExecute);
                _reverseCommand.Invoke(_command);
            }
            else
            {
                _reverseAction?.Invoke(SetTrigger);
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
        /// Returns <see langword="true"/> when the <see cref="Animator"/>'s <see cref="UnityEngine.GameObject"/> is active in the hierarchy.
        /// </summary>
        protected virtual bool CanExecute() =>
            Target.gameObject.activeInHierarchy;
    }
}