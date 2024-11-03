using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Views.Extensions;
using Aspid.UI.MVVM.ViewModels.Extensions;

namespace Aspid.UI.MVVM.Views
{
    /// <summary>
    /// Связывает <see cref="IView"/> с его <see cref="IViewModel"/>.
    /// Реализует интерфейс <see cref="IBinder{T}"/> для работы с объектами типа <see cref="IViewModel"/>.
    /// </summary>
    public sealed class ViewBinder : Binder, IBinder<IViewModel?>
    {
        private readonly IView _view;
        private readonly bool _isDisposeViewModel;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ViewBinder"/> с заданным представлением и опцией
        /// для автоматического освобождения ресурсов модели представления.
        /// </summary>
        /// <param name="view">Представление, с которым будет связана модель представления.</param>
        /// <param name="isDisposeViewModel">Указывает, нужно ли освобождать ресурсы модели представления при деинициализации.</param>
        public ViewBinder(IView view, bool isDisposeViewModel = false)
        {
            _view = view;
            _isDisposeViewModel = isDisposeViewModel;
        }

        /// <summary>
        /// Устанавливает ViewModel для связываемого View.
        /// Деинициализирует текущее View перед установкой новой ViewModel.
        /// </summary>
        /// <param name="viewModel"></param>
        public void SetValue(IViewModel? viewModel)
        {
            DeinitializeView();
            
            if (viewModel is not null) 
                _view.Initialize(viewModel);
        }

        /// <summary>
        /// Вызывается при разрыве привязки с ViewModel. Деинициализирует View.
        /// </summary>
        /// <param name="viewModel">ViewModel, которая была отвязана.</param>
        /// <param name="id">ID компонента, который совпадает с именем свойства у ViewModel.</param>
        protected override void OnUnbound(IViewModel viewModel, string id) => 
            DeinitializeView();

        /// <summary>
        /// Деинициализирует текущее View и освобождает ViewModel, если это предусмотрено.
        /// </summary>
        private void DeinitializeView()
        {
            var viewModel = _view.DeinitializeView();
            if (_isDisposeViewModel) viewModel?.DisposeViewModel();
        }
    }
}