using System;

namespace Aspid.UI.MVVM
{
    public interface IReverseBinder<out T> : IBinder
    {
        public event Action<T> ValueChanged;
        
        bool IBinder.IsReverseEnabled => true;
    }
}