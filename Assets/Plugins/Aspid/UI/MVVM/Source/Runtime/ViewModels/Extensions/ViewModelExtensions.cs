using System;

namespace Aspid.UI.MVVM.ViewModels.Extensions
{
    /// <summary>
    /// Предоставляет методы расширения для интерфейса <see cref="IViewModel"/>.
    /// </summary>
    public static class ViewModelExtensions
    {
        /// <summary>
        /// Освобождает экземпляр ViewModel, если реализует <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel для освобождения.</param>
        /// <typeparam name="T">Тип ViewModel, который реализует <see cref="IViewModel"/> и <see cref="IDisposable"/>.</typeparam>
        public static void DisposeViewModel<T>(this T viewModel)
            where T : class, IViewModel, IDisposable
        {
            viewModel.Dispose();
        }
        
        /// <summary>
        /// Освобождает экземпляр ViewModel, если реализует <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel для освобождения.</param>
        public static void DisposeViewModel(this IViewModel viewModel)
        {
            if (viewModel is IDisposable disposable)
                disposable.Dispose();
        }
    }
}