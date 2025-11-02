using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
// ReSharper disable SuspiciousTypeConversion.Global
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/View Initializers/View Initializer Manual")]
    public sealed class ViewInitializerManual : ViewInitializerBase
    {
        private IViewModel _viewModel;

        public void Initialize(IViewModel viewModel)
        {
            if (IsInitialized)
                throw new Exception($"{nameof(ViewInitializerManual)} can't be initialized twice");

            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            
            foreach (var view in Views)
                view.Initialize(viewModel);
            
            IsInitialized = true;
        }

        public void Deinitialize()
        {
            if (!IsInitialized) return;

            foreach (var view in Views)
                view.Deinitialize();

            _viewModel = null;
            IsInitialized = false;
        }

        private void OnDestroy()
        {
            if (IsDisposeViewOnDestroy)
            {
                foreach (var view in Views)
                    view.DisposeView();
            }
        }

        public override IViewModel ViewModel => _viewModel;
    }
}