using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        #region Singl BindSafely
        /// <summary>
        /// Binds a single binder to the provided <see cref="IBinderAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type, implementing <see cref="Aspid.MVVM.IBinder"/>.</typeparam>
        /// <param name="binder">The binder instance to bind.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T? binder, in FindBindableMemberResult result)
            where T : IBinder
        {
            if (result.IsFound) 
                binder?.Bind(result.Adder!);
        }
        
        /// <summary>
        /// Safely binds a single binder to the specified event adder.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binder">The binder instance.</param>
        /// <param name="binderAdder">The event adder to bind to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T? binder, IBinderAdder binderAdder)
            where T : IBinder
        {
            binder?.Bind(binderAdder);
        }
        #endregion

        #region Array BindSafely
        /// <summary>
        /// Binds an array of binders to the provided <see cref="IBinderAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The array of binders to bind.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the array is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[]? binders, in FindBindableMemberResult result)
            where T : IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        /// <summary>
        /// Safely binds an array of binders to the specified event adder.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The array of binders.</param>
        /// <param name="binderAdder">The event adder to bind to.</param>
        /// <exception cref="NullReferenceException">
        /// Thrown if any individual binder in the array is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[]? binders, IBinderAdder binderAdder)
            where T : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new BindSafelyNullReferenceException($"{nameof(binder)}");
                binder.Bind(binderAdder);
            }
        }
        #endregion
        
        #region List BindSafely
        /// <summary>
        /// Binds a list of binders to the provided <see cref="IBinderAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The list of binders.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the list is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T>? binders, in FindBindableMemberResult result)
            where T : IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        /// <summary>
        /// Safely binds a list of binders to the specified event adder.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The list of binders.</param>
        /// <param name="binderAdder">The event adder to bind to.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the list is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T>? binders, IBinderAdder binderAdder)
            where T : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new BindSafelyNullReferenceException($"{nameof(binder)}");
                binder.Bind(binderAdder);
            }
        }
        #endregion
        
        #region IEnumerable BindSafely
        /// <summary>
        /// Binds an enumerable of binders to the provided <see cref="IBinderAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The enumerable of binders.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the collection is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T>? binders, in FindBindableMemberResult result)
            where T : IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        /// <summary>
        /// Safely binds an enumerable of binders to the specified event adder.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The enumerable of binders.</param>
        /// <param name="binderAdder">The event adder to bind to.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the collection is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T>? binders, IBinderAdder binderAdder)
            where T : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new BindSafelyNullReferenceException($"{nameof(binder)}");
                binder.Bind(binderAdder);
            }
        }
        #endregion
    }
}