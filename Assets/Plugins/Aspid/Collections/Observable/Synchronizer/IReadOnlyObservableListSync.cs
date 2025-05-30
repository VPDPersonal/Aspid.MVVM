namespace Aspid.Collections.Observable.Synchronizer
{
    public interface IReadOnlyObservableListSync<out T> : IReadOnlyObservableList<T>, IReadOnlyObservableCollectionSync<T> { }
}