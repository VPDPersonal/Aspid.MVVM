using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IBinder"/> and binder collections.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBePrivate.Global")]
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Attaches each binder to the specified member.
        /// </summary>
        /// <remarks>Stops at the first failure without rolling back earlier bindings.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind.</param>
        /// <param name="context">The bindable member and binding owner.</param>
        /// <exception cref="ArgumentNullException"><paramref name="binders"/> is <see langword="null"/>.</exception>
        /// <exception cref="NullReferenceException">An entry is <see langword="null"/>.</exception>
        public static void Bind<T>(
            this T[] binders,
            in BindContext context) 
            where T : IBinder
        {
            if (binders is null)
                throw new ArgumentNullException(nameof(binders));
            
            foreach (var binder in binders)
                binder.Bind(in context);
        }
        
        /// <summary>
        /// Attaches each binder to the specified member.
        /// </summary>
        /// <remarks>Stops at the first failure without rolling back earlier bindings.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind.</param>
        /// <param name="context">The bindable member and binding owner.</param>
        /// <exception cref="NullReferenceException">An entry is <see langword="null"/>.</exception>
        public static void Bind<T>(
            this in Span<T> binders,
            in BindContext context) 
            where T : IBinder
        {
            foreach (var binder in binders)
                binder.Bind(in context);
        }
        
        /// <inheritdoc cref="Bind{T}(in Span{T}, in BindContext)"/>
        public static void Bind<T>(
            this in ReadOnlySpan<T> binders,
            in BindContext context) 
            where T : IBinder
        {
            foreach (var binder in binders)
                binder.Bind(in context);
        }
        
        /// <inheritdoc cref="Bind{T}(T[], in BindContext)"/>
        public static void Bind<T>(
            this List<T> binders, 
            in BindContext context) 
            where T : IBinder
        {
            if (binders is null)
                throw new ArgumentNullException(nameof(binders));
            
            foreach (var binder in binders)
                binder.Bind(in context);
        }
        
        /// <inheritdoc cref="Bind{T}(T[], in BindContext)"/>
        public static void Bind<T>(
            this IEnumerable<T> binders, 
            in BindContext context) 
            where T : IBinder
        {
            switch (binders)
            {
                case T[] array: array.Bind(in context); return;
                case List<T> list: list.Bind(in context); return;
                case null: throw new ArgumentNullException(nameof(binders));
            }

            foreach (var binder in binders)
                binder.Bind(in context);
        }
    }
}
