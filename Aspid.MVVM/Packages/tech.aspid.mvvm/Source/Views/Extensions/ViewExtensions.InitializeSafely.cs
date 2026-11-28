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
        /// Initializes the View with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="view">The View to initialize. Skipped if <see langword="null"/> or destroyed.</param>
        /// <param name="viewModel">Initialization is skipped for a <see langword="null"/> or destroyed ViewModel.</param>
        public static void InitializeSafely(this IView? view, IViewModel? viewModel)
        {
            if (viewModel.IsMissing()) return;
            view.InitializeViewsSafelyInternal(viewModel);
        }

        /// <summary>
        /// Initializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <param name="viewModel">Initialization is skipped for a <see langword="null"/> or destroyed ViewModel.</param>
        public static void InitializeViewsSafely<T>(this T[]? views, IViewModel? viewModel)
            where T : IView?
        {
            if (views is null || viewModel.IsMissing()) return;

            foreach (var view in views)
                view.InitializeViewsSafelyInternal(viewModel);
        }

        /// <summary>
        /// Initializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips <see langword="null"/> or destroyed entries.</param>
        /// <param name="viewModel">Initialization is skipped for a <see langword="null"/> or destroyed ViewModel.</param>
        public static void InitializeViewsSafely<T>(this Span<T> views, IViewModel? viewModel)
            where T : IView?
        {
            if (viewModel.IsMissing()) return;

            foreach (var view in views)
                view.InitializeViewsSafelyInternal(viewModel);
        }

        /// <inheritdoc cref="InitializeViewsSafely{T}(Span{T}, IViewModel)"/>
        public static void InitializeViewsSafely<T>(this ReadOnlySpan<T> views, IViewModel? viewModel)
            where T : IView?
        {
            if (viewModel.IsMissing()) return;

            foreach (var view in views)
                view.InitializeViewsSafelyInternal(viewModel);
        }

        /// <inheritdoc cref="InitializeViewsSafely{T}(T[], IViewModel)"/>
        public static void InitializeViewsSafely<T>(this List<T>? views, IViewModel? viewModel)
            where T : IView?
        {
            if (views is null || viewModel.IsMissing()) return;

            foreach (var view in views)
                view.InitializeViewsSafelyInternal(viewModel);
        }

        /// <summary>
        /// Initializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <param name="viewModel">Initialization is skipped for a <see langword="null"/> or destroyed ViewModel.</param>
        /// <exception cref="AggregateException">
        /// Processing the sequence and disposing its enumerator both fail.
        /// </exception>
        public static void InitializeViewsSafely<T>(this IEnumerable<T>? views, IViewModel? viewModel)
            where T : IView?
        {
            switch (views)
            {
                case null: return;
                case T[] array: array.InitializeViewsSafely(viewModel); return;
                case List<T> list: list.InitializeViewsSafely(viewModel); return;
            }

            if (viewModel.IsMissing()) return;
            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = views.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        enumerator.Current.InitializeViewsSafelyInternal(viewModel);
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

        private static void InitializeViewsSafelyInternal(this IView? view, IViewModel viewModel)
        {
            if (view.IsMissing()) return;
            view.Initialize(viewModel);
        }
    }
}
