using Aspid.UI.MVVM.ViewModels;
using System.Collections.Generic;

namespace Aspid.UI.MVVM.Extensions
{
    public static class BinderExtensions
    {
        public static void BindSafely<T>(this T? binder, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binder is null) return;
            binder.Bind(viewModel, id);
        }
        
        public static void BindSafely<T>(this T[]? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
                binder.Bind(viewModel, id);
        }

        public static void BindSafely<T>(this List<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
                binder.Bind(viewModel, id);
        }
        
        public static void BindSafely<T>(this IEnumerable<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
                binder.Bind(viewModel, id);
        }
        
        public static void UnbindSafely<T>(this T? binder, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binder is null) return;
            binder.Unbind(viewModel, id);
        }
        
        public static void UnbindSafely<T>(this T[]? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
                binder.Unbind(viewModel, id);
        }
        
        public static void UnbindSafely<T>(this List<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
                binder.Unbind(viewModel, id);
        }
        
        public static void UnbindSafely<T>(this IEnumerable<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
                binder.Unbind(viewModel, id);
        }
    }
}