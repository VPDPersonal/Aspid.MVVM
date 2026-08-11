#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{Animator}"/> that hands the ViewModel one operation on a trigger parameter —
    /// setting it or resetting it — as an <see cref="Action"/> or an <see cref="IRelayCommand"/>.
    /// </summary>
    /// <remarks>
    /// Only <see cref="BindMode.OneWayToSource"/> is supported. When binding is established, the binder
    /// exposes an internal <see cref="Animator.SetTrigger(string)"/> action to the ViewModel either as a plain <see cref="Action"/>
    /// or as an <see cref="IRelayCommand"/> whose <see cref="IRelayCommand.CanExecute()"/> mirrors <see cref="CanExecute()"/>.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public abstract class AnimatorTriggerBinder : TargetBinder<Animator>,
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
        private AnimatorParameterProbe _probe;
        private Action<Action?>? _reverseAction;
        private Action<IRelayCommand?>? _reverseCommand;

        [field: SerializeField]
        [field: Tooltip("The name of the trigger Animator parameter to fire.")]
        protected string TriggerName { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorTriggerBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Animator"/> whose trigger parameter is fired.</param>
        /// <param name="triggerName">The name of the trigger Animator parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="triggerName"/> is <see langword="null"/>.
        /// </exception>
        protected AnimatorTriggerBinder(Animator target, string triggerName)
            : base(target, BindMode.OneWayToSource)
        {
            TriggerName = triggerName ?? throw new ArgumentNullException(nameof(triggerName));
        }

        /// <summary>
        /// Notifies the bound <see cref="IRelayCommand"/> that its <see cref="IRelayCommand.CanExecute()"/> state may have changed.
        /// </summary>
        public void NotifyCanExecuteChanged() =>
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
        /// Exposes <see cref="Animator.SetTrigger(string)"/> to the ViewModel as an <see cref="IRelayCommand"/> or a plain <see cref="Action"/>.
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
            Target && Target.gameObject.activeInHierarchy &&
            _probe.IsUsable(Target, TriggerName, AnimatorControllerParameterType.Trigger, this);
    }

    /// <summary>
    /// Concrete <see cref="AnimatorTriggerBinder"/> that sets the trigger parameter.
    /// </summary>
    /// <remarks>
    /// The original binder of this family: the ViewModel says when the animation starts.
    /// </remarks>
    /// <include file="XmlExampleDoc-Animator-1.1.0.xml" path="doc//member[@name='AnimatorSetTriggerBinder']/*" />
    [Serializable]
    public class AnimatorSetTriggerBinder : AnimatorTriggerBinder
    {
        /// <inheritdoc/>
        public AnimatorSetTriggerBinder(Animator target, string triggerName)
            : base(target, triggerName) { }

        /// <inheritdoc/>
        protected override void Apply(string triggerName) =>
            Target.SetTrigger(triggerName);
    }

    /// <summary>
    /// Concrete <see cref="AnimatorTriggerBinder"/> that resets the trigger parameter.
    /// </summary>
    /// <remarks>
    /// A trigger that was set and never consumed stays armed, and the animation fires the moment its state becomes
    /// reachable — often seconds later, in a state nobody connected to it. Resetting is how that is undone, and nothing
    /// in the package could do it.
    /// </remarks>
    [Serializable]
    public class AnimatorResetTriggerBinder : AnimatorTriggerBinder
    {
        /// <inheritdoc/>
        public AnimatorResetTriggerBinder(Animator target, string triggerName)
            : base(target, triggerName) { }

        /// <inheritdoc/>
        protected override void Apply(string triggerName) =>
            Target.ResetTrigger(triggerName);
    }
}
