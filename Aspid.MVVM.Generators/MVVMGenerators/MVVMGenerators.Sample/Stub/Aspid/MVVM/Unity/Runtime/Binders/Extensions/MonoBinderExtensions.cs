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
        public static void BindSafely<TBinder, T>(this TBinder binder, in BindableMember<T> bindableMember)
            where TBinder : MonoBehaviour, IBinder
        {
            if (binder) 
                binder.Bind(bindableMember);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this TBinder binder, IViewModelEventAdder adder)
            where TBinder : MonoBehaviour, IBinder
        {
            if (binder) 
                binder.Bind(adder);
        }
        #endregion

        #region Array BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder, T>(this TBinder[] binders, in BindableMember<T> bindableMember)
            where TBinder : MonoBehaviour, IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (!binder) throw new NullReferenceException(nameof(binder));
                binder.Bind(bindableMember);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this TBinder[] binders, IViewModelEventAdder adder)
            where TBinder : MonoBehaviour, IBinder
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
        public static void BindSafely<TBinder, T>(this List<TBinder> binders, in BindableMember<T> bindableMember)
            where TBinder : MonoBehaviour, IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (!binder) throw new NullReferenceException(nameof(binder));
                binder.Bind(bindableMember);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this List<TBinder> binders, IViewModelEventAdder adder)
            where TBinder : MonoBehaviour, IBinder
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
        public static void BindSafely<TBinder, T>(this IEnumerable<TBinder> binders, in BindableMember<T> bindableMember)
            where TBinder : MonoBehaviour, IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (!binder) throw new NullReferenceException(nameof(binder));
                binder.Bind(bindableMember);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this IEnumerable<TBinder> binders, IViewModelEventAdder adder)
            where TBinder : MonoBehaviour, IBinder
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