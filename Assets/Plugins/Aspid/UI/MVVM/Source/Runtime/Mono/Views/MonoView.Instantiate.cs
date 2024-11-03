using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono.Views
{
    public abstract partial class MonoView
    {
        /// <summary>
        /// Создает экземпляр View и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Созданный экземпляр View.</returns>
        public static T Instantiate<T>(
            T original, 
            IViewModel viewModel)
            where T : Object, IView
        {
            var view = Object.Instantiate(original); 
            view.Initialize(viewModel);

            return view;
        }

        /// <summary>
        /// Создает экземпляр View с указанным родителем и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="parent">Родительский объект для нового экземпляра View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Созданный экземпляр View.</returns>
        public static T Instantiate<T>(
            T original, 
            Transform parent,
            IViewModel viewModel) 
            where T : Object, IView
        {
            return Instantiate(original, parent, false, viewModel);
        }
        
        /// <summary>
        /// Создает экземпляр View с указанным родителем и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="parent">Родительский объект для нового экземпляра View.</param>
        /// <param name="worldPositionStays">Указывает, должен ли новый экземпляр сохранять мировые позиции.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Созданный экземпляр View.</returns>
        public static T Instantiate<T>(
            T original,
            Transform parent,
            bool worldPositionStays,
            IViewModel viewModel) 
            where T : Object, IView
        {
            var view = Object.Instantiate(original, parent, worldPositionStays);
            view.Initialize(viewModel);

            return view;
        }

        /// <summary>
        /// Создает экземпляр View с заданными позицией и вращением, и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="position">Позиция для нового экземпляра View.</param>
        /// <param name="rotation">Вращение для нового экземпляра View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Созданный экземпляр View.</returns>
        public static T Instantiate<T>(
            T original,
            Vector3 position,
            Quaternion rotation, 
            IViewModel viewModel)
            where T : Object, IView
        {
            var view = Object.Instantiate(original, position, rotation);
            view.Initialize(viewModel);

            return view;
        }
        
        /// <summary>
        /// Создает экземпляр View с заданными позицией, вращением и родителем, и инициализирует его с заданным <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">Тип View, который должен наследовать от <see cref="Object"/> и реализовывать <see cref="IView"/>.</typeparam>
        /// <param name="original">Исходный объект View, который будет инстанцирован.</param>
        /// <param name="position">Позиция для нового экземпляра View.</param>
        /// <param name="rotation">Вращение для нового экземпляра View.</param>
        /// <param name="parent">Родительский объект для нового экземпляра View.</param>
        /// <param name="viewModel">ViewModel для инициализации.</param>
        /// <returns>Созданный экземпляр View.</returns>
        public static T Instantiate<T>(
            T original,
            Vector3 position, 
            Quaternion rotation,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            var view = Object.Instantiate(original, position, rotation, parent);
            view.Initialize(viewModel);

            return view;
        }
    }
}