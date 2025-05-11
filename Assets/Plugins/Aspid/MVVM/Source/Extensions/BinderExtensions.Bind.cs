using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        #region Singl BindSafely
        /// <summary>
        /// Binds a single binder to the provided <see cref="IBindableMemberEventAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type, implementing <see cref="IBinder"/>.</typeparam>
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
        /// <param name="adder">The event adder to bind to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T? binder, IBindableMemberEventAdder adder)
            where T : IBinder
        {
            binder?.Bind(adder);
        }
        #endregion

        #region Array BindSafely
        /// <summary>
        /// Binds an array of binders to the provided <see cref="IBindableMemberEventAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The array of binders to bind.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <exception cref="NullReferenceException">
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
        /// <param name="adder">The event adder to bind to.</param>
        /// <exception cref="NullReferenceException">
        /// Thrown if any individual binder in the array is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[]? binders, IBindableMemberEventAdder adder)
            where T : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                binder?.Bind(adder);
            }
        }
        #endregion
        
        #region List BindSafely
        /// <summary>
        /// Binds a list of binders to the provided <see cref="IBindableMemberEventAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The list of binders.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <exception cref="NullReferenceException">
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
        /// <param name="adder">The event adder to bind to.</param>
        /// <exception cref="NullReferenceException">
        /// Thrown if any individual binder in the list is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T>? binders, IBindableMemberEventAdder adder)
            where T : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                binder?.Bind(adder);
            }
        }
        #endregion
        
        #region IEnumerable BindSafely
        /// <summary>
        /// Binds an enumerable of binders to the provided <see cref="IBindableMemberEventAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The enumerable of binders.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <exception cref="NullReferenceException">
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
        /// <param name="adder">The event adder to bind to.</param>
        /// <exception cref="NullReferenceException">
        /// Thrown if any individual binder in the collection is <c>null</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T>? binders, IBindableMemberEventAdder adder)
            where T : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                binder?.Bind(adder);
            }
        }
        #endregion
    }
}