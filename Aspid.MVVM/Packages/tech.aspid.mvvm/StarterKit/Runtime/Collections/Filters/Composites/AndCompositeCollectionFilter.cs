using System;
using System.Linq;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class AndCompositeCollectionFilter<T> : AndCompositeCollectionFilter<T, ICollectionFilter<T>>
    {
        public AndCompositeCollectionFilter(ICollectionFilter<T>[] filters) 
            : base(filters) { }
    }
    
    public class AndCompositeCollectionFilter<T, TFilter> : ICollectionFilter<T>
        where TFilter : ICollectionFilter<T>
    {
        [TypeSelector]
        [SerializeReference] private TFilter[] _filters;

        public AndCompositeCollectionFilter(TFilter[] filters)
        {
            _filters = filters;
        }

        public Predicate<T> Get() => Filter;

        private bool Filter(T value)
        {
            return _filters.Select(filter => filter?.Get())
                .All(predicate => predicate?.Invoke(value) ?? true);
        }
    }
}