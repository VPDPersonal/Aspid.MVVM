using System;

namespace Aspid.UI.MVVM.Commands
{
    public sealed class RelayCommand : IRelayCommand
    {
        public event Action<IRelayCommand>? CanExecuteChanged;
        
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

    public sealed class RelayCommand<T> : IRelayCommand<T>
    {
        public event Action<IRelayCommand<T>>? CanExecuteChanged;
        
        private readonly Action<T?> _execute;
        private readonly Func<T?, bool>? _canExecute;

        public RelayCommand(Action<T?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        public bool CanExecute(T? param) =>
            _canExecute?.Invoke(param) ?? true;
        
        public void Execute(T? param)
        {
            if (CanExecute(param)) 
                _execute.Invoke(param);
        }
        
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    public sealed class RelayCommand<T1, T2> : IRelayCommand<T1, T2>
    {
        public event Action<IRelayCommand<T1, T2>>? CanExecuteChanged;
        
        private readonly Action<T1?, T2?> _execute;
        private readonly Func<T1?, T2?, bool>? _canExecute;

        public RelayCommand(Action<T1?, T2?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public RelayCommand(Action<T1?, T2?> execute, Func<T1?, T2?, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        public bool CanExecute(T1? param1, T2? param2) =>
            _canExecute?.Invoke(param1, param2) ?? true;
        
        public void Execute(T1? param1, T2? param2)
        {
            if (CanExecute(param1, param2)) 
                _execute.Invoke(param1, param2);
        }
        
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    public sealed class RelayCommand<T1, T2, T3> : IRelayCommand<T1, T2, T3>
    {
        public event Action<IRelayCommand<T1, T2, T3>>? CanExecuteChanged;
        
        private readonly Action<T1?, T2?, T3?> _execute;
        private readonly Func<T1?, T2?, T3?, bool>? _canExecute;

        public RelayCommand(Action<T1?, T2?, T3?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public RelayCommand(Action<T1?, T2?, T3?> execute, Func<T1?, T2?, T3?, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        public bool CanExecute(T1? param1, T2? param2, T3? param3) =>
            _canExecute?.Invoke(param1, param2, param3) ?? true;
        
        public void Execute(T1? param1, T2? param2, T3? param3)
        {
            if (CanExecute(param1, param2, param3)) 
                _execute.Invoke(param1, param2, param3);
        }
        
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
    
    public sealed class RelayCommand<T1, T2, T3, T4> : IRelayCommand<T1, T2, T3, T4>
    {
        public event Action<IRelayCommand<T1, T2, T3, T4>>? CanExecuteChanged;
        
        private readonly Action<T1?, T2?, T3?, T4?> _execute;
        private readonly Func<T1?, T2?, T3?, T4?, bool>? _canExecute;

        public RelayCommand(Action<T1?, T2?, T3?, T4?> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public RelayCommand(Action<T1?, T2?, T3?, T4?> execute, Func<T1?, T2?, T3?, T4?, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        
        public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4) =>
            _canExecute?.Invoke(param1, param2, param3, param4) ?? true;
        
        public void Execute(T1? param1, T2? param2, T3? param3, T4? param4)
        {
            if (CanExecute(param1, param2, param3, param4)) 
                _execute.Invoke(param1, param2, param3, param4);
        }
        
        public void NotifyCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this);
    }
}