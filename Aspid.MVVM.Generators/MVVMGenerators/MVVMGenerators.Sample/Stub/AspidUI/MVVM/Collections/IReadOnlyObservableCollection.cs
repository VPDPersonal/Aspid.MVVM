using System.Collections.Generic;

namespace AspidUI.MVVM.Collections
{
    public interface IReadOnlyObservableCollection<out T> : IObservableCollection<T>, IReadOnlyCollection<T> { }
}