#if UNITY_EDITOR && !ULTIMATE_UI_EDITOR_DISABLED
#nullable disable
using System;
using UnityEngine;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Unity.Views;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity
{
    public abstract partial class MonoBinder
    {
        [Header("Source")]
        [SerializeField] private MonoView _view;
        [SerializeField] private string _id;

        private IViewModel _viewModel;

        public MonoView View
        {
            get => _view;
            set => _view = value;
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

        public void RebindOnlyEditor()
        {
            if (_viewModel == null) return;

            var viewModel = _viewModel;
            Unbind(_viewModel, Id);
            Bind(viewModel, Id);
        }
    }
}
#endif