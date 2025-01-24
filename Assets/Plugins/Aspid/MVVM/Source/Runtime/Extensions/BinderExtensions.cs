using System;
using System.Collections.Generic;

namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for the <see cref="IBinder"/> interface.
    /// </summary>
    public static class BinderExtensions
    {
        /// <summary>
        /// Safely binds an object of type <see cref="IBinder"/>.
        /// If <paramref name="binder"/> is <c>null</c>, binding does not occur.
        /// </summary>
        /// <param name="binder">The object to be bound to the <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">The ViewModel to bind to.</param>
        /// <param name="id">The component ID that matches the property name in the ViewModel.</param>
        /// <typeparam name="T">The type that implements the <see cref="IBinder"/> interface.</typeparam>
        public static void BindSafely<T>(this T? binder, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binder is null) return;
            binder.Bind(new BindParameters(viewModel, id));
        }
        
        /// <summary>
        /// Safely binds an array of <see cref="IBinder"/> objects.
        /// If <paramref name="binders"/> is <c>null</c>, binding does not occur.
        /// If any element in the array is <c>null</c>, a <see cref="NullReferenceException"/> is thrown.
        /// </summary>
        /// <param name="binders">An array of objects to be bound to the <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">The ViewModel to bind to.</param>
        /// <param name="id">The component ID that matches the property name in the ViewModel.</param>
        /// <typeparam name="T">The type that implements the <see cref="IBinder"/> interface.</typeparam>
        public static void BindSafely<T>(this T[]? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;
            var parameters = new BindParameters(viewModel, id);

            foreach (var binder in binders)
            {
	            if (binder is null) throw new NullReferenceException($"Binder {id} is null. ViewModel {viewModel.GetType().FullName}");
	            binder.Bind(parameters);
            }
        }

        /// <summary>
        /// Safely binds a list of <see cref="IBinder"/> objects.
        /// If <paramref name="binders"/> is <c>null</c>, binding does not occur.
        /// If any element in the list is <c>null</c>, a <see cref="NullReferenceException"/> is thrown.
        /// </summary>
        /// <param name="binders">A list of objects to be bound to the <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">The ViewModel to bind to.</param>
        /// <param name="id">The component ID that matches the property name in the ViewModel.</param>
        /// <typeparam name="T">The type that implements the <see cref="IBinder"/> interface.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public static void BindSafely<T>(this List<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;
            var parameters = new BindParameters(viewModel, id);

            foreach (var binder in binders)
            {
	            if (binder is null) throw new NullReferenceException($"Binder {id} is null. ViewModel {viewModel.GetType().FullName}");
	            binder.Bind(parameters);
            }
        }
        
        /// <summary>
        /// Safely binds an enumeration of <see cref="IBinder"/> objects.
        /// If <paramref name="binders"/> is <c>null</c>, binding does not occur.
        /// If any element in the enumeration is <c>null</c>, a <see cref="NullReferenceException"/> is thrown.
        /// </summary>
        /// <param name="binders">An enumeration of objects to be bound to the <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">The ViewModel to bind to.</param>
        /// <param name="id">The component ID that matches the property name in the ViewModel.</param>
        /// <typeparam name="T">The type that implements the <see cref="IBinder"/> interface.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        public static void BindSafely<T>(this IEnumerable<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;
            var parameters = new BindParameters(viewModel, id);

            foreach (var binder in binders)
            {
	            if (binder is null) throw new NullReferenceException($"Binder {id} is null. ViewModel {viewModel.GetType().FullName}");
	            binder.Bind(parameters);
            }
        }
        
        /// <summary>
        /// Safely unbinds an object of type <see cref="IBinder"/>.
        /// If <paramref name="binder"/> is <c>null</c>, unbinding does not occur.
        /// </summary>
        /// <param name="binder">The object to unbind from the <see cref="IViewModel"/>.</param>
        /// <typeparam name="T">The type that implements the <see cref="IBinder"/> interface.</typeparam>
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