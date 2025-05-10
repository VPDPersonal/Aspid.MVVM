using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.Unity
{
    public static class MonoBinderExtensions
    {
        #region Singl BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T binder, FindBindableMemberResult result)
            where T : MonoBehaviour, IBinder
        {
            if (!result.IsFound) 
                binder.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T binder, IBindableMemberEventAdder adder)
            where T : MonoBehaviour, IBinder
        {
            if (binder)
                binder.Bind(adder);
        }
        #endregion

        #region Array BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[] binders, FindBindableMemberResult result)
            where T : MonoBehaviour, IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[] binders, IBindableMemberEventAdder adder)
            where T : MonoBehaviour, IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (!binder) throw new NullReferenceException(nameof(binder));
                binder.Bind(adder);
            }
        }
        #endregion
        
        #region List BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<Binder> binders, FindBindableMemberResult result)
            where T : MonoBehaviour, IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T> binders, IBindableMemberEventAdder adder)
            where T : MonoBehaviour, IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (!binder) throw new NullReferenceException(nameof(binder));
                binder.Bind(adder);
            }
        }
        #endregion
        
        #region IEnumerable BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<Binder> binders, FindBindableMemberResult result)
            where T : MonoBehaviour, IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T> binders, IBindableMemberEventAdder adder)
            where T : MonoBehaviour, IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (!binder) throw new NullReferenceException(nameof(binder));
                binder.Bind(adder);
            }
        }
        #endregion
    }
}