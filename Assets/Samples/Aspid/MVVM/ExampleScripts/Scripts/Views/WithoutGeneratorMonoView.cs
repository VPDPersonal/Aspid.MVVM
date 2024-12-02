using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Extensions;
using Aspid.MVVM.Mono.Views;
using Aspid.MVVM.ViewModels;

namespace Aspid.MVVM.ExampleScripts.Views
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