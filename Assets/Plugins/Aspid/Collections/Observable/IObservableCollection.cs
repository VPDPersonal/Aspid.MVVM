namespace Aspid.Collections.Observable;

public interface IObservableCollection<out T> : IReadOnlyCollection<T>
{
    public event NotifyCollectionChangedEventHandler<T>? CollectionChanged;
        
    public object SyncRoot { get; }
}