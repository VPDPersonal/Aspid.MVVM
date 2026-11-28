using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class ViewModelExtensions
    {
        /// <summary>
        /// Calls <see cref="IView.Initialize"/> with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel passed to the View.</param>
        /// <param name="view">The View to initialize.</param>
        /// <exception cref="NullReferenceException"><paramref name="view"/> is <see langword="null"/>.</exception>
        public static void InitializeView(this IViewModel viewModel, IView view) =>
            view.Initialize(viewModel);
        
        /// <inheritdoc cref="ViewExtensions.InitializeViews{T}(T[], IViewModel)"/>
        public static void InitializeViews<T>(this IViewModel viewModel, params T[] views)
            where T : IView =>
            views.InitializeViews(viewModel);

        /// <inheritdoc cref="ViewExtensions.InitializeViews{T}(Span{T}, IViewModel)"/>
        public static void InitializeViews<T>(this IViewModel viewModel, Span<T> views)
            where T : IView =>
            views.InitializeViews(viewModel);

        /// <inheritdoc cref="ViewExtensions.InitializeViews{T}(ReadOnlySpan{T}, IViewModel)"/>
        public static void InitializeViews<T>(this IViewModel viewModel, ReadOnlySpan<T> views)
            where T : IView =>
            views.InitializeViews(viewModel);

        /// <inheritdoc cref="ViewExtensions.InitializeViews{T}(List{T}, IViewModel)"/>
        public static void InitializeViews<T>(this IViewModel viewModel, List<T> views)
            where T : IView =>
            views.InitializeViews(viewModel);

        /// <inheritdoc cref="ViewExtensions.InitializeViews{T}(IEnumerable{T}, IViewModel)"/>
        public static void InitializeViews<T>(this IViewModel viewModel, IEnumerable<T> views)
            where T : IView =>
            views.InitializeViews(viewModel);
    }
}
