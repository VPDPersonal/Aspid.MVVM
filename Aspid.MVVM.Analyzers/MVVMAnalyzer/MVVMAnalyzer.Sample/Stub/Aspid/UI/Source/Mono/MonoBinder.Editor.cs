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

        public IView View
        {
            get => _view;
            set
            {
                _view = value switch
                {
                    MonoView monoView => monoView,
                    _ =>  null,
                };
            }
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        partial void OnBinding(IViewModel viewModel, string id)
        {
            if (id != Id) Debug.LogWarning("Some Warning");
            if (_viewModel != null) throw new Exception();
            
            Id = _id;
            _viewModel = viewModel;
        }

        partial void OnUnbinding(IViewModel viewModel, string id)
        {
            if (Id != id) throw new Exception();
            if (_viewModel != viewModel) throw new Exception();

            _viewModel = null;
        }
    }
}
#endif