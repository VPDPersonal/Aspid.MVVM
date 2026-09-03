// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Returns views produced by a factory back to it.
    /// </summary>
    /// <typeparam name="T">The type of the view.</typeparam>
    public interface IViewFactoryRelease<in T>
        where T : IView
    {
        /// <summary>
        /// Releases a view created by this factory.
        /// </summary>
        /// <param name="view">The view to release.</param>
        public void Release(T view);
    }
}
