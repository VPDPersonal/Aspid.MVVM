#if UNITY_EDITOR && !ASPID_UI_EDITOR_DISABLED
using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM
{
    public abstract partial class Binder
    {
        private IViewModel? _viewModel;

        partial void OnBindingDebug(IViewModel viewModel, string id)
        {
            if (_viewModel is not null) throw new Exception("Binder has already been bound");
            _viewModel = viewModel;
        }

        partial void OnUnbindingDebug() => _viewModel = null;
    }
}
#endif