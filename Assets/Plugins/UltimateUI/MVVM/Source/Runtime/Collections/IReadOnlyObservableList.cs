using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Collections
{
    public interface IReadOnlyObservableList<out T> : IObservableCollection<T>, IReadOnlyList<T> { }
}