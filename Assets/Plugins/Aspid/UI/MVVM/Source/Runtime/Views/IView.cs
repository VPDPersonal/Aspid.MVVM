using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views
{
    /// <summary>
    /// Интерфейс для инициализации View с использованием заданного ViewModel.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Получает связанный ViewModel.
        /// Если представление не инициализировано, может возвращать <c>null</c>.
        /// </summary>
        public IViewModel? ViewModel { get; }
        
        /// <summary>
        /// Инициализирует представление с заданным <see cref="IViewModel"/> для привязки.
        /// </summary>
        /// <param name="viewModel">Объект <see cref="IViewModel"/> для инициализации View.</param>
        public void Initialize(IViewModel viewModel);
        
        /// <summary>
        /// Деинициализирует представление, обнуляя свойство ViewModel.
        /// </summary>
        public void Deinitialize();
    }
} 