using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Aspid.Collections.Observable.Synchronizer
{
    public sealed class ObservableListSync<TFrom, TTo> : IDisposable
    {
        private readonly Func<TFrom, TTo> _converter;
        private readonly ObservableList<TTo> _toList;
        private readonly IReadOnlyObservableList<TFrom> _fromList;

        public ObservableListSync(
            IReadOnlyObservableList<TFrom> fromList, 
            out ObservableList<TTo> toList,
            Func<TFrom, TTo> converter)
        {
            _fromList = fromList;
            _converter = converter;
            _toList = toList = new ObservableList<TTo>(fromList.Count);

            foreach (var from in fromList)
                toList.Add(converter(from));
            
            Subscribe();
        }

        private void Subscribe() => 
            _fromList.CollectionChanged += OnFromListChanged;

        private void Unsubscribe() => 
            _fromList.CollectionChanged -= OnFromListChanged;
        
        private TTo[] Convert(IReadOnlyList<TFrom> fromValues)
        {
            var toValues = new TTo[fromValues.Count];

            for (var i = 0; i < toValues.Length; i++)
                toValues[i] = Convert(fromValues[i]);

            return toValues;
        }

        private TTo Convert(TFrom fromValue) => 
            _converter(fromValue);

        private void OnFromListChanged(INotifyCollectionChangedEventArgs<TFrom> args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (args.IsSingleItem) _toList.Add(Convert(args.NewItem!));
                        else _toList.AddRange(Convert(args.NewItems!));
                    }
                    break;
                
                case NotifyCollectionChangedAction.Move:
                    {
                        if (args.IsSingleItem) _toList.Move(args.OldStartingIndex, args.NewStartingIndex);
                        else throw new NotImplementedException();
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (args.IsSingleItem) _toList.RemoveAt(args.OldStartingIndex);
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (args.IsSingleItem) _toList[args.OldStartingIndex] = Convert(args.NewItem!);
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        _toList.Clear();
                    }
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose() => 
            Unsubscribe();
    }
}