using System;
using System.Linq;

namespace Aspid.MVVM.StarterKit.Composites
{
    [Serializable]
    public sealed class OrCompositeCollectionFilter<T> : OrCompositeCollectionFilter<T, ICollectionFilter<T>>
    {
        public OrCompositeCollectionFilter(ICollectionFilter<T>[] filters) 
            : base(filters) { }
    }
    
    [Serializable]
    public class OrCompositeCollectionFilter<T, TFilter> : ICollectionFilter<T>
        where TFilter : ICollectionFilter<T>
    {
#if UNITY_2022_1_OR_NEWER
        [SerializeReferenceDropdown]
        [UnityEngine.SerializeReference] 
#endif
        private TFilter[] _filters;

        public OrCompositeCollectionFilter(TFilter[] filters)
        {
            _filters = filters;
        }

        public Predicate<T> Get() => Filter;

        private bool Filter(T value)
        {
            return _filters.Select(filter => filter?.Get())
                .Any(predicate => predicate?.Invoke(value) ?? true);
        }
    }
}