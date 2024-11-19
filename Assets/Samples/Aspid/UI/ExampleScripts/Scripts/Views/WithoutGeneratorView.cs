using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Extensions;

namespace Aspid.UI.ExampleScripts.Views
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