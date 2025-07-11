using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Safely unbinds a single <see cref="Aspid.MVVM.IBinder"/> instance.
        /// If the binder is <c>null</c>, the call is ignored.
        /// </summary>
        /// <typeparam name="T">The type that implements the <see cref="Aspid.MVVM.IBinder"/> interface.</typeparam>
        /// <param name="binder">The binder to unbind from the <see cref="IViewModel"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this T? binder)
            where T : IBinder
        {
            if (binder is null) return;
            binder.Unbind();
        }
        
        /// <summary>
        /// Safely unbinds an array of <see cref="Aspid.MVVM.IBinder"/> instances.
        /// If <paramref name="binders"/> is <c>null</c>, unbinding is skipped.
        /// </summary>
        /// <typeparam name="T">The type that implements the <see cref="Aspid.MVVM.IBinder"/> interface.</typeparam>
        /// <param name="binders">An array of binders to unbind from the <see cref="IViewModel"/>.</param>
        /// <exception cref="UnbindSafelyNullReferenceException">
        /// Thrown if any element in the array is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this T[]? binders)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
                if (binder is null) throw new UnbindSafelyNullReferenceException($"{nameof(binder)}");
                binder.Unbind();
            }
        }
        
        /// <summary>
        /// Safely unbinds a list of <see cref="Aspid.MVVM.IBinder"/> instances.
        /// If <paramref name="binders"/> is <c>null</c>, unbinding is skipped.
        /// </summary>
        /// <typeparam name="T">The type that implements the <see cref="Aspid.MVVM.IBinder"/> interface.</typeparam>
        /// <param name="binders">A list of binders to unbind from the <see cref="IViewModel"/>.</param>
        /// <exception cref="UnbindSafelyNullReferenceException">
        /// Thrown if any element in the list is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this List<T>? binders)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
                if (binder is null) throw new UnbindSafelyNullReferenceException($"{nameof(binder)}");
	            binder.Unbind();
            }
        }
        
        /// <summary>
        /// Safely unbinds a sequence of <see cref="Aspid.MVVM.IBinder"/> instances.
        /// If <paramref name="binders"/> is <c>null</c>, unbinding is skipped.
        /// </summary>
        /// <typeparam name="T">The type that implements the <see cref="Aspid.MVVM.IBinder"/> interface.</typeparam>
        /// <param name="binders">An enumerable of binders to unbind from the <see cref="IViewModel"/>.</param>
        /// <exception cref="UnbindSafelyNullReferenceException">
        /// Thrown if any element in the sequence is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this IEnumerable<T>? binders)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
                if (binder is null) throw new UnbindSafelyNullReferenceException($"{nameof(binder)}");
	            binder.Unbind();
            }
        }
    }
}