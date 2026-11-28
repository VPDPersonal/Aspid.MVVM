using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class ViewModelExtensions
    {
        /// <inheritdoc cref="ViewExtensions.ReinitializeSafely(IView, IViewModel)"/>
        public static IViewModel? ReinitializeViewSafely(this IViewModel? newViewModel, IView? view) =>
            view.ReinitializeSafely(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViewsSafely{T}(T[], IViewModel)"/>
        public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, T[]? views)
            where T : IView? =>
            views.ReinitializeViewsSafely(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViewsSafely{T}(Span{T}, IViewModel)"/>
        public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, Span<T> views)
            where T : IView? =>
            views.ReinitializeViewsSafely(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViewsSafely{T}(ReadOnlySpan{T}, IViewModel)"/>
        public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, ReadOnlySpan<T> views)
            where T : IView? =>
            views.ReinitializeViewsSafely(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViewsSafely{T}(List{T}, IViewModel)"/>
        public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, List<T>? views)
            where T : IView? =>
            views.ReinitializeViewsSafely(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViewsSafely{T}(IEnumerable{T}, IViewModel)"/>
        public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, IEnumerable<T>? views)
            where T : IView? =>
            views.ReinitializeViewsSafely(newViewModel);
    }
}
