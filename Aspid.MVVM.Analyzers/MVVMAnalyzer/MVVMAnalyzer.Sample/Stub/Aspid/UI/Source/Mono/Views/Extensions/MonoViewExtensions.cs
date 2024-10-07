using System;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.Mono.Views.Extensions
{
    public static class MonoViewExtensions
    {
        public static IViewModel? DestroyView<T>(this T view)
            where T : Component, IView
        {
            var viewModel = view.ViewModel;

            if (view is IDisposable disposable)
            {
                disposable.Dispose();
            }
            else
            {
                view.Deinitialize();
                // Object.Destroy(view.gameObject);
            }

            return viewModel;
        }
        
        public static IViewModel? DestroyView(this IView view)
        {
            var viewModel = view.ViewModel;

            if (view is IDisposable disposable)
            {
                disposable.Dispose();
            }
            else
            {
                view.Deinitialize();
                // if (view is Component component) Object.Destroy(component.gameObject);
            }

            return viewModel;
        }
    }
}