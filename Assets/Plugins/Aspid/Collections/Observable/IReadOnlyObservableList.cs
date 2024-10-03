using System.Collections.Generic;

namespace Aspid.Collections.Observable
{
    public interface IReadOnlyObservableList<out T> : IReadOnlyObservableCollection<T>, IReadOnlyList<T> { }
}