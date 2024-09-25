using System.Collections.Generic;

namespace Aspid.Collections.Observable
{
    public interface IReadOnlyObservableCollection<out T> : IObservableCollection<T>, IReadOnlyCollection<T> { }
}