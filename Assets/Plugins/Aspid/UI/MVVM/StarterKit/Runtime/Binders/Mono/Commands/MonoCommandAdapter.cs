using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands
{
    public partial class MonoCommandAdapter : MonoBinder, IBinder<IRelayCommand>
    {
        private IRelayCommand _command;

        protected IRelayCommand Command
        {
            get => _command;
            private set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }

        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            Command = value;
        
        public bool CanExecute() => Command.CanExecute();

        public void InvokeCommand() => Command?.Execute();

        protected virtual void OnCanExecuteChanged(IRelayCommand command) { }
    }
    
    public abstract partial class MonoCommandAdapter<T> : MonoBinder, IBinder<IRelayCommand<T>>
    {
        private IRelayCommand<T> _command;

        protected IRelayCommand<T> Command
        {
            get => _command;
            private set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T> value) =>
            Command = value;
        
        public bool CanExecute(T param1) => Command.CanExecute(param1);

        public void InvokeCommand(T param1) => Command?.Execute(param1);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T> command) { }
    }
    
    public abstract partial class MonoCommandAdapter<T1, T2> : MonoBinder, IBinder<IRelayCommand<T1, T2>>
    {
        private IRelayCommand<T1, T2> _command;

        protected IRelayCommand<T1, T2> Command
        {
            get => _command;
            private set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value) =>
            Command = value;
        
        public bool CanExecute(T1 param1, T2 param2) => 
            Command.CanExecute(param1, param2);

        public void InvokeCommand(T1 param1, T2 param2) => 
            Command?.Execute(param1, param2);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2> command) { }
    }
    
    public abstract partial class MonoCommandAdapter<T1, T2, T3> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3>>
    {
        private IRelayCommand<T1, T2, T3> _command;

        protected IRelayCommand<T1, T2, T3> Command
        {
            get => _command;
            private set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value) =>
            Command = value;
        
        public bool CanExecute(T1 param1, T2 param2, T3 param3) => 
            Command.CanExecute(param1, param2, param3);

        public void InvokeCommand(T1 param1, T2 param2, T3 param3) => 
            Command?.Execute(param1, param2, param3);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command) { }
    }
    
    public abstract partial class MonoCommandAdapter<T1, T2, T3, T4> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        private IRelayCommand<T1, T2, T3, T4> _command;

        protected IRelayCommand<T1, T2, T3, T4> Command
        {
            get => _command;
            private set
            {
                if (_command == value) return;

                if (_command is not null)
                    _command.CanExecuteChanged -= OnCanExecuteChanged;
                
                _command = value;
                
                if (_command is not null)
                    _command.CanExecuteChanged += OnCanExecuteChanged;
            }
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value) =>
            Command = value;
        
        public bool CanExecute(T1 param1, T2 param2, T3 param3, T4 param4) => 
            Command.CanExecute(param1, param2, param3, param4);

        public void InvokeCommand(T1 param1, T2 param2, T3 param3, T4 param4) => 
            Command?.Execute(param1, param2, param3, param4);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command) { }
    }
}