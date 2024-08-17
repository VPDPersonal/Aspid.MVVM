using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public sealed class ViewBinder : Binder, IBinder<IViewModel>
    {
        private IViewModel? _viewModel;
        
        private readonly IView _view;

        public ViewBinder(IView view)
        {
            _view = view;
        }

        public void SetValue(IViewModel viewModel)
        {
            DeinitializeView();

            _viewModel = viewModel;
            _view.Initialize(viewModel);
        }

        protected override void OnUnbound(IViewModel viewModel, string id) =>
            DeinitializeView();

        private void DeinitializeView()
        {
            if (_viewModel == null) return;
            
            _view.Deinitialize(_viewModel);
            _viewModel = null;
        }
    }
}