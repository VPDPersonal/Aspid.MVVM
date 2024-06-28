using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Commands
{
    public sealed class RelayCommand
    {
        public event Action<RelayCommand>? CanExecuteChanged;
        
        private readonly Action _execute;

        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
        public RelayCommand(Action execute, Func<bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        public bool CanExecute() =>
            _canExecute?.Invoke() ?? true;

        public void Execute()
        {
            if (CanExecute()) 
                _execute.Invoke();
        }
        
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }

    public sealed class RelayCommand<T>
    {
        public event Action<RelayCommand<T>>? CanExecuteChanged;
        
        private readonly Action<T?> _execute;
        private readonly Predicate<T?>? _canExecute;

        public RelayCommand(Action<T?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public RelayCommand(Action<T?> execute, Predicate<T?> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        public bool CanExecute(T? parameter) =>
            _canExecute?.Invoke(parameter) ?? true;
        
        
        public void Execute(T? parameter)
        {
            if (CanExecute(parameter)) 
                _execute.Invoke(parameter);
        }
        
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
}