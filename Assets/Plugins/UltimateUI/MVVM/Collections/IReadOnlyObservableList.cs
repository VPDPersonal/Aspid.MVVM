using System.Collections.Generic;

namespace UltimateUI.MVVM.Collections
{
    public interface IReadOnlyObservableList<out T> : IObservableCollection<T>, IReadOnlyList<T> { }
}