using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Collections
{
    public interface IReadOnlyObservableCollection<T> : IObservableCollection<T>, IReadOnlyCollection<T> { }
}