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
        public static void BindSafely<T>(this IBinder? binder, IViewModel viewModel, in Id id)
        {
            var result = viewModel.FindBindableMember<T>(id);
            if (result.IsFound) binder.BindSafely(result.Member);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IBinder? binder, in BindableMember<T> bindableMember) =>
            binder?.Bind(bindableMember);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder, T>(this TBinder? binder, IViewModel viewModel, in Id id)
            where TBinder : IBinder
        {
            var result = viewModel.FindBindableMember<T>(id);
            if (result.IsFound) binder.BindSafely(result.Member);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder, T>(this TBinder? binder, in BindableMember<T> bindableMember)
            where TBinder : IBinder
        {
            binder?.Bind(bindableMember);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this TBinder? binder, IViewModel viewModel, in Id id)
            where TBinder : IBinder
        {
            var result = viewModel.FindBindableMember(id);
            if (result.IsFound) binder.BindSafely(result.Adder!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this TBinder? binder, IViewModelEventAdder adder)
            where TBinder : IBinder
        {
            binder?.Bind(adder);
        }
        #endregion

        #region Array BindSafely
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder, T>(this TBinder[]? binders, IViewModel viewModel, in Id id)
            where TBinder : IBinder
        {
            if (binders is null) return;
            
            var result = viewModel.FindBindableMember<T>(id);
            if (!result.IsFound) return;

            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(result.Member);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder, T>(this TBinder[]? binders, in BindableMember<T> bindableMember)
            where TBinder : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(bindableMember);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this TBinder[]? binders, IViewModel viewModel, in Id id)
            where TBinder : IBinder
        {
            if (binders is null) return;
            
            var result = viewModel.FindBindableMember(id);
            if (!result.IsFound) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(result.Adder!);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this TBinder[]? binders, IViewModelEventAdder adder)
            where TBinder : IBinder
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
        public static void BindSafely<TBinder, T>(this List<TBinder>? binders, IViewModel viewModel, in Id id)
            where TBinder : IBinder
        {
            if (binders is null) return;
            
            var result = viewModel.FindBindableMember<T>(id);
            if (!result.IsFound) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(result.Member);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder, T>(this List<TBinder>? binders, in BindableMember<T> bindableMember)
            where TBinder : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(bindableMember);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this List<TBinder>? binders, IViewModel viewModel, in Id id)
            where TBinder : IBinder
        {
            if (binders is null) return;
            
            var result = viewModel.FindBindableMember(id);
            if (!result.IsFound) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(result.Adder!);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this List<TBinder>? binders, IViewModelEventAdder adder)
            where TBinder : IBinder
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
        public static void BindSafely<TBinder, T>(this IEnumerable<TBinder>? binders, IViewModel viewModel, in Id id)
            where TBinder : IBinder
        {
            if (binders is null) return;
            
            var result = viewModel.FindBindableMember<T>(id);
            if (!result.IsFound) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(result.Member);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder, T>(this IEnumerable<TBinder>? binders, in BindableMember<T> bindableMember)
            where TBinder : IBinder
        {
            if (binders is null) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(bindableMember);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this IEnumerable<TBinder>? binders, IViewModel viewModel, in Id id)
            where TBinder : IBinder
        {
            if (binders is null) return;
            
            var result = viewModel.FindBindableMember(id);
            if (!result.IsFound) return;
            
            foreach (var binder in binders)
            {
                if (binder is null) throw new NullReferenceException(nameof(binder));
                binder.Bind(result.Adder!);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<TBinder>(this IEnumerable<TBinder>? binders, IViewModelEventAdder adder)
            where TBinder : IBinder
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