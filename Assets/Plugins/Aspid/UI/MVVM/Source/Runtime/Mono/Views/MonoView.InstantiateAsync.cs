#if UNITY_2022_3_OR_NEWER
using System;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Object = UnityEngine.Object;

#if UNITY_2023_1_OR_NEWER
using System.Threading;
#endif // UNITY_2023_1_OR_NEWER

namespace Aspid.UI.MVVM.Mono.Views
{
    public abstract partial class MonoView
    {
        /// <summary>
        /// Асинхронно создает экземпляр View и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляра View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }

        /// <summary>
        /// Асинхронно создает экземпляр View с указанным родителем
        /// и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="parent">Родительский объект для нового экземпляра View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляра View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, parent);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }
        
        /// <summary>
        /// Асинхронно создает экземпляр View с заданными позицией и вращением,
        /// и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="position">Позиция для нового экземпляра View.</param>
        /// <param name="rotation">Вращение для нового экземпляра View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляра View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, position, rotation);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }

        /// <summary>
        /// Асинхронно создает экземпляр View с заданными родителем, позицией и вращением,
        /// и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="parent">Родительский объект для нового экземпляра View.</param>
        /// <param name="position">Позиция для нового экземпляра View.</param>
        /// <param name="rotation">Вращение для нового экземпляра View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляра View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, parent, position, rotation);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }

        /// <summary>
        /// Асинхронно создает несколько экземпляров View и инициализирует их с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="count">Количество экземпляров, которые необходимо создать.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляров View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            IViewModel viewModel) 
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        /// <summary>
        /// Асинхронно создает несколько экземпляров View с указанным родителем
        /// и инициализирует их с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="count">Количество экземпляров, которые необходимо создать.</param>
        /// <param name="parent">Родительский объект для новых экземпляров View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляров View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        /// <summary>
        /// Асинхронно создает несколько экземпляров View с заданными позицией и вращением, 
        /// и инициализирует их с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="count">Количество экземпляров, которые необходимо создать.</param>
        /// <param name="position">Позиция для новых экземпляров View.</param>
        /// <param name="rotation">Вращение для новых экземпляров View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляров View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, position, rotation);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        /// <summary>
        /// Асинхронно создает несколько View с заданными позициями и вращениями, 
        /// и инициализирует их с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="count">Количество экземпляров, которые необходимо создать.</param>
        /// <param name="positions">Массив позиций для новых экземпляров View.</param>
        /// <param name="rotations">Массив вращений для новых экземпляров View.</param>
        /// <param name="viewModel">Модель представления для View.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляров View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, positions, rotations);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        /// <summary>
        /// Асинхронно создает экземпляр View с заданными родителем, позицией и вращением,
        /// и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="count">Количество экземпляров, которые необходимо создать.</param>
        /// <param name="parent">Родительский объект для нового экземпляра View.</param>
        /// <param name="position">Позиция для нового экземпляра View.</param>
        /// <param name="rotation">Вращение для нового экземпляра View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляра View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, position, rotation);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        /// <summary>
        /// Асинхронно создает несколько экземпляров View с заданными родителем, позициями и вращениями, 
        /// и инициализирует их с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="count">Количество экземпляров, которые необходимо создать.</param>
        /// <param name="parent">Родительский объект для новых экземпляров View.</param>
        /// <param name="positions">Массив позиций для новых экземпляров View.</param>
        /// <param name="rotations">Массив вращений для новых экземпляров View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляров View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, positions, rotations);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

#if UNITY_2023_1_OR_NEWER
        /// <summary>
        /// Асинхронно создает несколько экземпляров View с заданными родителем, позицией и вращением, 
        /// и инициализирует их с заданным <see cref="IViewModel"/>. Поддерживает отмену через <see cref="CancellationToken"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="count">Количество экземпляров, которые необходимо создать.</param>
        /// <param name="parent">Родительский объект для новых экземпляров View.</param>
        /// <param name="position">Позиция для новых экземпляров View.</param>
        /// <param name="rotation">Вращение для новых экземпляров View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <param name="cancellationToken">Токен отмены для остановки процесса создания.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляров View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel,
            CancellationToken cancellationToken)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, position, rotation, cancellationToken);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        
        /// <summary>
        /// Асинхронно создает несколько экземпляров View с заданными родителем, позициями и вращениями, 
        /// и инициализирует их с заданным <see cref="IViewModel"/>. Поддерживает отмену через <see cref="CancellationToken"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="count">Количество экземпляров, которые необходимо создать.</param>
        /// <param name="parent">Родительский объект для новых экземпляров View.</param>
        /// <param name="positions">Массив позиций для новых экземпляров View.</param>
        /// <param name="rotations">Массив вращений для новых экземпляров View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <param name="cancellationToken">Токен отмены для остановки процесса создания.</param>
        /// <returns>Операция, представляющая асинхронный процесс создания экземпляров View.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel,
            CancellationToken cancellationToken)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, positions, rotations, cancellationToken);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }
#endif // UNITY_2023_1_OR_NEWER
    }
}
#endif // UNITY_2022_3_OR_NEWER