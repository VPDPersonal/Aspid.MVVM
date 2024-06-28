// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Collections
{
    public interface IObservableCollection<out T>
    {
        public event NotifyCollectionChangedEventHandler<T> CollectionChanged;
    }
}