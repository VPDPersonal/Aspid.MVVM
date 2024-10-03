using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Aspid.Collections.Observable.Synchronizer
{
    public sealed class ObservableDictionarySync<TKey, TFrom, TTo> : IDisposable
        where TKey : notnull
    {
        private readonly IReadOnlyObservableDictionary<TKey, TFrom> _fromDictionary;
        private readonly ObservableDictionary<TKey, TTo> _toDictionary;
        private readonly Func<TFrom, TTo> _converter;

        public ObservableDictionarySync(
            IReadOnlyObservableDictionary<TKey, TFrom> fromDictionary, 
            out ObservableDictionary<TKey, TTo> toDictionary,
            Func<TFrom, TTo> converter)
        {
            _converter = converter;
            _fromDictionary = fromDictionary;
            _toDictionary = toDictionary = new ObservableDictionary<TKey, TTo>(fromDictionary.Count);

            foreach (var pair in fromDictionary)
                toDictionary.Add(pair.Key, converter(pair.Value));
            
            Subscribe();
        }

        private void Subscribe() => _fromDictionary.CollectionChanged += OnFromListChanged;

        private void Unsubscribe() => _fromDictionary.CollectionChanged -= OnFromListChanged;
        
        private TTo[] Convert(IReadOnlyList<TFrom> fromValues)
        {
            var toValues = new TTo[fromValues.Count];

            for (var i = 0; i < toValues.Length; i++)
                toValues[i] = Convert(fromValues[i]);

            return toValues;
        }

        private TTo Convert(TFrom fromValue) => _converter(fromValue);

        private void OnFromListChanged(INotifyCollectionChangedEventArgs<KeyValuePair<TKey, TFrom>> args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (args.IsSingleItem) _toDictionary.Add(args.NewItem.Key, Convert(args.NewItem.Value));
                        else throw new NotImplementedException();
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (args.IsSingleItem) _toDictionary.Remove(args.OldItem.Key);
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (args.IsSingleItem) _toDictionary[args.NewItem.Key] = Convert(args.NewItem.Value);
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        _toDictionary.Clear();
                    }
                    break;

                case NotifyCollectionChangedAction.Move: throw new NotImplementedException();
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose() => Unsubscribe();
    }
}