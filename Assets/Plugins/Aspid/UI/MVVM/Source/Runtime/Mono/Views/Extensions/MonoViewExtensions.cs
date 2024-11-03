using System;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.Mono.Views.Extensions
{
    /// <summary>
    /// Предоставляет методы расширения для интерфейса <see cref="IView"/>.
    /// </summary>
    public static class MonoViewExtensions
    {
        /// <summary>
        /// Разрушает View и деинициализирует ее, если она не реализует интерфейс <see cref="IDisposable"/>.
        /// Если View реализует <see cref="IDisposable"/>, вызывает метод <see cref="IDisposable.Dispose"/>.
        /// Возвращает экземпляр <see cref="IViewModel"/>, который был привязан к View перед ее уничтожением.
        /// </summary>
        /// <typeparam name="T">Тип View, который наследуется от <see cref="Component"/> и реализует интерфейс <see cref="IView"/>.</typeparam>
        /// <param name="view">Экземпляр View, который необходимо уничтожить.</param>
        /// <returns>ViewModel, который был привязан к View, или <c>null</c>, если ViewModel не было.</returns>
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
                Object.Destroy(view.gameObject);
            }

            return viewModel;
        }
        
        /// <summary>
        /// Разрушает View и деинициализирует ее, если она не реализует интерфейс <see cref="IDisposable"/>.
        /// Если View реализует <see cref="IDisposable"/>, вызывает метод <see cref="IDisposable.Dispose"/>.
        /// Возвращает экземпляр <see cref="IViewModel"/>, который был привязан к View перед ее уничтожением.
        /// </summary>
        /// <param name="view">Экземпляр View, который необходимо уничтожить.</param>
        /// <returns>ViewModel, который был привязан к View, или <c>null</c>, если ViewModel не было.</returns>
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
                if (view is Component component) Object.Destroy(component.gameObject);
            }

            return viewModel;
        }
    }
}