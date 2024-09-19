#nullable disable
using System;

namespace AspidUI.MVVM
{
    public interface IReverseBinder<out T> : IBinder
    {
        public event Action<T> ValueChanged;
        
        bool IBinder.IsReverseEnabled => true;
    }
}