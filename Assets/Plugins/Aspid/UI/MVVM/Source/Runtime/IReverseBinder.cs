using System;

namespace Aspid.UI.MVVM
{
    /// <summary>
    /// Интерфейс для создания обратной привязки данных от View к ViewModel.
    /// Обратная привязка данных используется для передачи измененных данных от компонента View в ViewModel.
    /// </summary>
    /// <typeparam name="T">Тип данных, передаваемый при обратной привязке.</typeparam>
    public interface IReverseBinder<out T> : IBinder
    {
        /// <summary>
        /// Событие, которое вызывается при изменении значения.
        /// </summary>
        public event Action<T>? ValueChanged;
        
        /// <summary>
        /// Определяет, что обратная привязка включена.
        /// Значение по умолчанию - true.
        /// </summary>
        bool IBinder.IsReverseEnabled => true;
    }
}