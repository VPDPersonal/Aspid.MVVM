using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Views.Extensions;
using Aspid.UI.MVVM.ViewModels.Extensions;

namespace Aspid.UI.MVVM.Views
{
    public sealed class ViewBinder : Binder, IBinder<IViewModel?>
    {
        private readonly IView _view;
        private readonly bool _isDisposeViewModel;

        public ViewBinder(IView view, bool isDisposeViewModel = false)
        {
            _view = view;
            _isDisposeViewModel = isDisposeViewModel;
        }

        public void SetValue(IViewModel? viewModel)
        {
            DeinitializeView();
            
            if (viewModel is not null) 
                _view.Initialize(viewModel);
        }

        protected override void OnUnbound(IViewModel viewModel, string id) => 
            DeinitializeView();

        private void DeinitializeView()
        {
            var viewModel = _view.DeinitializeView();
            if (_isDisposeViewModel) viewModel?.DisposeViewModel();
        }
    }
}