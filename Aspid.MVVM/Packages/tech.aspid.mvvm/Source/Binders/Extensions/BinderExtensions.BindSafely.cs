using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Attaches the binder to the specified member.
        /// </summary>
        /// <param name="binder">The binder to bind. Skipped if <see langword="null"/> or destroyed.</param>
        /// <param name="context">The bindable member and binding owner.</param>
        public static void BindSafely(
            this IBinder? binder,
            in BindContext context)
        {
            if (binder.IsMissing()) return;
            binder.Bind(in context);
        }

        /// <summary>
        /// Attaches each binder to the specified member, skipping <see langword="null"/> and destroyed Unity objects.
        /// </summary>
        /// <remarks>Stops at the first failure without rolling back earlier bindings.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind. Pass <see langword="null"/> to skip the operation.</param>
        /// <param name="context">The bindable member and binding owner.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no member or owner.</exception>
        public static void BindSafely<T>(
            this T?[]? binders,
            in BindContext context)
            where T : IBinder
        {
            if (binders is null) return;
            context.EnsureValid();

            foreach (var binder in binders) 
                binder.BindSafely(in context);
        }

        /// <summary>
        /// Attaches each binder to the specified member, skipping <see langword="null"/> and destroyed Unity objects.
        /// </summary>
        /// <remarks>Stops at the first failure without rolling back earlier bindings.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind.</param>
        /// <param name="context">The bindable member and binding owner.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no member or owner.</exception>
        public static void BindSafely<T>(
            this in Span<T?> binders,
            in BindContext context)
            where T : IBinder
        {
            context.EnsureValid();

            foreach (var binder in binders) 
                binder.BindSafely(in context);
        }

        /// <inheritdoc cref="BindSafely{T}(in Span{T}, in BindContext)"/>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no member or owner.</exception>
        public static void BindSafely<T>(
            this in ReadOnlySpan<T?> binders,
            in BindContext context)
            where T : IBinder
        {
            context.EnsureValid();

            foreach (var binder in binders) 
                binder.BindSafely(in context);
        }

        /// <inheritdoc cref="BindSafely{T}(T[], in BindContext)"/>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no member or owner.</exception>
        public static void BindSafely<T>(
            this List<T?>? binders,
            in BindContext context)
            where T : IBinder
        {
            if (binders is null) return;
            context.EnsureValid();

            foreach (var binder in binders) 
                binder.BindSafely(in context);
        }

        /// <summary>
        /// Attaches each binder to the specified member, skipping <see langword="null"/> and destroyed Unity objects.
        /// </summary>
        /// <remarks>
        /// <para>Stops at the first failure without rolling back earlier bindings.</para>
        /// <para>An exception during enumerator disposal can mask the binding failure.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind. Pass <see langword="null"/> to skip the operation.</param>
        /// <param name="context">The bindable member and binding owner.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no member or owner.</exception>
        public static void BindSafely<T>(
            this IEnumerable<T?>? binders,
            in BindContext context)
            where T : IBinder
        {
            switch (binders)
            {
                case null: return;
                case T?[] array: array.BindSafely(in context); return;
                case List<T?> list: list.BindSafely(in context); return;
            }

            context.EnsureValid();

            foreach (var binder in binders) 
                binder.BindSafely(in context);
        }
    }
}
