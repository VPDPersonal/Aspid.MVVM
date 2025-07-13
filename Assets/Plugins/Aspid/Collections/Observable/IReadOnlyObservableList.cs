namespace Aspid.Collections.Observable;

public interface IReadOnlyObservableList<out T> : IObservableCollection<T>, IReadOnlyList<T> { }