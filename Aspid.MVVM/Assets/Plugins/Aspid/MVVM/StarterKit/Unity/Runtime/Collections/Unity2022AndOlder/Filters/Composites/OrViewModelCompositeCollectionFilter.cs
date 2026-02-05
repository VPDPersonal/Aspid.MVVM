using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class OrViewModelCompositeCollectionFilter : 
        OrCompositeCollectionFilter<IViewModel, IViewModelCollectionFilter>, IViewModelCollectionFilter
    {
        public OrViewModelCompositeCollectionFilter(IViewModelCollectionFilter[] filters)
            : base(filters) { }
    }
}