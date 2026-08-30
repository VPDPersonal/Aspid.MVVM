using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{AudioSource}"/> that hands the ViewModel one playback
    /// operation on an <see cref="AudioSource"/> — play, stop, pause or resume.
    /// </summary>
    /// <remarks>
    /// Only <see cref="BindMode.OneWayToSource"/> is supported: this is a binder the ViewModel calls, not one that
    /// receives values. When binding is established it exposes <see cref="Execute"/> either as a plain
    /// <see cref="Action"/> or as an <see cref="IRelayCommand"/> whose <see cref="IRelayCommand.CanExecute()"/> mirrors
    /// <see cref="CanExecute"/>.
    /// </remarks>
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    public abstract class AudioSourcePlaybackMonoBinder : ComponentMonoBinder<AudioSource>,
        IReverseBinder<Action>,
        IReverseBinder<IRelayCommand>
    {
        private IRelayCommand _command;
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

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneWayToSource;

        /// <summary>
        /// Notifies the bound command that <see cref="IRelayCommand.CanExecute()"/> may have changed.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnEnable()</c>.
        /// </remarks>
        protected virtual void OnEnable() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Notifies the bound command that <see cref="IRelayCommand.CanExecute()"/> may have changed.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnDisable()</c>.
        /// </remarks>
        protected virtual void OnDisable() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Performs the operation this binder represents on <paramref name="audioSource"/>.
        /// </summary>
        /// <param name="audioSource">The source to act on; never <see langword="null"/> when this is called.</param>
        protected abstract void Perform(AudioSource audioSource);

        /// <summary>
        /// Determines whether the operation may run.
        /// Returns <see langword="true"/> when the source exists and its GameObject is active in the hierarchy.
        /// </summary>
        /// <remarks>
        /// A clip is deliberately not required, since <c>Stop</c> and <c>Pause</c> are meaningful without one.
        /// </remarks>
        protected virtual bool CanExecute() =>
            CachedComponent && CachedComponent.gameObject.activeInHierarchy;

        /// <summary>
        /// Called when binding is established. Exposes the operation to the ViewModel.
        /// </summary>
        protected sealed override void OnBound()
        {
            if (_reverseCommand is not null)
            {
                _command = new RelayCommand(Execute, CanExecute);
                _reverseCommand.Invoke(_command);
            }
            else
            {
                _reverseAction?.Invoke(Execute);
            }
        }

        /// <summary>
        /// Called when the binding is being released. Clears the command and notifies subscribers with
        /// <see langword="null"/>.
        /// </summary>
        protected sealed override void OnUnbinding()
        {
            _command = null;
            _reverseAction?.Invoke(null);
            _reverseCommand?.Invoke(null);
        }

        private void Execute()
        {
            if (!CanExecute()) return;
            Perform(CachedComponent);
        }
    }
}
