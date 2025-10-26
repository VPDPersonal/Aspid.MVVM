// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public sealed class AndViewModelCompositeCollectionFilter : 
        AndCompositeCollectionFilter<IViewModel, IViewModelCollectionFilter>, IViewModelCollectionFilter
    {
        public AndViewModelCompositeCollectionFilter(IViewModelCollectionFilter[] filters)
            : base(filters) { }
    }
}