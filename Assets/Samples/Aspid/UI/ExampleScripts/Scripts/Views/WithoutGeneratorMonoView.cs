using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Extensions;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.ExampleScripts.Views
{
    public class WithoutGeneratorMonoView : MonoView
    {
        [SerializeField] private MonoBinder _singleBinder; 
        [SerializeField] private MonoBinder[] _arrayBinders;
        
        protected override void InitializeInternal(IViewModel viewModel)
        {
            _singleBinder.BindSafely(viewModel, "SingleBinder");
            _arrayBinders.BindSafely(viewModel, "ArrayBinders");
        }

        protected override void DeinitializeInternal()
        {
            _singleBinder.UnbindSafely();
            _arrayBinders.UnbindSafely();
        }
    }
}