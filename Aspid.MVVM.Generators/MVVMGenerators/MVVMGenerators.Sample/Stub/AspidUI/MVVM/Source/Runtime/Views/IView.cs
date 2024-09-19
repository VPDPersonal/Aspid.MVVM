using AspidUI.MVVM.ViewModels;

namespace AspidUI.MVVM.Views
{
    public interface IView
    {
        public void Initialize(IViewModel viewModel);

        public void Deinitialize(IViewModel viewModel);
    }
} 