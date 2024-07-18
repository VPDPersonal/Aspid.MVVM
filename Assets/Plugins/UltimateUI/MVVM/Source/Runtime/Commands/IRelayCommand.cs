using System;

namespace UltimateUI.MVVM.Commands
{
    public interface IRelayCommand : IRelayCommandBase<IRelayCommand>
    {
        public bool CanExecute();

        public void Execute();
    }
    
    public interface IRelayCommand<in T> : IRelayCommandBase<IRelayCommand<T>>
    {
        public bool CanExecute(T? param);

        public void Execute(T param);
    }
    
    public interface IRelayCommand<in T1, in T2> : IRelayCommandBase<IRelayCommand<T1, T2>>
    {
        public bool CanExecute(T1? param1, T2? param2);

        public void Execute(T1 param1, T2 param2);
    }
    
    public interface IRelayCommand<in T1, in T2, in T3> : IRelayCommandBase<IRelayCommand<T1, T2, T3>>
    {
        public bool CanExecute(T1? param1, T2? param2, T3? param3);

        public void Execute(T1 param1, T2 param2, T3 param3);
    }
    
    public interface IRelayCommand<in T1, in T2, in T3, in T4> : IRelayCommandBase<IRelayCommand<T1, T2, T3, T4>>
    {
        public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4);

        public void Execute(T1 param1, T2 param2, T3 param3, T4 param4);
    }
}