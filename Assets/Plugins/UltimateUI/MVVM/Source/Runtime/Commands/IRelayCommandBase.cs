using System;

namespace UltimateUI.MVVM.Commands
{
    public interface IRelayCommandBase<out T> 
        where T : IRelayCommandBase<T>
    {
        public event Action<T> CanExecuteChanged;
        
        void NotifyCanExecuteChanged();
    }
}