namespace Aspid.Collections.Observable.Synchronizer;

internal sealed class ObservableDictionarySync<TKey, TFrom, TTo> : ObservableDictionary<TKey, TTo>, IReadOnlyObservableDictionarySync<TKey, TTo>
    where TKey : notnull
{
    private readonly bool _isDisposable;
    private readonly Action<TTo>? _remove;
    private readonly Func<TFrom, TTo> _converter;
    private readonly IReadOnlyObservableDictionary<TKey, TFrom> _fromDictionary;

    public ObservableDictionarySync(
        IReadOnlyObservableDictionary<TKey, TFrom> fromDictionary, 
        Func<TFrom, TTo> converter,
        Action<TTo>? remove)
    {
        _remove = remove;
        _converter = converter;
        _fromDictionary = fromDictionary;

        foreach (var pair in fromDictionary)
            Add(pair.Key, converter(pair.Value));
            
        Subscribe();
    }
        
    public ObservableDictionarySync(
        IReadOnlyObservableDictionary<TKey, TFrom> fromDictionary, 
        Func<TFrom, TTo> converter,
        bool isDisposable)
        : this(fromDictionary, converter, null)
    {
        _isDisposable = isDisposable;
    }

    private void Subscribe() => 
        _fromDictionary.CollectionChanged += OnFromListChanged;

    private void Unsubscribe() => 
        _fromDictionary.CollectionChanged -= OnFromListChanged;

    private TTo Convert(TFrom fromValue) =>
        _converter(fromValue);

    private void OnFromListChanged(INotifyCollectionChangedEventArgs<KeyValuePair<TKey, TFrom>> args)
    {
        switch (args.Action)
        {
            case NotifyCollectionChangedAction.Add:
                {
                    if (args.IsSingleItem) Add(args.NewItem.Key, Convert(args.NewItem.Value));
                    else throw new NotImplementedException();
                }
                break;
                
            case NotifyCollectionChangedAction.Remove:
                {
                    if (args.IsSingleItem) Remove(args.OldItem.Key);
                    else throw new NotImplementedException();
                }
                break;

            case NotifyCollectionChangedAction.Replace:
                {
                    if (args.IsSingleItem) base[args.NewItem.Key] = Convert(args.NewItem.Value);
                    else throw new NotImplementedException();
                }
                break;

            case NotifyCollectionChangedAction.Reset:
                {
                    Clear();
                }
                break;

            case NotifyCollectionChangedAction.Move: throw new NotImplementedException();
            default: throw new ArgumentOutOfRangeException();
        }
    }

    protected override void OnRemoved(in TKey key, in TTo value)
    {
        if (_isDisposable)
        {
            if (value is IDisposable disposable)
                disposable.Dispose();
        }
        else _remove?.Invoke(value);
    }

    protected override void OnReplaced(in KeyValuePair<TKey, TTo> oldItem, in KeyValuePair<TKey, TTo> newItem) =>
        OnRemoved(oldItem.Key, newItem.Value);

    protected override void OnClearing()
    {
        if (_isDisposable)
        {
            foreach (var value in Values)
            {
                if (value is IDisposable disposable)
                    disposable.Dispose();
            }
        }
        else if (_remove is not null)
        {
            foreach (var value in Values)
                _remove.Invoke(value);
        }
    }

    public override void Dispose()
    {
        Unsubscribe();
        base.Dispose();
    }
}