namespace Aspid.Collections.Observable.Synchronizer;

internal sealed class ObservableQueueSync<TFrom, TTo> : ObservableQueue<TTo>, IReadOnlyObservableCollectionSync<TTo>
{
    private readonly bool _isDisposable;
    private readonly Action<TTo>? _remove;
    private readonly Func<TFrom, TTo> _converter;
    private readonly ObservableQueue<TFrom> _fromQueue;

    public ObservableQueueSync(
        ObservableQueue<TFrom> fromQueue,
        Func<TFrom, TTo> converter,
        Action<TTo>? remove)
    {
        _remove = remove;
        _converter = converter;
        _fromQueue = fromQueue;

        foreach (var from in fromQueue) 
            Enqueue(converter(from));

        Subscribe();
    }

    public ObservableQueueSync(
        ObservableQueue<TFrom> fromQueue,
        Func<TFrom, TTo> converter,
        bool isDisposable = false)
        : this(fromQueue, converter, null)
    {
        _isDisposable = isDisposable;
    }

    private void Subscribe() => 
        _fromQueue.CollectionChanged += OnFromQueueChanged;

    private void Unsubscribe() =>
        _fromQueue.CollectionChanged -= OnFromQueueChanged;
        
    private TTo[] Convert(IReadOnlyList<TFrom> fromValues)
    {
        var toValues = new TTo[fromValues.Count];

        for (var i = 0; i < toValues.Length; i++)
            toValues[i] = Convert(fromValues[i]);

        return toValues;
    }

    private TTo Convert(TFrom fromValue) => 
        _converter(fromValue);

    private void OnFromQueueChanged(INotifyCollectionChangedEventArgs<TFrom> args)
    {
        switch (args.Action)
        {
            case NotifyCollectionChangedAction.Add:
                {
                    if (args.IsSingleItem) Enqueue(Convert(args.NewItem!));
                    else EnqueueRange(Convert(args.NewItems!));
                }
                break;
                
            case NotifyCollectionChangedAction.Remove:
                {
                    if (args.IsSingleItem) Dequeue();
                    else DequeueRange(new TTo[args.OldItems!.Count]);
                }
                break;

            case NotifyCollectionChangedAction.Reset:
                {
                    Clear();
                }
                break;

            case NotifyCollectionChangedAction.Move:
            case NotifyCollectionChangedAction.Replace:
                throw new NotImplementedException();
                    
            default: throw new ArgumentOutOfRangeException();
        }
    }

    protected override void OnDequeued(TTo item)
    {
        if (_isDisposable)
        {
            if (item is IDisposable disposable)
                disposable.Dispose();
        }
        else _remove?.Invoke(item);
    }

    protected override void OnDequeuedRange(in IReadOnlyList<TTo> dest) =>
        OnDequeuedRange(dest);
        
    private void OnDequeuedRange(IReadOnlyCollection<TTo> dest)
    {
        if (_isDisposable)
        {
            foreach (var value in dest)
            {
                if (value is IDisposable disposable)
                    disposable.Dispose();
            }
        }
        else if (_remove is not null)
        {
            foreach (var value in dest)
                _remove.Invoke(value);
        }
    }

    protected override void OnClearing() =>
        OnDequeuedRange(this);

    public override void Dispose()
    {
        Unsubscribe();
        base.Dispose();
    }
}