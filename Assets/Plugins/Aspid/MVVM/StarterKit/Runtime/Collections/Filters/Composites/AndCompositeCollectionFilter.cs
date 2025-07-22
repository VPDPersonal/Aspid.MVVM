using System;
using System.Linq;

namespace Aspid.MVVM.StarterKit.Composites
{
    [Serializable]
    public sealed class AndCompositeCollectionFilter<T> : OrCompositeCollectionFilter<T, ICollectionFilter<T>>
    {
        public AndCompositeCollectionFilter(ICollectionFilter<T>[] filters) 
            : base(filters) { }
    }
    
    public class AndCompositeCollectionFilter<T, TFilter> : ICollectionFilter<T>
        where TFilter : ICollectionFilter<T>
    {
#if UNITY_2022_1_OR_NEWER
        [SerializeReferenceDropdown]
        [UnityEngine.SerializeReference] 
#endif
        private TFilter[] _filters;

        public AndCompositeCollectionFilter(TFilter[] filters)
        {
            _filters = filters;
        }

        public Predicate<T> Get() => Filter;

        private bool Filter(T value)
        {
            return _filters.Select(filter => filter.Get())
                .All(predicate => predicate?.Invoke(value) ?? true);
        }
    }
}