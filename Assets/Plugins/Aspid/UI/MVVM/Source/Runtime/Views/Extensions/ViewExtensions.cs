using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views.Extensions
{
    /// <summary>
    /// Предоставляет методы расширения для интерфейса <see cref="IView"/>.
    /// </summary>
    public static class ViewExtensions
    {
        /// <summary>
        /// Освобождает View, если реализует <see cref="IDisposable"/> и возвращает связанный <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="view"></param>
        /// <typeparam name="T">Тип View, который реализует <see cref="IView"/> и <see cref="IDisposable"/>.</typeparam>
        /// <returns>Связанный <see cref="IViewModel"/> или <c>null</c>, если его нет.</returns>
        public static IViewModel? DisposeView<T>(this T view)
            where T : IView, IDisposable
        {
            var viewModel = view.ViewModel;
            view.Dispose();

            return viewModel;
        }
        
        /// <summary>
        /// Освобождает View, если реализует <see cref="IDisposable"/> и возвращает связанный <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="view">View для освобождения.</param>
        /// <returns>Связанный <see cref="IViewModel"/> или <c>null</c>, если его нет.</returns>
        public static IViewModel? DisposeView(this IView view)
        {
            var viewModel = view.ViewModel;

            if (view is IDisposable disposable) disposable.Dispose();
            else view.Deinitialize();

            return viewModel;
        }
        
        /// <summary>
        /// Деинициализирует View  и возвращает связанный <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который реализует <see cref="IView"/>.</typeparam>
        /// <param name="view">View для деинициализации.</param>
        /// <returns>Связанный <see cref="IViewModel"/> или <c>null</c>, если его нет.</returns>
        public static IViewModel? DeinitializeView<T>(this T view)
            where T : IView
        {
            var viewModel = view.ViewModel;
            view.Deinitialize();
            
            return viewModel;
        }
    }
}