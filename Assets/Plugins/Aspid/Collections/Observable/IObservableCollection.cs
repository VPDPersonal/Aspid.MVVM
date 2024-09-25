namespace Aspid.Collections.Observable
{
    public interface IObservableCollection<out T>
    {
        public event NotifyCollectionChangedEventHandler<T> CollectionChanged;
    }
}