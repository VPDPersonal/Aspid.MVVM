using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views
{
    public sealed class ViewBinder : Binder, IBinder<IViewModel>
    {
        private readonly IView _view;

        public ViewBinder(IView view)
        {
            _view = view;
        }

        public void SetValue(IViewModel viewModel)
        {
            DeinitializeView();
            _view.Initialize(viewModel);
        }

        protected override void OnUnbound(IViewModel viewModel, string id) =>
            DeinitializeView();

        private void DeinitializeView() =>
            _view.Deinitialize();
    }
}