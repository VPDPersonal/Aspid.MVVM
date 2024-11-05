#if UNITY_EDITOR && !ASPID_UI_EDITOR_DISABLED
using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM
{
    public abstract partial class Binder
    {
        private string? _id;
        private IViewModel? _viewModel;

        partial void OnBindingDebug(IViewModel viewModel, string id)
        {
            if (_viewModel is not null) throw new Exception("Binder has already been bound");

            _id = id;
            _viewModel = viewModel;
        }

        partial void OnUnbindingDebug(IViewModel viewModel, string id)
        {
            if (_id != id) throw new Exception($"Id mismatch. OldId {_id}; NewID {id}.");
            if (_viewModel != viewModel) throw new Exception($"ViewModel not match. Old ViewModel {_viewModel?.GetType()}; NewViewModel {viewModel.GetType()}.");

            _id = null;
            _viewModel = null;
        }
    }
}
#endif