#if UNITY_EDITOR && !ASPID_UI_EDITOR_DISABLED
#nullable disable
using System;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono
{
    public abstract partial class MonoBinder : IMonoBinderValidable
    {
        [SerializeField] private MonoView _view;
        [SerializeField] private string _id;
        
        private IViewModel _viewModel;

        public string Id
        {
            get => _id;
            set => _id = value;
        }
        
        public IView View
        {
            get => _view;
            set
            {
                if (value is MonoView view) _view = view;
                else throw new ArgumentException("View is not a MonoView");
            }
        }

        partial void OnBinding(IViewModel viewModel, string id)
        {
            if (Id != id) throw new Exception($"Id not match. Binder Id {Id}; Id {id}.");
            if (_viewModel is not null) throw new Exception("Binder has already been bound");
            
            _viewModel = viewModel;
        }

        partial void OnUnbinding(IViewModel viewModel, string id)
        {
            if (Id != id) throw new Exception($"Id not match. Binder Id {Id}; Id {id}.");
            if (_viewModel != viewModel) throw new Exception($"ViewModel not match. Old ViewModel {_viewModel?.GetType()}; NewViewModel {viewModel.GetType()}.");

            _viewModel = null;
        }
    }
}
#endif