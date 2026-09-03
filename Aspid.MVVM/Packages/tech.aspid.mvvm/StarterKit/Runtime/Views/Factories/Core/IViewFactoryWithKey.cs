#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Creates views for a ViewModel identified by a key.
    /// </summary>
    /// <typeparam name="TView">The type of the created view.</typeparam>
    public interface IViewFactoryWithKey<TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        /// <summary>
        /// Creates a view for <paramref name="viewModel"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <param name="key">The key the ViewModel is stored under.</param>
        /// <returns>The created view.</returns>
        public TView Create<TKey>(IViewModel? viewModel, TKey key);
    }

    /// <summary>
    /// Creates views for a ViewModel identified by a key, with one extra argument.
    /// </summary>
    /// <typeparam name="T">The type of the extra argument.</typeparam>
    /// <typeparam name="TView">The type of the created view.</typeparam>
    public interface IViewFactoryWithKey<in T, TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        /// <summary>
        /// Creates a view for <paramref name="viewModel"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <param name="key">The key the ViewModel is stored under.</param>
        /// <param name="param">The extra argument.</param>
        /// <returns>The created view.</returns>
        public TView Create<TKey>(IViewModel? viewModel, TKey key, T? param);
    }

    /// <summary>
    /// Creates views for a ViewModel identified by a key, with two extra arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra argument.</typeparam>
    /// <typeparam name="T2">The type of the second extra argument.</typeparam>
    /// <typeparam name="TView">The type of the created view.</typeparam>
    public interface IViewFactoryWithKey<in T1, in T2, TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        /// <summary>
        /// Creates a view for <paramref name="viewModel"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <param name="key">The key the ViewModel is stored under.</param>
        /// <param name="param1">The first extra argument.</param>
        /// <param name="param2">The second extra argument.</param>
        /// <returns>The created view.</returns>
        public TView Create<TKey>(IViewModel? viewModel, TKey key, T1? param1, T2? param2);
    }

    /// <summary>
    /// Creates views for a ViewModel identified by a key, with three extra arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra argument.</typeparam>
    /// <typeparam name="T2">The type of the second extra argument.</typeparam>
    /// <typeparam name="T3">The type of the third extra argument.</typeparam>
    /// <typeparam name="TView">The type of the created view.</typeparam>
    public interface IViewFactoryWithKey<in T1, in T2, in T3, TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        /// <summary>
        /// Creates a view for <paramref name="viewModel"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <param name="key">The key the ViewModel is stored under.</param>
        /// <param name="param1">The first extra argument.</param>
        /// <param name="param2">The second extra argument.</param>
        /// <param name="param3">The third extra argument.</param>
        /// <returns>The created view.</returns>
        public TView Create<TKey>(IViewModel? viewModel, TKey key, T1? param1, T2? param2, T3? param3);
    }
}
