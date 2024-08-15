#nullable disable
using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public interface IReverseBinder<out T> : IBinder
    {
        public event Action<T> ValueChanged;
        
        bool IBinder.IsReverseEnabled => true;
    }
}