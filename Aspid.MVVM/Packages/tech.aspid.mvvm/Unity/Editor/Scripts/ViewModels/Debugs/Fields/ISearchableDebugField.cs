// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal interface ISearchableDebugField
    {
        public bool Search(string searchPath, string typeFilter = null);
        
        public void ClearSearch();
    }
}