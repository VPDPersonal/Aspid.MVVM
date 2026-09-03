#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Creates views for a ViewModel. Keyed creation ignores the key.
    /// </summary>
    /// <typeparam name="TView">The type of the created view.</typeparam>
    public interface IViewFactory<TView> : IViewFactoryWithKey<TView>
        where TView : IView
    {
        TView IViewFactoryWithKey<TView>.Create<TKey>(IViewModel? viewModel, TKey key) =>
            Create(viewModel);

        /// <summary>
        /// Creates a view for <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <returns>The created view.</returns>
        public TView Create(IViewModel? viewModel);
    }

    /// <summary>
    /// Creates views for a ViewModel with one extra argument. Keyed creation ignores the key.
    /// </summary>
    /// <typeparam name="T">The type of the extra argument.</typeparam>
    /// <typeparam name="TView">The type of the created view.</typeparam>
    public interface IViewFactory<in T, TView> : IViewFactoryWithKey<T, TView>
        where TView : IView
    {
        TView IViewFactoryWithKey<T, TView>.Create<TKey>(IViewModel? viewModel, TKey key, T? param) =>
            Create(viewModel, param);

        /// <summary>
        /// Creates a view for <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <param name="param">The extra argument.</param>
        /// <returns>The created view.</returns>
        public TView Create(IViewModel? viewModel, T? param);
    }

    /// <summary>
    /// Creates views for a ViewModel with two extra arguments. Keyed creation ignores the key.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra argument.</typeparam>
    /// <typeparam name="T2">The type of the second extra argument.</typeparam>
    /// <typeparam name="TView">The type of the created view.</typeparam>
    public interface IViewFactory<in T1, in T2, TView> : IViewFactoryWithKey<T1, T2, TView>
        where TView : IView
    {
        TView IViewFactoryWithKey<T1, T2, TView>.Create<TKey>(
            IViewModel? viewModel,
            TKey key,
            T1? param1,
            T2? param2) =>
            Create(viewModel, param1, param2);

        /// <summary>
        /// Creates a view for <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <param name="param1">The first extra argument.</param>
        /// <param name="param2">The second extra argument.</param>
        /// <returns>The created view.</returns>
        public TView Create(IViewModel? viewModel, T1? param1, T2? param2);
    }

    /// <summary>
    /// Creates views for a ViewModel with three extra arguments. Keyed creation ignores the key.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra argument.</typeparam>
    /// <typeparam name="T2">The type of the second extra argument.</typeparam>
    /// <typeparam name="T3">The type of the third extra argument.</typeparam>
    /// <typeparam name="TView">The type of the created view.</typeparam>
    public interface IViewFactory<in T1, in T2, in T3, TView> : IViewFactoryWithKey<T1, T2, T3, TView>
        where TView : IView
    {
        TView IViewFactoryWithKey<T1, T2, T3, TView>.Create<TKey>(
            IViewModel? viewModel,
            TKey key,
            T1? param1,
            T2? param2,
            T3? param3) =>
            Create(viewModel, param1, param2, param3);

        /// <summary>
        /// Creates a view for <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <param name="param1">The first extra argument.</param>
        /// <param name="param2">The second extra argument.</param>
        /// <param name="param3">The third extra argument.</param>
        /// <returns>The created view.</returns>
        public TView Create(IViewModel? viewModel, T1? param1, T2? param2, T3? param3);
    }
}
