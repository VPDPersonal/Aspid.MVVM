using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for the <see cref="IBinder"/> interface.
    /// </summary>
    public static partial class BinderExtensions
    {
        #region Singl BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T? binder, in FindBindableMemberResult result)
            where T : IBinder
        {
            if (result.IsFound) 
                binder.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T? binder, IBindableMemberEventAdder adder)
            where T : IBinder
        {
            binder?.Bind(adder);
        }
        #endregion

        #region Array BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[]? binders, in FindBindableMemberResult result)
            where T : IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[]? binders, IBindableMemberEventAdder adder)
            where T : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(adder);
            }
        }
        #endregion
        
        #region List BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<Binder>? binders, in FindBindableMemberResult result)
            where T : IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T>? binders, IBindableMemberEventAdder adder)
            where T : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(adder);
            }
        }
        #endregion
        
        #region IEnumerable BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<Binder>? binders, in FindBindableMemberResult result)
            where T : IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T>? binders, IBindableMemberEventAdder adder)
            where T : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(adder);
            }
        }
        #endregion
    }
}