using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this T? binder)
            where T : IBinder
        {
            if (binder is null) return;
            binder.Unbind();
        }
        
        /// <summary>
        /// Safely unbinds an array of <see cref="IBinder"/> objects.
        /// If <paramref name="binders"/> is <c>null</c>, unbinding does not occur.
        /// If any element in the array is <c>null</c>, a <see cref="NullReferenceException"/> is thrown.
        /// </summary>
        /// <param name="binders">An array of objects to unbind from the <see cref="IViewModel"/>.</param>
        /// <typeparam name="T">The type that implements the <see cref="IBinder"/> interface.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this T[]? binders)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException($"{nameof(binder)}");
                binder.Unbind();
            }
        }
        
        /// <summary>
        /// Safely unbinds a list of <see cref="IBinder"/> objects.
        /// If <paramref name="binders"/> is <c>null</c>, unbinding does not occur.
        /// If any element in the list is <c>null</c>, a <see cref="NullReferenceException"/> is thrown.
        /// </summary>
        /// <param name="binders">A list of objects to unbind from the <see cref="IViewModel"/>.</param>
        /// <typeparam name="T">The type that implements the <see cref="IBinder"/> interface.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this List<T>? binders)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException($"{nameof(binder)}");
	            binder.Unbind();
            }
        }
        
        /// <summary>
        /// Safely unbinds an enumeration of <see cref="IBinder"/> objects.
        /// If <paramref name="binders"/> is <c>null</c>, unbinding does not occur.
        /// If any element in the enumeration is <c>null</c>, a <see cref="NullReferenceException"/> is thrown.
        /// </summary>
        /// <param name="binders">An enumeration of objects to unbind from the <see cref="IViewModel"/>.</param>
        /// <typeparam name="T">The type that implements the <see cref="IBinder"/> interface.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this IEnumerable<T>? binders)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException($"{nameof(binder)}");
	            binder.Unbind();
            }
        }
    }
}