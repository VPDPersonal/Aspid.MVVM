using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.Unity
{
    public static class MonoBinderExtensions
    {
        #region Single BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T binder, in FindBindableMemberResult result)
            where T : MonoBehaviour, IBinder
        {
            if (result.IsFound) 
                binder.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T binder, IBinderAdder binderAdder)
            where T : MonoBehaviour, IBinder
        {
            if (binder)
                binder.Bind(binderAdder);
        }
        #endregion

        #region Array BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[] binders, in FindBindableMemberResult result)
            where T : MonoBehaviour, IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[] binders, IBinderAdder binderAdder)
            where T : MonoBehaviour, IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (!binder) throw new NullReferenceException(nameof(binder));
                binder.Bind(binderAdder);
            }
        }
        #endregion
        
        #region List BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T> binders, in FindBindableMemberResult result)
            where T : MonoBehaviour, IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T> binders, IBinderAdder binderAdder)
            where T : MonoBehaviour, IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (!binder) throw new NullReferenceException(nameof(binder));
                binder.Bind(binderAdder);
            }
        }
        #endregion
        
        #region IEnumerable BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T> binders, in FindBindableMemberResult result)
            where T : MonoBehaviour, IBinder
        {
            if (result.IsFound) 
                binders.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T> binders, IBinderAdder binderAdder)
            where T : MonoBehaviour, IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (!binder) throw new NullReferenceException(nameof(binder));
                binder.Bind(binderAdder);
            }
        }
        #endregion
    }
}