using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class ViewModelExtensions
    {
        /// <inheritdoc cref="ViewExtensions.InitializeSafely(IView, IViewModel)"/>
        public static void InitializeViewSafely(this IViewModel? viewModel, IView? view) =>
            view.InitializeSafely(viewModel);

        /// <inheritdoc cref="ViewExtensions.InitializeViewsSafely{T}(T[], IViewModel)"/>
        public static void InitializeViewsSafely<T>(this IViewModel? viewModel, params T[]? views)
            where T : IView? =>
            views.InitializeViewsSafely(viewModel);

        /// <inheritdoc cref="ViewExtensions.InitializeViewsSafely{T}(Span{T}, IViewModel)"/>
        public static void InitializeViewsSafely<T>(this IViewModel? viewModel, Span<T> views)
            where T : IView? =>
            views.InitializeViewsSafely(viewModel);

        /// <inheritdoc cref="ViewExtensions.InitializeViewsSafely{T}(ReadOnlySpan{T}, IViewModel)"/>
        public static void InitializeViewsSafely<T>(this IViewModel? viewModel, ReadOnlySpan<T> views)
            where T : IView? =>
            views.InitializeViewsSafely(viewModel);

        /// <inheritdoc cref="ViewExtensions.InitializeViewsSafely{T}(List{T}, IViewModel)"/>
        public static void InitializeViewsSafely<T>(this IViewModel? viewModel, List<T>? views)
            where T : IView? =>
            views.InitializeViewsSafely(viewModel);

        /// <inheritdoc cref="ViewExtensions.InitializeViewsSafely{T}(IEnumerable{T}, IViewModel)"/>
        public static void InitializeViewsSafely<T>(this IViewModel? viewModel, IEnumerable<T>? views)
            where T : IView? =>
            views.InitializeViewsSafely(viewModel);
    }
}
