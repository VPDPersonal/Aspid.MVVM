using System.Collections.Generic;

namespace UltimateUI.MVVM.Collections
{
    public interface IReadOnlyObservableCollection<out T> : IObservableCollection<T>, IReadOnlyCollection<T> { }
}