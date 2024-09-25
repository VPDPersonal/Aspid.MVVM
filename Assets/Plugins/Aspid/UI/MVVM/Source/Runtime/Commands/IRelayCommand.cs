using System;

namespace Aspid.UI.MVVM.Commands
{
    public interface IRelayCommand
    {
        public event Action<IRelayCommand> CanExecuteChanged;
        
        public bool CanExecute();

        public void Execute();
        
        void NotifyCanExecuteChanged();
    }
    
    public interface IRelayCommand<in T>
    {
        public event Action<IRelayCommand<T>> CanExecuteChanged;
        
        public bool CanExecute(T? param);

        public void Execute(T param);
        
        void NotifyCanExecuteChanged();
    }
    
    public interface IRelayCommand<in T1, in T2>
    {
        public event Action<IRelayCommand<T1, T2>> CanExecuteChanged;
        
        public bool CanExecute(T1? param1, T2? param2);

        public void Execute(T1 param1, T2 param2);
        
        void NotifyCanExecuteChanged();
    }
    
    public interface IRelayCommand<in T1, in T2, in T3>
    {
        public event Action<IRelayCommand<T1, T2, T3>> CanExecuteChanged;
        
        public bool CanExecute(T1? param1, T2? param2, T3? param3);

        public void Execute(T1 param1, T2 param2, T3 param3);
        
        void NotifyCanExecuteChanged();
    }
    
    public interface IRelayCommand<in T1, in T2, in T3, in T4>
    {
        public event Action<IRelayCommand<T1, T2, T3, T4>> CanExecuteChanged;
        
        public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4);

        public void Execute(T1 param1, T2 param2, T3 param3, T4 param4);
        
        void NotifyCanExecuteChanged();
    }
}