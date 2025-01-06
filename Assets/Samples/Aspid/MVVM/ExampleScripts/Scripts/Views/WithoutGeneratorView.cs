using Aspid.MVVM.Mono;

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