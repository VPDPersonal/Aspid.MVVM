using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Commands/Binder – Command")]
    [AddBinderContextMenu(typeof(Component), serializePropertyNames: "m_Calls", Path = "Add General Binder/Commands/Binder – Command")]
    public partial class MonoCommandBinder : MonoBinder, IBinder<IRelayCommand>
    {
        private IRelayCommand _command;

        protected IRelayCommand Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        [BinderLog]
        public void SetValue(IRelayCommand value)
        {
            Command = value;
            OnSetValue(value);
        }

        protected override void OnUnbound() =>
            Command = null;

        protected virtual void OnSetValue(IRelayCommand value) { }
        
        public bool CanExecute() => 
            Command?.CanExecute() ?? false;

        public void Execute() =>
            Command?.Execute();

        protected virtual void OnCanExecuteChanged(IRelayCommand command) { }
    }
    
    public abstract partial class MonoCommandBinder<T> : MonoBinder, IBinder<IRelayCommand<T>>
    {
        private IRelayCommand<T> _command;

        protected IRelayCommand<T> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T> value)
        {
            Command = value;
            OnSetValue(value);
        }
        
        protected override void OnUnbound() =>
            Command = null;

        protected virtual void OnSetValue(IRelayCommand<T> value) { }
        
        public bool CanExecute(T param1) =>
            Command?.CanExecute(param1) ?? false;

        public void Execute(T param1) => 
            Command?.Execute(param1);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T> command) { }
    }
    
    public abstract partial class MonoCommandBinder<T1, T2> : MonoBinder, IBinder<IRelayCommand<T1, T2>>
    {
        private IRelayCommand<T1, T2> _command;

        protected IRelayCommand<T1, T2> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value)
        {
            Command = value;
            OnSetValue(value);
        }
        
        protected override void OnUnbound() =>
            Command = null;
        
        protected virtual void OnSetValue(IRelayCommand<T1, T2> value) { }

        public bool CanExecute(T1 param1, T2 param2) => 
            Command?.CanExecute(param1, param2) ?? false;

        public void Execute(T1 param1, T2 param2) => 
            Command?.Execute(param1, param2);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2> command) { }
    }
    
    public abstract partial class MonoCommandBinder<T1, T2, T3> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3>>
    {
        private IRelayCommand<T1, T2, T3> _command;

        protected IRelayCommand<T1, T2, T3> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value)
        {
            Command = value;
            OnSetValue(value);
        }
        
        protected override void OnUnbound() =>
            Command = null;

        protected virtual void OnSetValue(IRelayCommand<T1, T2, T3> value) { }
        
        public bool CanExecute(T1 param1, T2 param2, T3 param3) => 
            Command?.CanExecute(param1, param2, param3) ?? false;

        public void Execute(T1 param1, T2 param2, T3 param3) => 
            Command?.Execute(param1, param2, param3);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command) { }
    }
    
    public abstract partial class MonoCommandBinder<T1, T2, T3, T4> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        private IRelayCommand<T1, T2, T3, T4> _command;

        protected IRelayCommand<T1, T2, T3, T4> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value)
        {
            Command = value;
            OnSetValue(value);
        }
        
        protected override void OnUnbound() =>
            Command = null;

        protected virtual void OnSetValue(IRelayCommand<T1, T2, T3, T4> value) { }
        
        public bool CanExecute(T1 param1, T2 param2, T3 param3, T4 param4) => 
            Command?.CanExecute(param1, param2, param3, param4) ?? false;

        public void Execute(T1 param1, T2 param2, T3 param3, T4 param4) => 
            Command?.Execute(param1, param2, param3, param4);

        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command) { }
    }
}