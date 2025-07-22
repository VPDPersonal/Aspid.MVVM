using Aspid.MVVM.StarterKit.Composites;

namespace Aspid.MVVM.StarterKit.Unity
{
    public sealed class AndViewModelCompositeCollectionFilter : 
        AndCompositeCollectionFilter<IViewModel, IViewModelCollectionFilter>, IViewModelCollectionFilter
    {
        public AndViewModelCompositeCollectionFilter(IViewModelCollectionFilter[] filters)
            : base(filters) { }
    }
}