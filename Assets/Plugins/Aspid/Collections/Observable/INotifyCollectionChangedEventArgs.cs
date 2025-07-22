namespace Aspid.Collections.Observable;

public interface INotifyCollectionChangedEventArgs<out T>
{
    public NotifyCollectionChangedAction Action { get; }

    public bool IsSingleItem { get; }
        
    public T? OldItem { get; }
        
    public T? NewItem { get; }

    public IReadOnlyList<T>? OldItems { get; }
        
    public IReadOnlyList<T>? NewItems { get; }

    public int OldStartingIndex { get; }
        
    public int NewStartingIndex { get; }
}