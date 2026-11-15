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
        /// <remarks>
        /// If release throws, the view must not remain available for reuse. Collection binders may
        /// deinitialize and destroy it as emergency cleanup; they do not retry the failed release.
        /// </remarks>
        /// <param name="view">The view to release.</param>
        public void Release(T view);
    }
}
