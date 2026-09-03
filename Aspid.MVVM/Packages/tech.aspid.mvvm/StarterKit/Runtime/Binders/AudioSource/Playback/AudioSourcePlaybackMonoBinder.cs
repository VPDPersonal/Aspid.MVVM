using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that exposes one playback operation on an
    /// <see cref="AudioSource"/> to the ViewModel as an <see cref="Action"/> or an <see cref="IRelayCommand"/>.
    /// </summary>
    /// <remarks>
    /// The command's <see cref="IRelayCommand.CanExecute()"/> mirrors <see cref="CanExecute"/>.
    /// </remarks>
    [BindModeOverride(BindMode.OneWayToSource)]
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
        /// When overriding, always call <c>base.OnEnable()</c>.
        /// </remarks>
        protected virtual void OnEnable() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Notifies the bound command that <see cref="IRelayCommand.CanExecute()"/> may have changed.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnDisable()</c>.
        /// </remarks>
        protected virtual void OnDisable() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Performs the operation on <paramref name="audioSource"/>.
        /// </summary>
        /// <param name="audioSource">The source to act on; never <see langword="null"/>.</param>
        protected abstract void Perform(AudioSource audioSource);

        /// <summary>
        /// Determines whether the operation may run: the source exists and is active in the hierarchy.
        /// </summary>
        /// <remarks>
        /// A clip is not required, since Stop and Pause are meaningful without one.
        /// </remarks>
        /// <returns><see langword="true"/> when the operation may run; otherwise, <see langword="false"/>.</returns>
        protected virtual bool CanExecute() =>
            CachedComponent && CachedComponent.gameObject.activeInHierarchy;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
