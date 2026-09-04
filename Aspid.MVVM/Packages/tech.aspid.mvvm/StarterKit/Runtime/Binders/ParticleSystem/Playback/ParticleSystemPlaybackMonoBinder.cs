using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="ComponentMonoBinder{TComponent}"/> that hands the ViewModel one playback operation on a
    /// <see cref="ParticleSystem"/> as an <see cref="Action"/> or an <see cref="IRelayCommand"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWayToSource)]
    public abstract class ParticleSystemPlaybackMonoBinder : ComponentMonoBinder<ParticleSystem>,
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

        /// <summary>
        /// Performs the operation on <paramref name="particleSystem"/>.
        /// </summary>
        /// <param name="particleSystem">The system to act on.</param>
        protected abstract void Perform(ParticleSystem particleSystem);

        /// <summary>
        /// Whether the operation may run: the system exists and is active in the hierarchy.
        /// </summary>
        /// <returns><see langword="true"/> when the operation may run.</returns>
        protected virtual bool CanExecute() =>
            CachedComponent && CachedComponent.gameObject.activeInHierarchy;

        private void Execute()
        {
            if (CanExecute())
                Perform(CachedComponent);
        }
    }
}
