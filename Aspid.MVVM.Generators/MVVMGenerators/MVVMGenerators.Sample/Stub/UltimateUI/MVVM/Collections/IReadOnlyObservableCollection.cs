using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Collections
{
    public interface IReadOnlyObservableCollection<out T> : IObservableCollection<T>, IReadOnlyCollection<T> { }
}