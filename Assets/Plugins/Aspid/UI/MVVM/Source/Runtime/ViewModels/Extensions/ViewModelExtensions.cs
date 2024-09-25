using System;

namespace Aspid.UI.MVVM.ViewModels.Extensions
{
    public static class ViewModelExtensions
    {
        public static void DisposeViewModel<T>(this T viewModel)
            where T : class, IViewModel, IDisposable
        {
            viewModel.Dispose();
        }
        
        public static void DisposeViewModel(this IViewModel viewModel)
        {
            if (viewModel is IDisposable disposable)
                disposable.Dispose();
        }
    }
}