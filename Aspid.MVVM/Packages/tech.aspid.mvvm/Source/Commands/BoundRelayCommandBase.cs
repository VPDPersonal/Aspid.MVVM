#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal abstract class BoundRelayCommandBase<TCommand> : IRelayCommand
        where TCommand : class
    {
        private Action<IRelayCommand>? _canExecuteChanged;

        protected TCommand Command { get; }

        protected Action<TCommand> SourceChangedHandler { get; }

        protected BoundRelayCommandBase(TCommand command)
        {
            SourceChangedHandler = OnSourceCanExecuteChanged;
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public event Action<IRelayCommand>? CanExecuteChanged
        {
            add
            {
                if (value is null) return;

                var previous = _canExecuteChanged;
                _canExecuteChanged += value;

                if (previous is not null) return;

                try
                {
                    SubscribeToSource();
                }
                catch
                {
                    _canExecuteChanged = previous;

                    try
                    {
                        UnsubscribeFromSource();
                    }
                    catch
                    {
                        // Do not replace the subscription failure with a cleanup failure.
                    }

                    throw;
                }
            }
            remove
            {
                if (value is null || _canExecuteChanged is null) return;

                var previous = _canExecuteChanged;
                _canExecuteChanged -= value;

                if (_canExecuteChanged is not null) return;

                try
                {
                    UnsubscribeFromSource();
                }
                catch
                {
                    _canExecuteChanged = previous;
                    throw;
                }
            }
        }

        public abstract void Execute();

        public abstract bool CanExecute();

        public void NotifyCanExecuteChanged()
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                _canExecuteChanged?.Invoke(this);
            }
        }

        protected abstract void SubscribeToSource();

        protected abstract void UnsubscribeFromSource();

        private void OnSourceCanExecuteChanged(TCommand _) =>
            NotifyCanExecuteChanged();
    }
}
