using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Counter
{
    // [ViewModel] makes the source generator implement IViewModel for this partial class.
    // [Serializable] lets ViewInitializer create and edit the instance right in the Inspector.
    [ViewModel]
    [Serializable]
    public sealed partial class CounterViewModel
    {
        // Generates the Count property. The View binds to it by the field name.
        [Bind] private int _count;

        // Generates the IncrementCommand property of type IRelayCommand.
        [RelayCommand]
        private void Increment() => Count++;

        [RelayCommand]
        private void Decrement() => Count--;

        [RelayCommand]
        private void Reset() => Count = 0;
    }
}
