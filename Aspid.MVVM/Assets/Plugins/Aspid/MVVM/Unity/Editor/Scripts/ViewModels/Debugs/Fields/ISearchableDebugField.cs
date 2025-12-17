// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for debug fields that support search functionality.
    /// </summary>
    internal interface ISearchableDebugField
    {
        /// <summary>
        /// Searches for a field matching the given path.
        /// Returns true if this field matches the search query.
        /// When a match is found, the field should be made visible (expanded if needed).
        /// </summary>
        /// <param name="searchPath">The search query can be a simple name or path like "_hero._points"</param>
        /// <returns>True if this field matches the search</returns>
        public bool Search(string searchPath);
        
        /// <summary>
        /// Clears the search state and restores default visibility.
        /// </summary>
        public void ClearSearch();
    }
}
