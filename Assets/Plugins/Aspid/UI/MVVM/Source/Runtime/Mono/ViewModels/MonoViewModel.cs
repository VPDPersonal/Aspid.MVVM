using UnityEngine;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono.ViewModels
{
    /// <summary>
    /// Абстрактный базовый класс для реализации ViewModel, который предоставляет методы для управления <see cref="IBinder"/>.
    /// Наследует <see cref="MonoBehaviour"/> и реализует интерфейс <see cref="IViewModel"/>.
    /// Этот класс не содержит собственной реализации и служит для унификации ViewModel как объектов <see cref="MonoBehaviour"/>.
    /// </summary>
    public abstract class MonoViewModel : MonoBehaviour, IViewModel
    {
        /// <summary>
        /// Добавляет Binder для указанного свойства ViewModel.
        /// Наследник должен реализовать данный метод.
        /// </summary>
        /// <param name="binder">Binder, который будет добавлен.</param>
        /// <param name="propertyName">Имя свойства, к которому будет привязан Binder</param>
        public abstract void AddBinder(IBinder binder, string propertyName);

        /// <summary>
        /// Удаляет Binder для указанного свойства ViewModel.
        /// Наследник должен реализовать данный метод.
        /// </summary>
        /// <param name="binder">Binder, который будет удален.</param>
        /// <param name="propertyName">Имя свойства, от которого будет отвязан Binder.</param>
        public abstract void RemoveBinder(IBinder binder, string propertyName);
    }
}