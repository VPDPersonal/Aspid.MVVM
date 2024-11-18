using UnityEngine;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands.Adapters
{
    public class MonoCommandAdapter : MonoBehaviour
    {
        private IRelayCommand _command;

        public IRelayCommand Command
        {
            protected get => _command;
            set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }
        
        public bool CanExecute() => Command.CanExecute();

        public void InvokeCommand() => Command?.Execute();

        protected virtual void OnCanExecuteChanged(IRelayCommand command) { }
    }
    
    public abstract class MonoCommandAdapter<T> : MonoBehaviour
    {
        private IRelayCommand<T> _command;

        public IRelayCommand<T> Command
        {
            protected get => _command;
            set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }
        
        public bool CanExecute(T param1) => Command.CanExecute(param1);

        public void InvokeCommand(T param1) => Command?.Execute(param1);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T> command) { }
    }
    
    public abstract class MonoCommandAdapter<T1, T2> : MonoBehaviour
    {
        private IRelayCommand<T1, T2> _command;

        public IRelayCommand<T1, T2> Command
        {
            protected get => _command;
            set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }
        
        public bool CanExecute(T1 param1, T2 param2) => 
            Command.CanExecute(param1, param2);

        public void InvokeCommand(T1 param1, T2 param2) => 
            Command?.Execute(param1, param2);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2> command) { }
    }
    
    public abstract class MonoCommandAdapter<T1, T2, T3> : MonoBehaviour
    {
        private IRelayCommand<T1, T2, T3> _command;

        public IRelayCommand<T1, T2, T3> Command
        {
            protected get => _command;
            set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }
        
        public bool CanExecute(T1 param1, T2 param2, T3 param3) => 
            Command.CanExecute(param1, param2, param3);

        public void InvokeCommand(T1 param1, T2 param2, T3 param3) => 
            Command?.Execute(param1, param2, param3);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command) { }
    }
    
    public abstract class MonoCommandAdapter<T1, T2, T3, T4> : MonoBehaviour
    {
        private IRelayCommand<T1, T2, T3, T4> _command;

        public IRelayCommand<T1, T2, T3, T4> Command
        {
            protected get => _command;
            set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }
        
        public bool CanExecute(T1 param1, T2 param2, T3 param3, T4 param4) => 
            Command.CanExecute(param1, param2, param3, param4);

        public void InvokeCommand(T1 param1, T2 param2, T3 param3, T4 param4) => 
            Command?.Execute(param1, param2, param3, param4);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command) { }
    }
}