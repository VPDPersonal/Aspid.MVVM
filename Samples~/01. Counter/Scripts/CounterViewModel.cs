using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Counter
{
    [ViewModel]
    [Serializable]
    public sealed partial class CounterViewModel
    {
        [Bind] private int _count;

        [RelayCommand]
        private void Increment() => Count++; 
        
        [RelayCommand]
        private void Decrement() => Count--;

        [RelayCommand]
        private void Reset() => Count = 0;
    }
}