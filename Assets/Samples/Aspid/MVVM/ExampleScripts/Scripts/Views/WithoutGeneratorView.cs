using Aspid.MVVM.Mono;
using Aspid.MVVM.Views;
using Aspid.MVVM.ViewModels;
using Aspid.MVVM.Extensions;

namespace Aspid.MVVM.ExampleScripts.Views
{
    public class WithoutGeneratorView : View
    {
        private readonly MonoBinder _singleBinder; 
        private readonly MonoBinder[] _arrayBinders;

        public WithoutGeneratorView(MonoBinder singleBinder, MonoBinder[] arrayBinders)
        {
            _singleBinder = singleBinder;
            _arrayBinders = arrayBinders;
        }

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