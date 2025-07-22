using System;
using Aspid.MVVM.StarterKit.Composites;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class OrViewModelCompositeCollectionFilter : 
        OrCompositeCollectionFilter<IViewModel, IViewModelCollectionFilter>, IViewModelCollectionFilter
    {
        public OrViewModelCompositeCollectionFilter(IViewModelCollectionFilter[] filters)
            : base(filters) { }
    }
}