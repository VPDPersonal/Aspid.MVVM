using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM
{
    /// <summary>
    /// Интерфейс для связывания компонента с <see cref="IViewModel"/>
    /// для обеспечения функциональности привязки данных без возможности установки значения.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Определяет, включена ли обратная привязка (от View к ViewModel).
        /// Значение по умолчанию - false.
        /// </summary>
        public bool IsReverseEnabled => false;
        
        /// <summary>
        /// Связывает компонент с указанной <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="viewModel">ViewModel, с которой устанавливается связь.</param>
        /// <param name="id">ID компонента для привязки, который совпадает с именем свойства у ViewModel.</param>
        public void Bind(IViewModel viewModel, string id);
        
        /// <summary>
        /// Разрывает привязку компонента с указанной <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="viewModel">ViewModel, с которой разрывается связь.</param>
        /// <param name="id">ID компонента для разрыва привязки, который совпадает с именем свойства у ViewModel.</param>
        public void Unbind(IViewModel viewModel, string id);
    }
    
    /// <summary>
    /// Интерфейс для связывания компонента с <see cref="IViewModel"/>
    /// для обеспечения функциональности привязки данных с установкой значения.
    /// </summary>
    /// <typeparam name="T">Тип значения, которое будет устанавливаться.</typeparam>
    public interface IBinder<in T> : IBinder
    {
        /// <summary>
        /// Устанавливает значения для привязанного компонента.
        /// </summary>
        /// <param name="value">Значение, которое нужно установить.</param>
        public void SetValue(T value);
    }
}