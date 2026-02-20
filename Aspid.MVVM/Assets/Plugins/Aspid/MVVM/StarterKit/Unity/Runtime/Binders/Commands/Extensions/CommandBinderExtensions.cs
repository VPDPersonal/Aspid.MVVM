using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class CommandBinderExtensions
    {
        public static void UpdateCommand(ref IRelayCommand command, IRelayCommand value, in Action<IRelayCommand> onCanExecuteChanged = null)
        {
            if (command == value) return;
            
            if (command is not null && onCanExecuteChanged is not null) 
                command.CanExecuteChanged -= onCanExecuteChanged;
            
            command = value;

            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }
        
        public static void UpdateCommand<T>(ref IRelayCommand<T> command,
            IRelayCommand<T> value,
            in Action<IRelayCommand<T>> onCanExecuteChanged = null)
        {
            if (command == value) return;
            
            if (command is not null && onCanExecuteChanged is not null) 
                command.CanExecuteChanged -= onCanExecuteChanged;
            
            command = value;
            
            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }
        
        public static void UpdateCommand<T1, T2>(
            ref IRelayCommand<T1, T2> command,
            IRelayCommand<T1, T2> value, 
            in Action<IRelayCommand<T1, T2>> onCanExecuteChanged = null)
        {
            if (command == value) return;
            
            if (command is not null && onCanExecuteChanged is not null) 
                command.CanExecuteChanged -= onCanExecuteChanged;
            
            command = value;
            
            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }
        
        public static void UpdateCommand<T1, T2, T3>(
            ref IRelayCommand<T1, T2, T3> command,
            IRelayCommand<T1, T2, T3> value, 
            in Action<IRelayCommand<T1, T2, T3>> onCanExecuteChanged = null)
        {
            if (command == value) return;
            
            if (command is not null && onCanExecuteChanged is not null) 
                command.CanExecuteChanged -= onCanExecuteChanged;
            
            command = value;
            
            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }
        
        public static void UpdateCommand<T1, T2, T3, T4>(
            ref IRelayCommand<T1, T2, T3, T4> command,
            IRelayCommand<T1, T2, T3, T4> value, 
            in Action<IRelayCommand<T1, T2, T3, T4>> onCanExecuteChanged = null)
        {
            if (command == value) return;
            
            if (command is not null && onCanExecuteChanged is not null) 
                command.CanExecuteChanged -= onCanExecuteChanged;
            
            command = value;
            
            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }
    }
}