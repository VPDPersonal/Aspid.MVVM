using System;
using Aspid.UI.MVVM.ViewModels;
using System.Collections.Generic;

namespace Aspid.UI.MVVM.Extensions
{
    /// <summary>
    /// Предоставляет методы расширения для интерфейса <see cref="IBinder"/>.
    /// </summary>
    public static class BinderExtensions
    {
        /// <summary>
        /// Безопасное связывание для объекта <see cref="IBinder"/>.
        /// Если <paramref name="binder"/> равен <c>null</c>, связывание не выполняется.
        /// </summary>
        /// <param name="binder">Объект, который будет связываться с <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">ViewModel, с которой устанавливается связь.</param>
        /// <param name="id">ID компонента, который совпадает с именем свойства у ViewModel.</param>
        /// <typeparam name="T">Тип, реализующий интерфейс <see cref="IBinder"/>.</typeparam>
        public static void BindSafely<T>(this T? binder, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binder is null) return;
            binder.Bind(viewModel, id);
        }
        
        /// <summary>
        /// Безопасное связывание для массива объектов <see cref="IBinder"/>.
        /// Если <paramref name="binders"/> равен <c>null</c>, связывание не выполняется.
        /// Если какой-либо элемент массива равен <c>null</c>, выбрасывается исключение <see cref="NullReferenceException"/>.
        /// </summary>
        /// <param name="binders">Массив объектов, которые будут связываться с <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">ViewModel, с которой устанавливается связь.</param>
        /// <param name="id">ID компонента, который совпадает с именем свойства у ViewModel.</param>
        /// <typeparam name="T">Тип, реализующий интерфейс <see cref="IBinder"/>.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        public static void BindSafely<T>(this T[]? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
	            if (binder is null) throw new NullReferenceException($"Binder {id} is null. ViewModel {viewModel.GetType().FullName}");
	            binder.Bind(viewModel, id);
            }
        }

        /// <summary>
        /// Безопасное связывание для листа объектов <see cref="IBinder"/>.
        /// Если <paramref name="binders"/> равен <c>null</c>, связывание не выполняется.
        /// Если какой-либо элемент листа равен <c>null</c>, выбрасывается исключение <see cref="NullReferenceException"/>.
        /// </summary>
        /// <param name="binders">Лист объектов, которые будут связываться с <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">ViewModel, с которой устанавливается связь.</param>
        /// <param name="id">ID компонента, который совпадает с именем свойства у ViewModel.</param>
        /// <typeparam name="T">Тип, реализующий интерфейс <see cref="IBinder"/>.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        public static void BindSafely<T>(this List<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
	            if (binder is null) throw new NullReferenceException($"Binder {id} is null. ViewModel {viewModel.GetType().FullName}");
	            binder.Bind(viewModel, id);
            }
        }
        
        /// <summary>
        /// Безопасное связывание для перечисления объектов <see cref="IBinder"/>.
        /// Если <paramref name="binders"/> равен <c>null</c>, связывание не выполняется.
        /// Если какой-либо элемент перечисления равен <c>null</c>, выбрасывается исключение <see cref="NullReferenceException"/>.
        /// </summary>
        /// <param name="binders">Перечисление объектов, которые будут связываться с <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">ViewModel, с которой устанавливается связь.</param>
        /// <param name="id">ID компонента, который совпадает с именем свойства у ViewModel.</param>
        /// <typeparam name="T">Тип, реализующий интерфейс <see cref="IBinder"/>.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        public static void BindSafely<T>(this IEnumerable<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
	            if (binder is null) throw new NullReferenceException($"Binder {id} is null. ViewModel {viewModel.GetType().FullName}");
	            binder.Bind(viewModel, id);
            }
        }
        
        /// <summary>
        /// Безопасный разрыв привязки для объекта <see cref="IBinder"/>.
        /// Если <paramref name="binder"/> равен <c>null</c>, разрыв привязки не выполняется.
        /// </summary>
        /// <param name="binder">Объект, у которого будет разорвана привязка с <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">ViewModel, с которой разрывается связь.</param>
        /// <param name="id">ID компонента для разрыва привязки, который совпадает с именем свойства у ViewModel.</param>
        /// <typeparam name="T">Тип, реализующий интерфейс <see cref="IBinder"/>.</typeparam>
        public static void UnbindSafely<T>(this T? binder, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binder is null) return;
            binder.Unbind(viewModel, id);
        }
        
        /// <summary>
        /// Безопасный разрыв привязки для массива объектов <see cref="IBinder"/>.
        /// Если <paramref name="binders"/> равен <c>null</c>, разрыв привязки не выполняется.
        /// Если какой-либо элемент массива равен <c>null</c>, выбрасывается исключение <see cref="NullReferenceException"/>.
        /// </summary>
        /// <param name="binders">Массив объектов, у которого будет разорвана привязка с <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">ViewModel, с которой устанавливается связь.</param>
        /// <param name="id">ID компонента для разрыва привязки, который совпадает с именем свойства у ViewModel.</param>
        /// <typeparam name="T">Тип, реализующий интерфейс <see cref="IBinder"/>.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        public static void UnbindSafely<T>(this T[]? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
                binder.Unbind(viewModel, id);
        }
        
        /// <summary>
        /// Безопасный разрыв привязки для листа объектов <see cref="IBinder"/>.
        /// Если <paramref name="binders"/> равен <c>null</c>, разрыв привязки не выполняется.
        /// Если какой-либо элемент листа равен <c>null</c>, выбрасывается исключение <see cref="NullReferenceException"/>.
        /// </summary>
        /// <param name="binders">Лист объектов, у которого будет разорвана привязка с <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">ViewModel, с которой устанавливается связь.</param>
        /// <param name="id">ID компонента для разрыва привязки, который совпадает с именем свойства у ViewModel.</param>
        /// <typeparam name="T">Тип, реализующий интерфейс <see cref="IBinder"/>.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        public static void UnbindSafely<T>(this List<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
	            if (binder is null) throw new NullReferenceException($"Binder {id} is null. ViewModel {viewModel.GetType().FullName}");
	            binder.Unbind(viewModel, id);
            }
        }
        
        /// <summary>
        /// Безопасный разрыв привязки для перечисления объектов <see cref="IBinder"/>.
        /// Если <paramref name="binders"/> равен <c>null</c>, разрыв привязки не выполняется.
        /// Если какой-либо элемент перечисления равен <c>null</c>, выбрасывается исключение <see cref="NullReferenceException"/>.
        /// </summary>
        /// <param name="binders">Перечисление объектов, у которого будет разорвана привязка с <see cref="IViewModel"/>.</param>
        /// <param name="viewModel">ViewModel, с которой устанавливается связь.</param>
        /// <param name="id">ID компонента для разрыва привязки, который совпадает с именем свойства у ViewModel.</param>
        /// <typeparam name="T">Тип, реализующий интерфейс <see cref="IBinder"/>.</typeparam>
        /// <exception cref="NullReferenceException"></exception>
        public static void UnbindSafely<T>(this IEnumerable<T>? binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
	            if (binder is null) throw new NullReferenceException($"Binder {id} is null. ViewModel {viewModel.GetType().FullName}");
	            binder.Unbind(viewModel, id);
            }
        }
    }
}