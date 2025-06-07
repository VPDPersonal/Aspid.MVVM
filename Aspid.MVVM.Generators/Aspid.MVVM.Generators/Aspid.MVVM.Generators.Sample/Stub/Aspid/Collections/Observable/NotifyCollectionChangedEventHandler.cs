namespace Aspid.Collections.Observable
{
    public delegate void NotifyCollectionChangedEventHandler<in T>(INotifyCollectionChangedEventArgs<T> e);
}