using UnityEngine.UI;
using UltimateUI.MVVM.Commands;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Commands
{
    public abstract class ButtonCommandProviderBase<TRelayCommand> : Binding, ITargetBinding<TRelayCommand>
        where TRelayCommand : class, IRelayCommandBase<TRelayCommand>
    {
        private readonly Button _button;
        private readonly bool _isBindInteractable;
        
        protected TRelayCommand Command { get; private set; }

        protected ButtonCommandProviderBase(PropertyPath path, Button button, bool isBindInteractable = true)
            : base(path)
        {
            _button = button;
            _isBindInteractable = isBindInteractable;
        }
        
        protected ButtonCommandProviderBase(string path, Button button, bool isBindInteractable = true)
            : base(path)
        {
            _button = button;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(TRelayCommand command)
        {
            ReleaseCommand();
            Command = command;
            
            SubscribeCommand();
            OnCanExecuteChanged(command);
        }
        
        protected override void OnBound() =>
            _button.onClick.AddListener(Execute);
        
        protected override void OnUnbound()
        {
            _button.onClick.RemoveListener(Execute);
            ReleaseCommand();
        }

        protected abstract void Execute();
        
        protected abstract bool CanExecute(TRelayCommand command);

        private void SubscribeCommand() =>
            Command.CanExecuteChanged += OnCanExecuteChanged;
        
        private void UnsubscribeCommand() =>
            Command.CanExecuteChanged -= OnCanExecuteChanged;
        
        private void ReleaseCommand()
        {
            if (Command != null) UnsubscribeCommand();
            Command = null;
        }
        
        private void OnCanExecuteChanged(TRelayCommand command)
        {
            if (!_isBindInteractable) return;
            _button.interactable = CanExecute(command);
        }
    }
    
    public sealed class ButtonCommandProvider : ButtonCommandProviderBase<IRelayCommand>
    {
        public ButtonCommandProvider(PropertyPath path, Button button, bool isBindInteractable = true) 
            : base(path, button, isBindInteractable) { }
        
        public ButtonCommandProvider(string path, Button button, bool isBindInteractable = true) 
            : base(path, button, isBindInteractable) { }

        protected override void Execute() =>
            Command?.Execute();

        protected override bool CanExecute(IRelayCommand command) =>
            Command?.CanExecute() ?? false;
    }
    
    public sealed class ButtonCommandProvider<T1> : ButtonCommandProviderBase<IRelayCommand<T1>>
    {
        public T1 Parameter { get; set; }
        
        public ButtonCommandProvider(PropertyPath path, Button button,
            T1 parameter = default, bool isBindInteractable = true)
            : base(path, button, isBindInteractable)
        {
            Parameter = parameter;
        }

        public ButtonCommandProvider(string path, Button button,
            T1 parameter = default, bool isBindInteractable = true)
            : base(path, button, isBindInteractable)
        {
            Parameter = parameter;
        }

        protected override void Execute() =>
            Command?.Execute(Parameter);

        protected override bool CanExecute(IRelayCommand<T1> command) =>
            command.CanExecute(Parameter);
    }
    
    public sealed class ButtonCommandProvider<T1, T2> : ButtonCommandProviderBase<IRelayCommand<T1, T2>>
    {
        private IRelayCommand<T1, T2> _command;

        public T1 Parameter1 { get; set; }
        
        public T2 Parameter2 { get; set; }
        
        public ButtonCommandProvider(PropertyPath path, Button button, 
            T1 parameter1 = default, T2 parameter2 = default, bool isBindInteractable = true) 
            : base(path, button, isBindInteractable)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
        }

        public ButtonCommandProvider(string path, Button button, 
            T1 parameter1 = default, T2 parameter2 = default, bool isBindInteractable = true) 
            : base(path, button, isBindInteractable)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
        }

        protected override void Execute() =>
            Command?.Execute(Parameter1, Parameter2);

        protected override bool CanExecute(IRelayCommand<T1, T2> command) =>
            command.CanExecute(Parameter1, Parameter2);
    }
    
    public sealed class ButtonCommandProvider<T1, T2, T3> : ButtonCommandProviderBase<IRelayCommand<T1, T2, T3>>
    {
        public T1 Parameter1 { get; set; }
        
        public T2 Parameter2 { get; set; }
        
        public T3 Parameter3 { get; set; }
        
        public ButtonCommandProvider(PropertyPath path, Button button, 
            T1 parameter1 = default, T2 parameter2 = default, T3 parameter3 = default, bool isBindInteractable = true) 
            : base(path, button, isBindInteractable)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
            Parameter3 = parameter3;
        }
        
        public ButtonCommandProvider(string path, Button button, 
            T1 parameter1 = default, T2 parameter2 = default, T3 parameter3 = default, bool isBindInteractable = true) 
            : base(path, button, isBindInteractable)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
            Parameter3 = parameter3;
        }

        protected override void Execute() =>
            Command?.Execute(Parameter1, Parameter2, Parameter3);

        protected override bool CanExecute(IRelayCommand<T1, T2, T3> command) =>
            command.CanExecute(Parameter1, Parameter2, Parameter3);
    }
    
    public sealed class ButtonCommandProvider<T1, T2, T3, T4> : ButtonCommandProviderBase<IRelayCommand<T1, T2, T3, T4>>
    {
        public T1 Parameter1 { get; set; }
        
        public T2 Parameter2 { get; set; }
        
        public T3 Parameter3 { get; set; }
        
        public T4 Parameter4 { get; set; }
        
        public ButtonCommandProvider(PropertyPath path, Button button, 
            T1 parameter1 = default, T2 parameter2 = default, 
            T3 parameter3 = default, T4 parameter4 = default, bool isBindInteractable = true) 
            : base(path, button, isBindInteractable)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
            Parameter3 = parameter3;
            Parameter4 = parameter4;
        }
        
        public ButtonCommandProvider(string path, Button button, 
            T1 parameter1 = default, T2 parameter2 = default, 
            T3 parameter3 = default, T4 parameter4 = default, bool isBindInteractable = true) 
            : base(path, button, isBindInteractable)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
            Parameter3 = parameter3;
            Parameter4 = parameter4;
        }

        protected override void Execute() =>
            Command?.Execute(Parameter1, Parameter2, Parameter3, Parameter4);

        protected override bool CanExecute(IRelayCommand<T1, T2, T3, T4> command) =>
            command.CanExecute(Parameter1, Parameter2, Parameter3, Parameter4);
    }
} 