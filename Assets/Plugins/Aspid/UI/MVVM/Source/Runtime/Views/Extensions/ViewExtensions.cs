using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views.Extensions
{
    public static class ViewExtensions
    {
        public static IViewModel? DisposeView<T>(this T view)
            where T : class, IView, IDisposable
        {
            var viewModel = view.ViewModel;
            view.Dispose();

            return viewModel;
        }

        public static IViewModel? DisposeView(this IView view)
        {
            var viewModel = view.ViewModel;

            if (view is IDisposable disposable) disposable.Dispose();
            else view.Deinitialize();

            return viewModel;
        }
        
        public static IViewModel? DeinitializeView<T>(this T view)
            where T : class, IView, IDisposable
        {
            var viewModel = view.ViewModel;
            view.Deinitialize();
            
            return viewModel;
        }

        public static IViewModel? DeinitializeView(this IView view)
        {
            var viewModel = view.ViewModel;
            view.Deinitialize();
            
            return viewModel;
        }
    }
}