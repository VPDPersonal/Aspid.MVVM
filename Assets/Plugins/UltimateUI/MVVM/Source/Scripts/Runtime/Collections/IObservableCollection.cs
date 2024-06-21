#nullable enable

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Collections
{
    public interface IObservableCollection<T>
    {
        public event NotifyCollectionChangedEventHandler<T>? CollectionChanged;
    }
}