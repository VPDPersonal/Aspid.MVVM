using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBePrivate.Global")]
    public static partial class ViewExtensions
    {
        /// <summary>
        /// Deinitializes the View before binding the replacement ViewModel.
        /// </summary>
        /// <param name="view">The View to reinitialize.</param>
        /// <param name="newViewModel">A <see langword="null"/> or destroyed replacement leaves the View unbound.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or destroyed.
        /// </exception>
        public static IViewModel? Reinitialize(this IView view, IViewModel? newViewModel)
        {
            var oldViewModel = view.DeinitializeView();

            if (!newViewModel.IsMissing())
                view.Initialize(newViewModel);

            return oldViewModel;
        }

        /// <summary>
        /// Reinitializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to reinitialize.</param>
        /// <param name="newViewModel">A <see langword="null"/> or destroyed replacement leaves each View unbound.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="views"/> is <see langword="null"/>, or a View is <see langword="null"/> or destroyed.
        /// </exception>
        public static void ReinitializeViews<T>(this T[] views, IViewModel? newViewModel)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            foreach (var view in views)
                view.Reinitialize(newViewModel);
        }

        /// <summary>
        /// Reinitializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to reinitialize.</param>
        /// <param name="newViewModel">A <see langword="null"/> or destroyed replacement leaves each View unbound.</param>
        /// <exception cref="ArgumentNullException">A View is <see langword="null"/> or destroyed.</exception>
        public static void ReinitializeViews<T>(this Span<T> views, IViewModel? newViewModel)
            where T : IView
        {
            foreach (var view in views)
                view.Reinitialize(newViewModel);
        }

        /// <inheritdoc cref="ReinitializeViews{T}(Span{T}, IViewModel)"/>
        public static void ReinitializeViews<T>(this ReadOnlySpan<T> views, IViewModel? newViewModel)
            where T : IView
        {
            foreach (var view in views)
                view.Reinitialize(newViewModel);
        }

        /// <inheritdoc cref="ReinitializeViews{T}(T[], IViewModel)"/>
        public static void ReinitializeViews<T>(this List<T> views, IViewModel? newViewModel)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            foreach (var view in views)
                view.Reinitialize(newViewModel);
        }

        /// <inheritdoc cref="ReinitializeViews{T}(T[], IViewModel)"/>
        public static void ReinitializeViews<T>(this IEnumerable<T> views, IViewModel? newViewModel)
            where T : IView
        {
            switch (views)
            {
                case T[] array: array.ReinitializeViews(newViewModel); return;
                case List<T> list: list.ReinitializeViews(newViewModel); return;
                case null: throw new ArgumentNullException(nameof(views));
            }

            foreach (var view in views)
                view.Reinitialize(newViewModel);
        }
    }
}
