using System;
using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.Extensions;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.ExampleScripts.Views
{
    public class WithoutGeneratorNoneView : MonoBehaviour, IView
    {
        private readonly MonoBinder _singleBinder; 
        private readonly MonoBinder[] _arrayBinders;

        public IViewModel ViewModel { get; private set; }
        
        public void Initialize(IViewModel viewModel)
        {
            if (ViewModel is not null) throw new Exception("View is already initialized");

            ViewModel = viewModel;
            _singleBinder.BindSafely(viewModel, "SingleBinder");
            _arrayBinders.BindSafely(viewModel, "ArrayBinders");
        }

        public void Deinitialize()
        {
            ViewModel = null;
            _singleBinder.UnbindSafely();
            _arrayBinders.UnbindSafely();
        }
    }
}