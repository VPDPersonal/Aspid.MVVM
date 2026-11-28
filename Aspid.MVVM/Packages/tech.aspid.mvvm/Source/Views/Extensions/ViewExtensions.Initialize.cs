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
        /// Initializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to initialize.</param>
        /// <param name="viewModel">The ViewModel passed to each View.</param>
        /// <exception cref="ArgumentNullException"><paramref name="views"/> is <see langword="null"/>.</exception>
        /// <exception cref="NullReferenceException">A View is <see langword="null"/>.</exception>
        public static void InitializeViews<T>(this T[] views, IViewModel viewModel)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            foreach (var view in views)
                view.Initialize(viewModel);
        }

        /// <summary>
        /// Initializes the Views in order until one fails.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to initialize.</param>
        /// <param name="viewModel">The ViewModel passed to each View.</param>
        /// <exception cref="NullReferenceException">A View is <see langword="null"/>.</exception>
        public static void InitializeViews<T>(this Span<T> views, IViewModel viewModel)
            where T : IView
        {
            foreach (var view in views)
                view.Initialize(viewModel);
        }

        /// <inheritdoc cref="InitializeViews{T}(Span{T}, IViewModel)"/>
        public static void InitializeViews<T>(this ReadOnlySpan<T> views, IViewModel viewModel)
            where T : IView
        {
            foreach (var view in views)
                view.Initialize(viewModel);
        }

        /// <inheritdoc cref="InitializeViews{T}(T[], IViewModel)"/>
        public static void InitializeViews<T>(this List<T> views, IViewModel viewModel)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            foreach (var view in views)
                view.Initialize(viewModel);
        }

        /// <inheritdoc cref="InitializeViews{T}(T[], IViewModel)"/>
        public static void InitializeViews<T>(this IEnumerable<T> views, IViewModel viewModel)
            where T : IView
        {
            switch (views)
            {
                case T[] array: array.InitializeViews(viewModel); return;
                case List<T> list: list.InitializeViews(viewModel); return;
                case null: throw new ArgumentNullException(nameof(views));
            }

            foreach (var view in views)
                view.Initialize(viewModel);
        }
    }
}
