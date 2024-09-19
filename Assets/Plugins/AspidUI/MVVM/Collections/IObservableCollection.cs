namespace AspidUI.MVVM.Collections
{
    public interface IObservableCollection<out T>
    {
        public event NotifyCollectionChangedEventHandler<T> CollectionChanged;
    }
}