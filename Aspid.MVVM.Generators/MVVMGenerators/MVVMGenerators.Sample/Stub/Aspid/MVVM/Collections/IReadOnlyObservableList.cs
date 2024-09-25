using System.Collections.Generic;

namespace AspidUI.MVVM.Collections
{
    public interface IReadOnlyObservableList<out T> : IObservableCollection<T>, IReadOnlyList<T> { }
}