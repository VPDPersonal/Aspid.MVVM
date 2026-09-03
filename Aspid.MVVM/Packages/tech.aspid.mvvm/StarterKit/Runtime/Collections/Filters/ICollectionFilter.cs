// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Serializable filter for collection binders.
    /// </summary>
    /// <typeparam name="T">The element type being filtered.</typeparam>
    public interface ICollectionFilter<in T>
    {
        /// <summary>
        /// Returns whether the element is shown.
        /// </summary>
        /// <param name="item">The element to test.</param>
        public bool Matches(T item);
    }
}
