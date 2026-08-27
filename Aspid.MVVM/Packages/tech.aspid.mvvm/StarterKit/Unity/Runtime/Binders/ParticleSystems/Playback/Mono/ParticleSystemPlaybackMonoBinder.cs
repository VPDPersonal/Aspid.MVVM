using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{ParticleSystem}"/> that hands the ViewModel one playback
    /// operation on a <see cref="ParticleSystem"/> — play, stop, pause or clear.
    /// </summary>
    /// <remarks>
    /// Only <see cref="BindMode.OneWayToSource"/> is supported: this is a binder the ViewModel calls, not one that
    /// receives values. Once bound, it exposes the operation either as a plain <see cref="Action"/> or as an
    /// <see cref="IRelayCommand"/> whose <see cref="IRelayCommand.CanExecute()"/> mirrors <see cref="CanExecute"/>.
    /// </remarks>
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    public abstract class ParticleSystemPlaybackMonoBinder : ComponentMonoBinder<ParticleSystem>,
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
        private Action<Action> _reverseAction;
        private Action<IRelayCommand> _reverseCommand;

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
        /// Performs the operation this binder represents on <paramref name="particleSystem"/>.
        /// </summary>
        /// <param name="particleSystem">The system to act on; never <see langword="null"/> when this is called.</param>
        protected abstract void Perform(ParticleSystem particleSystem);

        /// <summary>
        /// Determines whether the operation may run.
        /// Returns <see langword="true"/> when the system exists and its GameObject is active in the hierarchy.
        /// </summary>
        /// <remarks>
        /// The system's playback state isn't checked — stopping an idle system or clearing an empty one is harmless.
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
