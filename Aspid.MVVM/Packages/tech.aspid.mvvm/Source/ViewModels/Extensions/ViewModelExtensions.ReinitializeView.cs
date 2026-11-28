using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class ViewModelExtensions
    {
        /// <inheritdoc cref="ViewExtensions.Reinitialize(IView, IViewModel)"/>
        public static IViewModel? ReinitializeView(this IViewModel? newViewModel, IView view) =>
            view.Reinitialize(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViews{T}(T[], IViewModel)"/>
        public static void ReinitializeViews<T>(this IViewModel? newViewModel, T[] views)
            where T : IView =>
            views.ReinitializeViews(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViews{T}(Span{T}, IViewModel)"/>
        public static void ReinitializeViews<T>(this IViewModel? newViewModel, Span<T> views)
            where T : IView =>
            views.ReinitializeViews(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViews{T}(ReadOnlySpan{T}, IViewModel)"/>
        public static void ReinitializeViews<T>(this IViewModel? newViewModel, ReadOnlySpan<T> views)
            where T : IView =>
            views.ReinitializeViews(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViews{T}(List{T}, IViewModel)"/>
        public static void ReinitializeViews<T>(this IViewModel? newViewModel, List<T> views)
            where T : IView =>
            views.ReinitializeViews(newViewModel);

        /// <inheritdoc cref="ViewExtensions.ReinitializeViews{T}(IEnumerable{T}, IViewModel)"/>
        public static void ReinitializeViews<T>(this IViewModel? newViewModel, IEnumerable<T> views)
            where T : IView =>
            views.ReinitializeViews(newViewModel);
    }
}
