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
        /// <param name="view">The View to reinitialize. Skipped if <see langword="null"/> or destroyed.</param>
        /// <param name="newViewModel">A <see langword="null"/> or destroyed replacement leaves the View unbound.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View was skipped or had none.</returns>
        public static IViewModel? ReinitializeSafely(this IView? view, IViewModel? newViewModel) => view.IsMissing()
            ? null
            : view.Reinitialize(newViewModel);

        /// <summary>
        /// Reinitializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <param name="newViewModel">A <see langword="null"/> or destroyed replacement leaves each View unbound.</param>
        public static void ReinitializeViewsSafely<T>(this T[]? views, IViewModel? newViewModel)
            where T : IView?
        {
            if (views is null) return;

            foreach (var view in views)
                view.ReinitializeSafely(newViewModel);
        }

        /// <summary>
        /// Reinitializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips <see langword="null"/> or destroyed entries.</param>
        /// <param name="newViewModel">A <see langword="null"/> or destroyed replacement leaves each View unbound.</param>
        public static void ReinitializeViewsSafely<T>(this Span<T> views, IViewModel? newViewModel)
            where T : IView?
        {
            foreach (var view in views)
                view.ReinitializeSafely(newViewModel);
        }

        /// <inheritdoc cref="ReinitializeViewsSafely{T}(Span{T}, IViewModel)"/>
        public static void ReinitializeViewsSafely<T>(this ReadOnlySpan<T> views, IViewModel? newViewModel)
            where T : IView?
        {
            foreach (var view in views)
                view.ReinitializeSafely(newViewModel);
        }

        /// <inheritdoc cref="ReinitializeViewsSafely{T}(T[], IViewModel)"/>
        public static void ReinitializeViewsSafely<T>(this List<T>? views, IViewModel? newViewModel)
            where T : IView?
        {
            if (views is null) return;

            foreach (var view in views)
                view.ReinitializeSafely(newViewModel);
        }

        /// <summary>
        /// Reinitializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <param name="newViewModel">A <see langword="null"/> or destroyed replacement leaves each View unbound.</param>
        /// <exception cref="AggregateException">
        /// Processing the sequence and disposing its enumerator both fail.
        /// </exception>
        public static void ReinitializeViewsSafely<T>(this IEnumerable<T>? views, IViewModel? newViewModel)
            where T : IView?
        {
            switch (views)
            {
                case null: return;
                case T[] array: array.ReinitializeViewsSafely(newViewModel); return;
                case List<T> list: list.ReinitializeViewsSafely(newViewModel); return;
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = views.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        enumerator.Current.ReinitializeSafely(newViewModel);
                }
                catch (Exception exception)
                {
                    failures.Add(exception);
                }
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }
    }
}
